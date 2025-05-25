using Items;
using UnityEditor;
using UnityEngine;

namespace Util.Editor
{
    [CustomEditor(typeof(DebugUtil))]
    public class DebugUtilEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Add Item"))
            {
                DebugUtil.Instance.AddItemToPlayer(
                    serializedObject.FindProperty("itemToAdd").objectReferenceValue as InventoryItemData,
                    1);
            }
        }
    }
}