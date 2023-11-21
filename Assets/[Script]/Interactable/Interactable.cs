using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
abstract public class Interactable : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnInteraction();
    }

    abstract protected void OnInteraction();

    protected void Awake()
    {
        var collider = GetComponent<Collider>();
        if (collider == null)
        {
            this.gameObject.AddComponent<MeshCollider>();
        }
        
        this.gameObject.layer = 13;
    }
}
