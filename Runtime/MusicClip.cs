using UnityEngine;

namespace Incantium.Audio
{
    /// <summary>
    /// Class that enables the additional information required for seamlessly looping audio clips.
    /// </summary>
    /// <seealso href="https://github.com/Incantium/Audio-Player-Plus/blob/main/Documentation~/MusicClip.md">MusicClip
    /// </seealso>
    public sealed class MusicClip : ScriptableObject
    {
        /// <summary>
        /// The audio clip, altered by volume, pitch, and smart looping.
        /// </summary>
        [SerializeField]
        [Tooltip("The audio clip.")]
        public AudioClip clip;
        
        /// <summary>
        /// The type of audio clip. The following types are available:
        /// <ul>
        ///     <li><see cref="MusicType.Smart"/>: <inheritdoc cref="MusicType.Smart"/></li>
        ///     <li><see cref="MusicType.Loop"/>: <inheritdoc cref="MusicType.Loop"/></li>
        ///     <li><see cref="MusicType.Once"/>: <inheritdoc cref="MusicType.Once"/></li>
        /// </ul>
        /// </summary>
        [SerializeField]
        [Tooltip("The type of audio clip.")]
        public MusicType type = MusicType.Loop;
        
        /// <summary>
        /// The maximum volume of the audio clip after fading in. 
        /// </summary>
        [SerializeField]
        [Tooltip("The volume of the audio clip.")]
        [Range(0, 1)]
        public float volume = 1;
        
        /// <summary>
        /// The pitch of the audio clip, which influences the total length of the audio clip. This in turn will speed
        /// up/down the audio clip.
        /// </summary>
        [SerializeField]
        [Tooltip("The pitch or speed of the audio clip.")]
        [Range(-3, 3)]
        public float pitch = 1;
        
        /// <summary>
        /// The start boundary of the main loop in seconds.
        /// </summary>
        [SerializeField]
        [Tooltip("The start boundary of the main loop in seconds.")]
        [Min(0)]
        public float start;
        
        /// <summary>
        /// The end boundary of the main loop in seconds.
        /// </summary>
        [SerializeField]
        [Tooltip("The end boundary of the main loop in seconds.")] 
        [Min(0)]
        public float end;
        
        /// <summary>
        /// The length of the main section of the audio clip, excluding the intro and outro section.
        /// </summary>
        /// <remarks>This value does not take into account the scaling of pitch.</remarks>
        /// <seealso cref="main"/>
        internal float unscaledMain => end - start;
        
        /// <summary>
        /// The total length of the clip in seconds.
        /// </summary>
        /// <remarks>This value takes into account the scaling of the pitch, which shortens the original
        /// <see cref="AudioClip.length"/>.</remarks>
        internal float length => clip.length / pitch;
        
        /// <summary>
        /// The length of the intro section of the audio clip.
        /// </summary>
        /// <remarks>This value takes into account the scaling of the pitch, which shortens the original
        /// <see cref="AudioClip.length"/>.</remarks>
        internal float intro => start / pitch;
        
        /// <summary>
        /// The length of the main section of the audio clip, excluding the intro and outro section.
        /// </summary>
        /// <remarks>This value takes into account the scaling of the pitch, which shortens the original
        /// <see cref="AudioClip.length"/>.</remarks>
        /// <seealso cref="unscaledMain"/>
        internal float main => unscaledMain / pitch;

        /// <summary>
        /// The length of the outro section of the audio clip.
        /// </summary>
        /// <remarks>This value takes into account the scaling of the pitch, which shortens the original
        /// <see cref="AudioClip.length"/>.</remarks>
        internal float outro => (clip.length - end) / pitch;

        /// <summary>
        /// Method to start playing a new audio clip as on its <see cref="MusicClip.type"/>.
        /// </summary>
        /// <param name="seconds">The amount of seconds to fade in and out between audio clips. Setting this to 0
        /// will mean instant transition.</param>
        /// <param name="type">How to fade the audio clip between each other.</param>
        public void Play(float seconds = 0f, FadeType type = FadeType.CrossFade) 
            => AudioPlayer.instance.Play(this, seconds, type);
    }
}