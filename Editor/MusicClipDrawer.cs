using UnityEditor;
using UnityEngine;

namespace Incantium.Audio.Editor
{
    [CustomEditor(typeof(MusicClip))]
    public class MusicClipDrawer : UnityEditor.Editor
    {
        private MusicClip musicClip;

        private void OnEnable()
        {
            musicClip = target as MusicClip;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
            
            DrawAudioSection();
            DrawTypeSection();
            
            EditorGUI.EndDisabledGroup();
            
            DrawButtonSection();
            
            if (!EditorGUI.EndChangeCheck()) return;

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAudioSection()
        {
            EditorGUILayout.LabelField("Audio", EditorStyles.boldLabel);
            DrawClipField();
            DrawVolumeField();
            DrawPitchField();
            EditorGUILayout.Space();
        }

        private void DrawClipField()
        {
            var clip = serializedObject.FindProperty("clip");
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(clip);
            EditorGUI.EndDisabledGroup();
            
            if (clip.objectReferenceValue) return;
            
            EditorGUILayout.HelpBox("Missing required audio clip.", MessageType.Warning);
        }

        private void DrawVolumeField()
        {
            var volume = serializedObject.FindProperty("volume");
            EditorGUILayout.PropertyField(volume);
        }

        private void DrawPitchField()
        {
            var pitch = serializedObject.FindProperty("pitch");
            EditorGUILayout.PropertyField(pitch);
        }

        private void DrawTypeSection()
        {
            EditorGUILayout.LabelField("Type", EditorStyles.boldLabel);
            DrawTypeField();
            DrawStartEndFields();
            EditorGUILayout.Space();
        }

        private void DrawTypeField()
        {
            var type = serializedObject.FindProperty("type");
            EditorGUILayout.PropertyField(type, GUIContent.none);
        }

        private void DrawStartEndFields()
        {
            var type = (MusicType) serializedObject.FindProperty("type").boxedValue;
            var clip = serializedObject.FindProperty("clip").objectReferenceValue as AudioClip;
            
            if (type is not MusicType.Smart || !clip) return;
            
            var start = serializedObject.FindProperty("start");
            var end = serializedObject.FindProperty("end");

            var minVal = start.floatValue;
            var maxVal = end.floatValue;
            
            EditorGUILayout.Space();
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, 0, clip.length);
            if (EditorGUI.EndChangeCheck())
            {
                start.floatValue = minVal;
                end.floatValue = maxVal;
            }
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(start, GUIContent.none);
            EditorGUILayout.PropertyField(end, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawButtonSection()
        {
            if (!EditorApplication.isPlaying) return;
            if (!GUILayout.Button(Styles.BUTTON) || !AudioPlayer.instance) return;
            
            AudioPlayer.instance.Play(musicClip, 2f);
        }
        
        private static class Styles
        {
            public static readonly GUIContent BUTTON = new("Play", "Play the music clip.");
        }
    }
}