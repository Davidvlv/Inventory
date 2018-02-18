using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScreenResizer))]
public class ScreenResizerEditor : Editor {
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ScreenResizer myScript = (ScreenResizer)target;
        if (GUILayout.Button("Resize"))
        {
            myScript.Resize();
        }
    }
}
