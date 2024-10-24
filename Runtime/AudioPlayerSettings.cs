using UnityEngine;

namespace Incantium.Audio
{
    internal sealed class AudioPlayerSettings : ScriptableObject
    {
        [SerializeField] 
        internal bool instantiateAtStartup = true;

        [SerializeField]
        internal bool playAtAwake;

        [SerializeField]
        internal MusicClip startMusic;
    }
}