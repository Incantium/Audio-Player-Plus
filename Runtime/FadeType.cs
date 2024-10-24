namespace Incantium.Audio
{
    /// <summary>
    /// Types of fading mechanism from one audio clip to another.
    /// </summary>
    /// <seealso href="https://github.com/Incantium/Audio-Player-Plus/blob/main/Documentation~/FadeType.md">FadeType
    /// </seealso>
    public enum FadeType
    {
        /// <summary>
        /// Waits until the last audio clip has faded out before fading in the new clip.
        /// </summary>
        Wait,
        
        /// <summary>
        /// Directly fading in the new audio clip while fading out the last clip.
        /// </summary>
        CrossFade
    }
}