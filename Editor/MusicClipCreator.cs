using System.IO;
using UnityEditor;
using UnityEngine;

namespace Incantium.Audio.Editor
{
    public static class MusicClipCreator
    {
        [MenuItem("Assets/Create/Audio/Music Clip", true)]
        private static bool ValidateCreateMusicClip()
        {
            return Selection.objects.Length == 1 && Selection.activeObject is AudioClip;
        }
        
        [MenuItem("Assets/Create/Audio/Music Clip")]
        private static void CreateMusicClip()
        {
            if (Selection.activeObject is not AudioClip selectedClip) return;
            
            var selectedPath = AssetDatabase.GetAssetPath(selectedClip);
            var directory = Path.GetDirectoryName(selectedPath);
            var name = Path.GetFileNameWithoutExtension(selectedPath);
            
            var musicClip = ScriptableObject.CreateInstance<MusicClip>();
            musicClip.clip = selectedClip;

            if (directory == null) return;            
            
            var musicClipPath = Path.Combine(directory, name + ".asset");
            
            AssetDatabase.CreateAsset(musicClip, musicClipPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = musicClip;
        }
    }
}