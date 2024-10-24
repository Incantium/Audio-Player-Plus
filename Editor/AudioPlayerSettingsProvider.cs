using UnityEditor;
using UnityEngine.UIElements;

namespace Incantium.Audio.Editor
{
    public class AudioPlayerSettingsProvider : SettingsProvider
    {
        private const string PROJECT_PATH = "Project/Audio/Audio Player Plus";
        private const SettingsScope SCOPE = SettingsScope.Project;

        private SerializedObject settings;
        
        [SettingsProvider]
        private static SettingsProvider CreateProvider() => new AudioPlayerSettingsProvider();
        
        public AudioPlayerSettingsProvider() : base(PROJECT_PATH, SCOPE) {}

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            settings = new SerializedObject(AudioPlayerSettings.instance);
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(settings.FindProperty("instantiateAtStartup"));
            
            var initialize = settings.FindProperty("instantiateAtStartup").boolValue;

            if (initialize)
            {
                EditorGUILayout.PropertyField(settings.FindProperty("music"));
            }
            
            if (!EditorGUI.EndChangeCheck()) return;

            settings.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}