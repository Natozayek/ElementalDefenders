using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(TreeGenerator))]
public class TreeGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Tree"))
        {
            TreeGenerator myTarget = (TreeGenerator)target;
            myTarget.Generate();
            //myTarget.AddBoxColliderToChildren();
        }
    }
}
#endif