
using System;
using PCG.RoomData;
using UnityEditor;
using UnityEngine;

namespace PCG.Editor
{
    [CustomEditor(typeof(BaseRoom))]
    public class RoomLightingDataEditor : UnityEditor.Editor
    {
        private BaseRoom _room;
        private bool[] _roomLightingData;
        
        private void OnEnable()
        {
            _room = (BaseRoom)target;
            _roomLightingData = _room.lightGrid;
        }
        
        public override void OnInspectorGUI()
        {
            // Let Unity draw default fields for width and height
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("width"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("height"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("doorX"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("doorZ"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("doorDirection"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("roomObjects"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("roomType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lightIntensity"));
            serializedObject.ApplyModifiedProperties();

            var width = _room.width;
            var height = _room.height;

            if (_roomLightingData == null || _roomLightingData.Length != width * height)
            {
                Undo.RecordObject(_room, "Resize Grid");
                _room.ResizeLightingGrid(width, height);
            }
            
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Lighting Data Grid", EditorStyles.boldLabel);

            for (var z = 0; z < height; z++)
            {
                EditorGUILayout.BeginHorizontal();
                for (var x = 0; x < width; x++)
                {
                    var currentValue = _room.GetLightingGridValue(x, z);

                    var originalColor = GUI.backgroundColor;
                    GUI.backgroundColor = currentValue ? Color.green : Color.red;

                    var newValue = GUILayout.Toggle(currentValue,
                        "",
                        "Button",
                        GUILayout.Width(25),
                        GUILayout.Height(25)
                        );

                    GUI.backgroundColor = originalColor;

                    if (newValue == currentValue) continue;
                    
                    Undo.RecordObject(_room, "Toggle Grid Value");
                    _room.SetLightingGridValue(x, z, newValue);
                    EditorUtility.SetDirty(_room);
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
