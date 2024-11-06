using System;
using UnityEditor;
using UnityEngine;

namespace Incantium.Audio.Editor
{
    public static class MusicClipCreator
    {
        [MenuItem("Assets/Create/Data/Music Clip", true)]
        private static bool ValidateCreateMusicClip()
        {
            return Selection.objects.Length == 1 && Selection.activeObject is AudioClip;
        }
        
        [MenuItem("Assets/Create/Data/Music Clip")]
        private static void CreateMusicClip()
        {
            if (Selection.activeObject is not AudioClip selectedClip) return;
            
            var musicClip = ScriptableObject.CreateInstance<MusicClip>();
            musicClip.clip = selectedClip;
            
            var fullPath = EditorUtility.SaveFilePanel("Create Music Clip", "", "", "asset");
            var path = fullPath[fullPath.IndexOf("Assets", StringComparison.Ordinal)..];
            
            AssetDatabase.CreateAsset(musicClip, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = musicClip;
        }
    }
}