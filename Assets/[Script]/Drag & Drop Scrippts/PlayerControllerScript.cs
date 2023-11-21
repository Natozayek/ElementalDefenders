using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Rendering;

public class PlayerControllerScript : MonoBehaviour
{
    public static UnityAction<TowerScript> EndDragEvent;
    public static UnityAction<TowerScript> StartDragEvent;
    public static UnityAction CancelDragEvent;

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private Vector2 movePosition;

    [SerializeField]
    private bool isBeingPress = false;

    [SerializeField]
    private Vector3 placePos;

    [SerializeField]
    private Vector3Int currentCellPos;


    // It would be a better idea to have this as another script
    // but that's too much work
    [FormerlySerializedAs("Placement")]
    [SerializeField]
    private GameObject placementIndicator;
    private Shader placementShader;
    private Renderer placementIndicatorRender;
    private LocalKeyword shaderKeyWord;
    [SerializeField]
    private bool isVaildPlacement = false;

    //
    public ActionMenuManager actionMenuManager;


    private void Awake()
    {
        //checkDelay = 60 / checkFrequencyPerSecond;
        EndDragEvent = PlaceTower;
        StartDragEvent = ShowPlacement;
        CancelDragEvent = CancelPlacement;

        placementShader = Resources.Load<Shader>("Shader/PlacementIndicatorShader");
        
    }

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager == null)
        {
            Debug.LogWarning("Warning: Player manager not found.");
        }

        if (actionMenuManager == null)
        {
            actionMenuManager = FindObjectOfType<ActionMenuManager>();
        }

        if(placementIndicator)
        {
            placementIndicatorRender = placementIndicator.GetComponent<Renderer>();
            placementIndicatorRender.material.shader = placementShader;
            shaderKeyWord = new LocalKeyword(placementIndicatorRender.material.shader, "_ISVAILDPLACEMENT");
        }
    }

    private void OnDestroy()
    {
        EndDragEvent = null;
        StartDragEvent = null;
        CancelDragEvent = null;
    }

    private IEnumerator CheckForTile(TowerScript towerRef)
    {
        if(towerRef.TryGetComponent<MeshFilter>(out var meshComp))
        {
            placementIndicator.GetComponent<MeshFilter>().sharedMesh = meshComp.sharedMesh;
        }

        placementIndicator.transform.position = new Vector3(0.0f, -100.0f, 0.0f);
        placementIndicator.SetActive(isBeingPress);

        while (isBeingPress)
        {
            // If this is with the other camera I wont need this
            var pos = Camera.main.ScreenPointToRay(movePosition);

            // Look, no physics!
            var distance = Camera.main.transform.position.y / pos.direction.y;
            var newPos = Camera.main.transform.position + (pos.direction * -distance);

            // Snap the position to grid
            currentCellPos = TowerPlacableArea.tileMap.WorldToCell(newPos);
            var indicatorPosition = TowerPlacableArea.tileMap.CellToWorld(currentCellPos) + new Vector3(0.5f, 0.0f, 0.5f);

            // Indicator
            placementIndicator.transform.position = indicatorPosition;
            placementIndicatorRender.material.DisableKeyword(shaderKeyWord);
            isVaildPlacement = false;

            if (!TowerPlacableArea.unplacableTiles.Contains(currentCellPos) &&
                !TowerPlacableArea.towerPos.Contains(currentCellPos))
            {
                placementIndicatorRender.material.EnableKeyword(shaderKeyWord);
                placePos = indicatorPosition;
                placePos.y += 0.2f;
                isVaildPlacement = true;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
        //placementIndicator.SetActive(isBeingPress);
    }


    public void ShowPlacement(TowerScript towerRef)
    {
        isBeingPress = true;
        StartCoroutine(CheckForTile(towerRef));
    }

    public void PlaceTower(TowerScript towerRef)
    {
        StopCoroutine("CheckForTile");
        isBeingPress = false;
        placementIndicator.SetActive(false);
        if (actionMenuManager.buyOnConstruct)
        {
            if (isVaildPlacement && playerManager.Gold - towerRef.TowerCost >= 0 && playerManager.Gems > 0)
            {
                playerManager.ModifyGold(towerRef.TowerCost * -1);
                playerManager.ModifyGems(-1);
                var Ref = Instantiate(towerRef.gameObject);
                Ref.transform.position = placePos;
                Ref.GetComponent<TowerScript>().isBuild = true;
                Ref.SetActive(true);
                //TowerPlacableArea.towerPos.Add(TowerPlacableArea.tileMap.WorldToCell(placePos));
                TowerPlacableArea.towerPos.Add(currentCellPos);
            }
        }
        else
        {
            if (isVaildPlacement && playerManager.Gold - towerRef.TowerCost >= 0)
            {
                playerManager.ModifyGold(towerRef.TowerCost * -1);
                var Ref = Instantiate(towerRef.gameObject);
                Ref.transform.position = placePos;
                Ref.GetComponent<TowerScript>().isBuild = false;
                Ref.SetActive(true);
                //TowerPlacableArea.towerPos.Add(TowerPlacableArea.tileMap.WorldToCell(placePos));
                TowerPlacableArea.towerPos.Add(currentCellPos);
            }
        }

        //OLD
        //if (isVaildPlacement && playerManager.Gold - towerRef.TowerCost >= 0)
        //{
        //    playerManager.ModifyGold(towerRef.TowerCost * -1);
        //    var Ref = Instantiate(towerRef.gameObject);
        //    Ref.transform.position = placePos;
        //    if(actionMenuManager.buyOnConstruct)
        //    {
        //        Ref.GetComponent<TowerScript>().isBuild = true;
        //    }
        //    else
        //    {
        //        Ref.GetComponent<TowerScript>().isBuild = false;
        //    }
        //    Ref.SetActive(true);
        //    //TowerPlacableArea.towerPos.Add(TowerPlacableArea.tileMap.WorldToCell(placePos));
        //    TowerPlacableArea.towerPos.Add(currentCellPos);
        //}
    }

    public void CancelPlacement()
    {
        StopCoroutine("CheckForTile");
        isBeingPress = false;
        placementIndicator.SetActive(false);
    }

    public void OnMovePosition(InputAction.CallbackContext ctx)
    {
        movePosition = ctx.ReadValue<Vector2>();
    }
}
