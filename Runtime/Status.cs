namespace Incantium.Audio
{
    /// <summary>
    /// The internal status of an <see cref="AudioTrack"/>.
    /// </summary>
    internal enum Status
    {
        /// <summary>
        /// The audio track is not playing.
        /// </summary>
        Idle,
        
        /// <summary>
        /// The audio track is increasing its volume and will soon play an audio clip in full.
        /// </summary>
        Starting,
        
        /// <summary>
        /// The audio track is playing an audio clip at its desired volume.
        /// </summary>
        Playing,
        
        /// <summary>
        /// The audio track is decreasing its volume and will soon stop.
        /// </summary>
        Stopping
    }
}