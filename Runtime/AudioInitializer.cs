using UnityEngine;

namespace Incantium.Audio
{
    public static class AudioInitializer
    {
        [RuntimeInitializeOnLoadMethod]
        private static void OnStartup()
        {
            var settings = Resources.Load<AudioPlayerSettings>("AudioPlayerSettings");
            
            if (!settings.instantiateAtStartup) return;

            var audio = new GameObject("Audio");
            Object.DontDestroyOnLoad(audio);
            
            var player = audio.AddComponent<AudioPlayer>();
            player.playOnAwake = settings.playAtAwake;
            player.music = settings.startMusic;
        }
    }
}