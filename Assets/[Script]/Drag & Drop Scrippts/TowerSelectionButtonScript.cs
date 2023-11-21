using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TowerSelectionButtonScript : Button, IDragHandler, IBeginDragHandler, 
    IEndDragHandler, IPointerEnterHandler ,IPointerExitHandler
{
    private bool isPointerInside = false;

    static private TowerScript currentTowerRef = null;
    public TowerScript towerRef;
    public TMP_Text nameTextRef;
    public TMP_Text costTextRef;

    protected override void Awake()
    {
        nameTextRef.text = towerRef.name;
        costTextRef.text = towerRef.TowerCost.ToString();
        PlayerManager.UpdateMoney.AddListener(SetCanBeUse);
    }

    protected override void OnDestroy()
    {
        PlayerManager.UpdateMoney.RemoveListener(SetCanBeUse);
    }

    private void SetCanBeUse(float amount)
    {
        this.interactable = (towerRef.TowerCost <= amount);
    }

    // This function is not really need
    // But IDragHandler needs this to be here
    // and IBeginDragHandler and IEndDragHandler needs IDragHandler
    public void OnDrag(PointerEventData data){}

    public void OnBeginDrag(PointerEventData data)
    {
        if (PlayerControllerScript.StartDragEvent != null && interactable && !currentTowerRef)
        {
            PlayerControllerScript.StartDragEvent.Invoke(towerRef);
            currentTowerRef = towerRef;
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (PlayerControllerScript.EndDragEvent != null && !isPointerInside && interactable)
        {
            PlayerControllerScript.EndDragEvent.Invoke(towerRef);
            currentTowerRef = null;
        }
    }

    override public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isPointerInside = true;
        if (PlayerControllerScript.CancelDragEvent != null && interactable)
            PlayerControllerScript.CancelDragEvent.Invoke();
    }

    override public void OnPointerExit(PointerEventData pointerEventData)
    {
        isPointerInside = false;

        if (PlayerControllerScript.StartDragEvent != null && interactable && currentTowerRef)
        {
            PlayerControllerScript.StartDragEvent.Invoke(currentTowerRef);
        }
    }
}
