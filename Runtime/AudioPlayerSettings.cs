using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Incantium.Audio
{
    internal sealed class AudioPlayerSettings : ScriptableObject
    {
        private const string PATH = "Assets/Settings/Resources/AudioPlayerSettings.asset";
        
        [SerializeField] 
        internal bool instantiateAtStartup = true;

        [SerializeField]
        internal bool playAtAwake;

        [SerializeField]
        internal MusicClip startMusic;
        
        internal static AudioPlayerSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<AudioPlayerSettings>("AudioPlayerSettings");

            if (settings) return settings;
            
#if UNITY_EDITOR
            settings = CreateInstance<AudioPlayerSettings>();
                
            AssetDatabase.CreateAsset(settings, PATH);
            AssetDatabase.SaveAssets();

            return settings;
#else
            return null;
#endif
        }
    }
}