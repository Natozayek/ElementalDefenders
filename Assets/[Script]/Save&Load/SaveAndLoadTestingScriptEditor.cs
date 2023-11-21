#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveAndLoadTestingScript))]
public class SaveAndLoadTestingScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Save"))
        {
            SaveAndLoadTestingScript myTarget = (SaveAndLoadTestingScript)target;
            SaveManager.Save(myTarget.Level);
            Debug.Log("Saved");
        }

        if (GUILayout.Button("Reset"))
        {
            //SaveAndLoadTestingScript myTarget = (SaveAndLoadTestingScript)target;
            SaveManager.Save(0);
            Debug.Log("Reseted");
        }
    }
}

#endif