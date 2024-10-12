namespace Incantium.Audio
{
    /// <summary>
    /// Types of fading mechanism from one audio clip to another.
    /// </summary>
    /// <since>0.1.0</since>
    public enum FadeType
    {
        /// <summary>
        /// Complete waits for the last audio clip has faded out before fading in the new clip.
        /// </summary>
        /// <since>0.1.0</since>
        Wait,
        
        /// <summary>
        /// Directly fading in the new audio clip while fading out the previous clip.
        /// </summary>
        /// <since>0.1.0</since>
        CrossFade
    }
}