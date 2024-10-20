using UnityEngine;

namespace Incantium.Audio
{
    public sealed class AudioInput : ScriptableObject
    {
        internal AudioPlayer player;
        
        /// <summary>
        /// Method to start playing the audio clip already set in the audio player.
        /// </summary>
        /// <param name="fadeSeconds">The amount of seconds to fade in and out between audio clips. Setting this to 0
        /// will mean instant transition.</param>
        /// <param name="type">How to fade the audio clip between each other.</param>
        /// <seealso cref="Stop"/>
        public void Play(float fadeSeconds, FadeType type) => player?.Play(fadeSeconds, type);
        
        /// <summary>
        /// Method to start playing a new audio clip.
        /// </summary>
        /// <param name="music">The audio clip to play.</param>
        /// <param name="fadeSeconds">The amount of seconds to fade in and out between audio clips. Setting this to 0
        /// will mean instant transition.</param>
        /// <param name="type">How to fade the audio clip between each other.</param>
        /// <seealso cref="Stop"/>
        public void Play(MusicClip music, float fadeSeconds, FadeType type) => player?.Play(music, fadeSeconds, type);

        /// <summary>
        /// Method to play the audio clip as a sound effect once in the background.
        /// </summary>
        /// <param name="music">The audio clip to play.</param>
        public void PlaySFX(MusicClip music) => player?.PlaySFX(music);
        
        /// <summary>
        /// Method to stop the audio player.
        /// </summary>
        /// <param name="fadeSeconds">The amount of seconds to fade out the audio player.</param>
        /// <seealso cref="Play(float,Incantium.Audio.FadeType)"/>
        public void Stop(float fadeSeconds = 0) => player?.Stop(fadeSeconds);

        /// <summary>
        /// Method to pause the audio player.
        /// </summary>
        /// <seealso cref="Resume"/>
        public void Pause() => player?.Pause();

        /// <summary>
        /// Method to resume the audio player.
        /// </summary>
        /// <seealso cref="Pause"/>
        public void Resume() => player?.Resume();
    }
}