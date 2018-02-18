using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Inventory myScript = (Inventory)target;

        if (GUILayout.Button("Create UI"))
        {
            myScript.CreateUI();
        }
    }
}
