using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneObjectScript : MonoBehaviour
{
    private List<Collider> addedCollider = new List<Collider>();

    public void AddBoxColliderToChildren()
    {
        var children = GetComponentsInChildren<Transform>();

        foreach (var child in children)
        {
            if (!child.TryGetComponent<Collider>(out var collider))
            {
                if (child.TryGetComponent<MeshFilter>(out var Mesh))
                {
                    var childCollider = child.gameObject.AddComponent<MeshCollider>();
                    addedCollider.Add(childCollider);
                }

                if (child.TryGetComponent<SkinnedMeshRenderer>(out var SkinnedMesh))
                {
                    var childCollider = child.gameObject.AddComponent<BoxCollider>();
                    addedCollider.Add(childCollider);
                }
            }
        }
    }

    public void RemoveColliderFromChildren()
    {
        foreach(var collider in addedCollider)
        {
            DestroyImmediate(collider);
        }
        addedCollider.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
