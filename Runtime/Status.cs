namespace Incantium.Audio
{
    /// <summary>
    /// The internal status of the audio track.
    /// </summary>
    /// <seealso cref="AudioTrack"/>
    /// <since>0.1.0</since>
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
        /// The audio track is playing a audio clip at its desired volume.
        /// </summary>
        Playing,
        
        /// <summary>
        /// The audio track is decreasing its volume and will soon stop.
        /// </summary>
        Stopping
    }
}