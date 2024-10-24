using System;
using System.Collections.Generic;
using UnityEngine;

namespace Incantium.Audio
{
    /// <summary>
    /// Class that handles the fading in and out of audio clips, alongside seamlessly looping audio clips with custom
    /// looping boundaries.
    /// </summary>
    internal sealed class AudioTrack : MonoBehaviour
    {
        private const int SOURCE_AMOUNT = 2;
        private const float DEFAULT_FADE = 0.055f;
        private const float PREPARE = 0.02f;

        [SerializeField]
        private Status _status = Status.Idle;
        internal Status status => _status;
        
        private readonly Queue<AudioSource> sources = new();
        private MusicClip music;
        private double transition;
        private double pause;
        private float volume;
        private float fadeSeconds;
        
        /// <summary>
        /// Method called at initialization. This method will create new <see cref="AudioSource"/>s. At least two are
        /// required to use smart looping.
        /// </summary>
        private void Awake()
        {
            for (var i = 1; i <= SOURCE_AMOUNT; i++)
            {
                CreateSources(i);
            }
        }
        
        /// <summary>
        /// Method to create new <see cref="AudioSource"/>s.
        /// </summary>
        /// <param name="index">The number of the <see cref="AudioSource"/>.</param>
        private void CreateSources(int index)
        {
            var gameObject = new GameObject($"Source {index}");
            gameObject.transform.parent = transform;
            
            var source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            
            sources.Enqueue(source);
        }
        
        /// <summary>
        /// Method called each frame. This method will execute the starting, stopping and rescheduling code if this
        /// <see cref="AudioTrack"/> is not <see cref="Status.Idle"/>.
        /// </summary>
        private void Update()
        {
            if (_status is Status.Idle) return;

            Starting();
            Stopping();
            Schedule();
        }
        
        /// <summary>
        /// Method to increase the volume to the desired amount when starting a new music clip.
        /// </summary>
        private void Starting()
        {
            if (!music || _status is not Status.Starting) return;

            volume += Time.deltaTime / fadeSeconds;
                
            if (volume >= 1)
            {
                volume = 1;
                _status = Status.Playing;
            }
            
            ApplyVolume();
        }

        /// <summary>
        /// Method to decrease the volume to nothing when stopping a new music clip.
        /// </summary>
        private void Stopping()
        {
            if (!music || _status is not Status.Stopping) return;
            
            volume -= Time.deltaTime / fadeSeconds;
                
            if (volume <= 0)
            {
                volume = 0;
                _status = Status.Idle;
                Stop();
            }

            ApplyVolume();
        }

        /// <summary>
        /// Method to reschedule the smart looping of a music clip.
        /// </summary>
        private void Schedule()
        {
            if (!music || music.type is not MusicType.Smart) return;
            
            var halfway = AudioSettings.dspTime + music.main / 2f;
            
            if (halfway <= transition) return;
            
            var stop = sources.Dequeue();
            var start = sources.Peek();
            sources.Enqueue(stop);
            
            stop.SetScheduledEndTime(transition);
            start.time = music.start;
            start.PlayScheduled(transition);

            transition += music.type is MusicType.Loop ? music.length : music.main;
        }
        
        /// <summary>
        /// Method to apply the volume to all the <see cref="AudioSource"/>s of this track.
        /// </summary>
        private void ApplyVolume()
        {
            if (!music) return;

            foreach (var source in sources)
            {
                source.volume = volume * music.volume;
            }
        }
        
        /// <summary>
        /// Method to start playing a new music clip on this track.
        /// </summary>
        /// <param name="music">The music clip to play.</param>
        /// <param name="fadeSeconds">The amount of seconds to fade in and out between the tracks.</param>
        internal void Play(MusicClip music, float fadeSeconds)
        {
            this.music = music;
            this.fadeSeconds = fadeSeconds <= 0 ? DEFAULT_FADE : fadeSeconds;
            
            foreach (var scheduler in sources)
            {
                scheduler.clip = music.clip;
                scheduler.pitch = music.pitch;
                scheduler.loop = music.type is MusicType.Loop;
            }
            
            if (music.type is MusicType.Once) throw new ArgumentException("Cannot loop music that is a sound effect.");
            if (music.type is MusicType.Loop) PlayLoop();
            else PlaySmart(music);

            _status = Status.Starting;
        }

        /// <summary>
        /// Method to play a music clip on loop.
        /// </summary>
        private void PlayLoop() => sources.Peek().Play();

        /// <summary>
        /// Method to play a music clip with smart looping.
        /// </summary>
        /// <param name="music">The music clip to be played.</param>
        private void PlaySmart(MusicClip music)
        {
            var now = AudioSettings.dspTime + PREPARE;
            
            sources.Peek().PlayScheduled(now);
            
            transition = now + music.intro + music.main;
        }
        
        /// <summary>
        /// Method to pause this audio track from playing.
        /// </summary>
        internal void Pause()
        {
            sources.Peek().Pause();
            pause = transition - AudioSettings.dspTime;
        }
        
        /// <summary>
        /// Method to resume playing the music clip on this audio track.
        /// </summary>
        internal void Resume()
        {
            sources.Peek().Play();
            transition = AudioSettings.dspTime + pause;
            Schedule();
        }
        
        /// <summary>
        /// Method to stop the audio track.
        /// </summary>
        /// <param name="fadeSeconds">The amount of seconds to fade out the music clip.</param>
        internal void Stop(float fadeSeconds)
        {
            if (!music) return;
            
            this.fadeSeconds = fadeSeconds <= 0 ? DEFAULT_FADE : fadeSeconds;
            _status = Status.Stopping;
        }
        
        /// <summary>
        /// Method to completely stop the audio track from playing and unload the music clip data.
        /// </summary>
        private void Stop()
        {
            foreach (var source in sources)
            {
                source.Stop();
                source.time = 0;
            }

            music.clip.UnloadAudioData();
            music = null;
            _status = Status.Idle;
        }
    }
}