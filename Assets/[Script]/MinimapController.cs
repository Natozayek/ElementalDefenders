using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] GameObject miniMap;
  
    private void Awake()
    {
        miniMap = GameObject.Find("MiniMap");
    }
    public  void ToggleMinimap()
    {
        miniMap.SetActive((!miniMap.activeInHierarchy));
    }
}
