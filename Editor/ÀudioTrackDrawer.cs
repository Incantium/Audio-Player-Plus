﻿using UnityEditor;

namespace Incantium.Audio.Editor
{
    [CustomEditor(typeof(AudioTrack))]
    public class ÀudioTrackDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);

            var status = serializedObject.FindProperty("_status");
            EditorGUILayout.PropertyField(status);
            
            EditorGUI.EndDisabledGroup();
        }
    }
}