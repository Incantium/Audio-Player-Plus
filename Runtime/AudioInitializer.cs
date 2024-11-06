using UnityEngine;

namespace Incantium.Audio
{
    /// <summary>
    /// Class handling the initialization of the <see cref="AudioPlayer"/> at the start of the game. The
    /// <see cref="AudioPlayerSettings"/> determine how the audio player should be instantiated.
    /// </summary>
    internal static class AudioInitializer
    {
        /// <summary>
        /// Method called before the first game objects are loaded in the game. This method will instantiate a new
        /// <see cref="AudioPlayer"/> if the <see cref="AudioPlayerSettings"/> say so.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnStartup()
        {
            var settings = AudioPlayerSettings.instance;
            
            if (!settings || !settings.instantiateAtStartup) return;

            var audio = new GameObject("Audio");
            audio.AddComponent<AudioPlayer>();
            
            if (settings.music) settings.music.Play();
            
            Object.DontDestroyOnLoad(audio);
        }
    }
}