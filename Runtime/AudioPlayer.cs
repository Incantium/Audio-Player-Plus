using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Incantium.Audio
{
    /// <summary>
    /// Class handling the creation of different audio tracks that can fade audio between each other.
    /// </summary>
    /// <seealso href="https://github.com/Incantium/Audio-Player-Plus/blob/main/Documentation~/AudioPlayer.md">
    /// AudioPlayer</seealso>
    [RequireComponent(typeof(AudioSource))]
    [HelpURL("https://github.com/Incantium/Audio-Player-Plus/blob/main/Documentation~/AudioPlayer.md")]
    public class AudioPlayer : SingleBehaviour<AudioPlayer>
    {
        private const int TRACK_AMOUNT = 2;
        
        [SerializeField]
        [Tooltip("Whether to play an audio clip at awake.")]
        internal bool playOnAwake;
        
        [SerializeField] 
        [Tooltip("The audio clip currently in play.")]
        internal MusicClip music;
        
        private readonly Queue<AudioTrack> tracks = new();
        private AudioSource sfx;
        
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
            
            Play(music);
        }
        
        /// <summary>
        /// Method to start playing a new audio clip as on its <see cref="MusicClip.type"/>.
        /// </summary>
        /// <param name="music">The audio clip to play.</param>
        /// <param name="seconds">The amount of seconds to fade in and out between audio clips. Setting this to 0
        /// will mean instant transition.</param>
        /// <param name="type">How to fade the audio clip between each other.</param>
        /// <seealso cref="Stop"/>
        public void Play([NotNull] MusicClip music, float seconds = 0f, FadeType type = FadeType.CrossFade)
        {
            if (!music || !music.clip) throw new ArgumentException("Missing audio clip.");
            if (music.type == MusicType.Once) sfx.PlayOneShot(music.clip, music.volume);
            else PlayLoop(music, seconds, type);
        }

        /// <summary>
        /// Method to stop the audio player.
        /// </summary>
        /// <param name="seconds">The amount of seconds to fade out the audio player.</param>
        /// <seealso cref="Play(MusicClip,float,FadeType)"/>
        public void Stop(float seconds = 0) => tracks.Peek().Stop(seconds);
        
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
        /// Method to start playing a new audio clip in a <see cref="MusicType.Loop"/> or <see cref="MusicType.Smart"/>.
        /// </summary>
        /// <param name="music">The audio clip to play.</param>
        /// <param name="seconds">The amount of seconds to fade in and out between audio clips. Setting this to 0
        /// will mean instant transition.</param>
        /// <param name="type">How to fade the audio clip between each other.</param>
        /// <seealso cref="Stop"/>
        private void PlayLoop(MusicClip music, float seconds = 0f, FadeType type = FadeType.CrossFade)
        {
            if (this.music == music) return;
            
            this.music = music;
            
            StopAllCoroutines();
            StartCoroutine(PlayRoutine(music, seconds, type));
        }
        
        /// <summary>
        /// Method to, firstly, load in the requested music clip before gracefully switching between.
        /// </summary>
        /// <param name="music">The audio clip to play.</param>
        /// <param name="seconds">The amount of seconds to fade in and out between audio clips. Setting this to 0
        /// will mean instant transition.</param>
        /// <param name="type">How to fade the audio clip between each other.</param>
        /// <seealso cref="Stop"/>
        private IEnumerator PlayRoutine(MusicClip music, float seconds, FadeType type)
        {
            yield return LoadClip(music);
            yield return Switch(seconds, type is FadeType.Wait);
        }
        
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
        /// <param name="seconds">The amount of seconds to fade in and out the music clips.</param>
        /// <param name="wait">True if this method should wait before the previously has stopped completely, otherwise
        /// false.</param>
        private IEnumerator Switch(float seconds, bool wait)
        {
            var previous = tracks.Dequeue();
            previous.Stop(seconds);
            tracks.Enqueue(previous);

            if (wait) yield return new WaitUntil(() => previous.status is Status.Idle);
            
            var track = tracks.Peek();
            track.Play(music, seconds);
        }
    }
}