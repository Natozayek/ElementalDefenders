using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
[CustomEditor(typeof(TowerPlacableArea))]
public class TowerPlacableAreaEditor : Editor
{
    //public List<GameObject> unplacealbeTileAdded;


    // Oh god this is too over engineer
    // Why did I decided to do this
    // This add the collider to every child of object with SceneObjectScript
    // Then it call for the function SetPlacableArea which require colliders to work
    // Then it remove the collider added from the children
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Build"))
        {
            var sceneObjects = GameObject.FindObjectsOfType<SceneObjectScript>();
            if (sceneObjects != null)
            {
                foreach(var i in sceneObjects)
                    i.AddBoxColliderToChildren();


                SetPlacableArea();

                foreach (var i2 in sceneObjects)
                    i2.RemoveColliderFromChildren();
            }
        }
    }

    // Detect any collider above it's 3d tiles and tiles with RoadScript 
    // If there are any it will add them to a list 
    // Then the list will be store in a scriptable object with the same name as the level
    // This function can be use in the inspector using the build button
    // That is to avoid running this function everytime the game starts
    public void SetPlacableArea()
    {
        if (Application.isEditor)
        {
            TowerPlacableArea myTarget = (TowerPlacableArea)target;

            var tileMapRef = myTarget.GetComponent<Tilemap>();
            var children = myTarget.GetComponentsInChildren<Transform>();
            List<Vector3Int> tempRef = new List<Vector3Int>();

            var roadScript = GameObject.FindObjectsOfType<RoadScript>();
            foreach(var road in roadScript)
            {
                tempRef.Add(tileMapRef.WorldToCell(road.transform.position));
            }

            foreach (var child in children)
            {
                if (myTarget.transform != child.transform && child.gameObject.TryGetComponent<MeshFilter>(out var filter))
                {
                    var mesh = filter.sharedMesh;
                    //child.gameObject.SetActive(false);
                    if (Physics.BoxCast(child.position, mesh.bounds.size / 2, Vector3.up))
                    {
                        Vector2Int.CeilToInt(child.position);
                        tempRef.Add(tileMapRef.WorldToCell(child.position));

                    }
                }
            }

            //tempRef.Sort();
            var data = ScriptableObject.CreateInstance<PlacableTileData>();
            data.unplacableTiles = new List<Vector3Int>(tempRef);
            string path = string.Format("Assets/Resources/LevelData/{0}.asset", SceneManager.GetActiveScene().name);
            AssetDatabase.CreateAsset(data, path);
            tempRef.Clear();
        }
    }


}
#endif