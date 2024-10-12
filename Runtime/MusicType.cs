namespace Incantium.Audio
{
    /// <summary>
    /// Types of audio clips. These types dictate how the audio clip should be played with special regards to looping.
    /// </summary>
    /// <since>0.1.0</since>
    public enum MusicType
    {
        /// <summary>
        /// Uses a defined start and end boundary in the audio clip to seamlessly loop the main section.
        /// </summary>
        /// <since>0.1.0</since>
        Smart,
        
        /// <summary>
        /// Plays the entire audio clip in a loop.
        /// </summary>
        /// <since>0.1.0</since>
        Loop,
        
        /// <summary>
        /// Plays the audio clip in its entirely once.
        /// </summary>
        /// <since>0.1.0</since>
        Once
    }
}