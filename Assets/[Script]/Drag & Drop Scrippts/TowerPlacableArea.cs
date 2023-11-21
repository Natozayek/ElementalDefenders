using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEditor;
public class TowerPlacableArea : MonoBehaviour
{
    [SerializeField]
    static public List<Vector3Int> unplacableTiles = new List<Vector3Int>();
    static public List<Vector3Int> towerPos = new List<Vector3Int>();

    static public Tilemap tileMap;

    // Load level data that has the same name as the scene
    // If there isn't any display a warning
    private void Awake()
    {
        string path = string.Format("LevelData/{0}", SceneManager.GetActiveScene().name);
        PlacableTileData data = Resources.Load<PlacableTileData>(path);
        towerPos = new List<Vector3Int>();
        if (data)
        {
            unplacableTiles = data.unplacableTiles;
        }
        else
        {
            Debug.LogWarning("Warning: no level data found. Please build level data before running the game");
        }

        tileMap = GetComponent<Tilemap>();
    }
}
