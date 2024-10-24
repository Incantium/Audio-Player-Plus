using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Incantium.Audio
{
    /// <summary>
    /// Class that handles the creation of different audio tracks that can fade audio between each other.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    internal sealed class AudioPlayer : SingleBehaviour<AudioPlayer>
    {
        private const int TRACK_AMOUNT = 2;
        
        [SerializeField] 
        [Tooltip("The audio clip currently in play.")]
        internal MusicClip music;
        
        [SerializeField]
        [Tooltip("True if the current audio clip should be played at wake-up.")]
        internal bool playOnAwake;

        private readonly Queue<AudioTrack> tracks = new();
        private AudioSource sfx;
        private Coroutine routine;

        /// <summary>
        /// Method called when initializing the audio player. This method will create new <see cref="AudioTrack"/>s to
        /// play music on. At least two is required for a cross-fade effect.
        /// </summary>
        private new void Awake()
        {
            base.Awake();
            
            sfx = GetComponent<AudioSource>();

            for (var i = 1; i <= TRACK_AMOUNT; i++)
            {
                CreateTrack(i);
            }
        }
        
        /// <summary>
        /// Method to create a new <see cref="AudioTrack"/>.
        /// </summary>
        /// <param name="index">The number for the audio track.</param>
        private void CreateTrack(int index)
        {
            var gameObject = new GameObject($"Track {index}");
            gameObject.transform.parent = transform;
            
            tracks.Enqueue(gameObject.AddComponent<AudioTrack>());
        }

        /// <summary>
        /// Method called after initialization is complete. This method will start the first music clip if possible.
        /// </summary>
        private void Start()
        {
            if (!playOnAwake || !music) return;
            
            Play(0, FadeType.CrossFade);
        }

        /// <summary>
        /// Method called when the game is pausing or resuming.
        /// </summary>
        /// <param name="paused">True if the game is pausing, otherwise false.</param>
        private void OnApplicationPause(bool paused)
        {
            if (paused) Pause();
            else Resume();
        }
        
        /// <summary>
        /// Method to start playing the audio clip already set in the audio player.
        /// </summary>
        /// <param name="fadeSeconds">The amount of seconds to fade in and out between audio clips. Setting this to 0
        /// will mean instant transition.</param>
        /// <param name="type">How to fade the audio clip between each other.</param>
        /// <seealso cref="Stop"/>
        public void Play(float fadeSeconds, FadeType type) => Play(music, fadeSeconds, type);
        
        /// <summary>
        /// Method to start playing a new audio clip.
        /// </summary>
        /// <param name="music">The audio clip to play.</param>
        /// <param name="fadeSeconds">The amount of seconds to fade in and out between audio clips. Setting this to 0
        /// will mean instant transition.</param>
        /// <param name="type">How to fade the audio clip between each other.</param>
        /// <seealso cref="Stop"/>
        public void Play(MusicClip music, float fadeSeconds, FadeType type)
        {
            if (!music) throw new ArgumentException("Missing required audio music.");
            if (this.music == music) return;

            this.music = music;
            
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(PlayRoutine(music, fadeSeconds, type));
        }

        private IEnumerator PlayRoutine(MusicClip music, float fadeSeconds, FadeType type)
        {
            yield return LoadClip(music);
            yield return Switch(fadeSeconds, type is FadeType.Wait);
        }

        /// <summary>
        /// Method to play the audio clip as a sound effect once in the background.
        /// </summary>
        /// <param name="music">The audio clip to play.</param>
        public void PlaySFX(MusicClip music) => sfx.PlayOneShot(music.clip, music.volume);
        
        /// <summary>
        /// Method to stop the audio player.
        /// </summary>
        /// <param name="fadeSeconds">The amount of seconds to fade out the audio player.</param>
        /// <seealso cref="Play(float,Incantium.Audio.FadeType)"/>
        public void Stop(float fadeSeconds = 0) => tracks.Peek().Stop(fadeSeconds);
        
        /// <summary>
        /// Method to pause the audio player.
        /// </summary>
        /// <seealso cref="Resume"/>
        public void Pause() => tracks.Peek().Pause();

        /// <summary>
        /// Method to resume the audio player.
        /// </summary>
        /// <seealso cref="Pause"/>
        public void Resume() => tracks.Peek().Resume();
        
        /// <summary>
        /// Method to load in a music clip into local memory. This method will wait until this process has been
        /// completed.
        /// </summary>
        /// <param name="music">The music clip to load into local memory.</param>
        private static IEnumerator LoadClip(MusicClip music)
        {
            music.clip.LoadAudioData();

            yield return new WaitUntil(() => music.clip.loadState 
                is AudioDataLoadState.Loaded 
                or AudioDataLoadState.Failed);
        }
        
        /// <summary>
        /// Method to switch to a new music clip. This method will stop the previously running music clip and start the
        /// new music clip. This method can also wait for the previous track to have stopped before starting the next
        /// music clip.
        /// </summary>
        /// <param name="fadeSeconds">The amount of seconds to fade in and out the music clips.</param>
        /// <param name="wait">True if this method should wait before the previously has stopped completely, otherwise
        /// false.</param>
        private IEnumerator Switch(float fadeSeconds, bool wait)
        {
            var previous = tracks.Dequeue();
            previous.Stop(fadeSeconds);
            tracks.Enqueue(previous);

            if (wait) yield return new WaitUntil(() => previous.status is Status.Idle);
            
            var track = tracks.Peek();
            track.Play(music, fadeSeconds);
        }
    }
}