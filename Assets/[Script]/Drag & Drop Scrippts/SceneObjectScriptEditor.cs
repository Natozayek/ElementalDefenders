using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR
[CustomEditor(typeof(SceneObjectScript))]
public class SceneObjectScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Add Colliders"))
        {
            SceneObjectScript myTarget = (SceneObjectScript)target;
            myTarget.AddBoxColliderToChildren();
        }

        if (GUILayout.Button("Remove Colliders"))
        {
            SceneObjectScript myTarget = (SceneObjectScript)target;
            myTarget.RemoveColliderFromChildren();
        }
    }
}
#endif
