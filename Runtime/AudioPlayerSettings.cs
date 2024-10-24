using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Incantium.Audio
{
    /// <summary>
    /// Class representing the audio player settings data visible in the Project Settings under
    /// "Audio -> Audio Player Plus". 
    /// </summary>
    /// <seealso href="https://github.com/Incantium/Audio-Player-Plus/blob/main/Documentation~/AudioPlayerSettings.md">
    /// AudioPlayerSettings</seealso>
    internal sealed class AudioPlayerSettings : ScriptableObject
    {
        /// <summary>
        /// The path of the settings within a project.
        /// </summary>
        private const string PATH = "Assets/Settings/Resources/AudioPlayerSettings.asset";
        
        /// <summary>
        /// The settings within the project.
        /// </summary>
        internal static AudioPlayerSettings instance => GetOrCreateSettings();
        
        [SerializeField] 
        [Tooltip("Whether to instantiate a new audio player at the start of the game.")]
        internal bool instantiateAtStartup = true;

        [SerializeField]
        [Tooltip("The audio clip to play at awake.")]
        internal MusicClip music;
        
        /// <summary>
        /// Method to get the audio player settings from within the project. If there are no settings found, this method
        /// will create a new instance of the settings if able to do so.
        /// </summary>
        /// <returns>The audio player settings.</returns>
        private static AudioPlayerSettings GetOrCreateSettings()
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