﻿using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Incantium.Audio.Editor
{
    public class AudioPlayerSettingsProvider : SettingsProvider
    {
        private const string SETTINGS_PATH = "Project/Audio/Audio Player Plus";
        private const SettingsScope SCOPE = SettingsScope.Project;

        private SerializedObject settings;
        
        [SettingsProvider]
        private static SettingsProvider CreateProvider() => new AudioPlayerSettingsProvider();
        
        public AudioPlayerSettingsProvider() : base(SETTINGS_PATH, SCOPE) {}

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            var scriptableObject = Resources.Load<AudioPlayerSettings>("AudioPlayerSettings");
            settings = new SerializedObject(scriptableObject);
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(settings.FindProperty("instantiateAtStartup"));
            
            var initialize = settings.FindProperty("instantiateAtStartup").boolValue;

            if (initialize)
            {
                EditorGUILayout.PropertyField(settings.FindProperty("playAtAwake"));
                EditorGUILayout.PropertyField(settings.FindProperty("startMusic"));
            }
            
            if (!EditorGUI.EndChangeCheck()) return;

            settings.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}