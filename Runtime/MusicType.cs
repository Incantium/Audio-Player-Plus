namespace Incantium.Audio
{
    /// <summary>
    /// Types of audio clips. These types dictate how the audio clip should be played with special regards to looping.
    /// </summary>
    public enum MusicType
    {
        /// <summary>
        /// Uses a defined start and end boundary in the audio clip to seamlessly loop the main section. Use this type
        /// for advanced audio clips with a distinct middle section that loops on its own.
        /// </summary>
        Smart,
        
        /// <summary>
        /// Plays the entire audio clip in a loop. Use this type for general audio clips.
        /// </summary>
        Loop,
        
        /// <summary>
        /// Plays the audio clip in its entirety once. Use this type for sound effect audio clips.
        /// </summary>
        Once
    }
}