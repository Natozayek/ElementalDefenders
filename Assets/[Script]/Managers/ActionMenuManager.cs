using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuManager : MonoBehaviour
{
    GameObject ActiveMenu = null; //Used to identify what menu is the active one   
    EnumActionMenuSubMenus ActiveMenuEnum; //for menu traveling
    public GameObject SelectedObject = null; //Reference to the current object the player is interacting
    PlayerManager playerManager;

    [Header("SubMenus")]
    public GameObject ConstructionMenu;
    public GameObject UpgradeMenu;
    public GameObject ConfirmMenu;
    public GameObject SellMenu;

    [Header("ConstructionRelated")]
    public List<GameObject> ListTower; //List of all Buildable towers, this and the towerIndex must be the same to build a tower 
    public Toggle buyToggle;
    public bool buyOnConstruct;

    [Header("UpgradeRelated")]
    public List<ConstructDataScript> UpgradeTowerOptions; //List of all Buildable towers, this and the towerIndex must be the same to build a tower 

    [Header("ConfirmMenu Related")]
    public Button BuyUpgradeButton;
    public Button BuildUpgradeButton;
    public GameObject ConfirmCancellButton;

    EnumTowerType TowerType = EnumTowerType.NONE;

    


    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        ExitActionMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(ConstructionMenu.gameObject.activeInHierarchy)
        {
            buyOnConstruct = buyToggle.isOn;
        }
    }

    //Active the Action menu with the corresponding Submenu
    public void ToggleMenu(EnumActionMenuSubMenus menu, GameObject targetObject)
    {
        SelectedObject = targetObject;
        switch (menu)
        {
            case EnumActionMenuSubMenus.CONSTRUCTIONMENU:
                ActiveMenu = ConstructionMenu;
                break;
            case EnumActionMenuSubMenus.UPGRADEMENU:
                ActiveMenu = UpgradeMenu;
                UpdateUpgradeData();
                break;
            case EnumActionMenuSubMenus.SELLMENU:
                ActiveMenu = SellMenu;
                break;
        }
        ActiveMenuEnum = menu;
        ConstructionMenu.SetActive(false); //Check This Later
        UpgradeMenu.SetActive(false);
        ConfirmMenu.SetActive(false);
        SellMenu.SetActive(false);
        ActiveMenu.SetActive(true);

    }

    //Activates the confirm Menu and its respectives Sub Menus
    public void ActiveConfirmMenu()
    {
        ConfirmMenu.SetActive(true);
        UpgradeMenu.SetActive(false);
        SellMenu.SetActive(false);
        UpdateInteractButtons();
    }

    //Set the index for the tower at the ConstructionZone Script
    public void SetTowerConstructionData(EnumTowerType tower)
    {
        TowerType = tower;
        ActiveConfirmMenu();
    }

    //Needed to get upgrade tower routes
    public void SetTowerUpgradeData(EnumTowerType tower)
    {
        TowerType = tower;
    }

    public void UpdateUpgradeData()
    {
        int index = 0;
        switch (TowerType)
        {
            case EnumTowerType.SHORTRANGETOWER:
                index = 3;
                break;
            case EnumTowerType.MIDRANGETOWER:
                index = 6;
                break;
            case EnumTowerType.LONGRANGETOWER:
                index = 9;
                break;
        }

        int z = 0;

        for (int i = index; i < index + 3; i++)
        {
            UpgradeTowerOptions[z].SetTowerType(i);
            z++;
        }
    }

    //Pay more but Construct the tower with no build time
    public void BuyTower()
    {
        Debug.Log("Buy");
        Transform currentPos = SelectedObject.transform;
        GameObject tower = Instantiate(ListTower[(int)TowerType], currentPos.position, currentPos.rotation);
        tower.GetComponent<TowerScript>().isBuild = true;
        Destroy(SelectedObject);
        SelectedObject = null;
        playerManager.ModifyGold(-ListTower[(int)TowerType].GetComponent<TowerScript>().TowerCost);
        playerManager.ModifyGems(-1);

        if (TowerType == EnumTowerType.SHORTFIRETOWER || TowerType == EnumTowerType.MIDFIRETOWER || TowerType == EnumTowerType.LONGFIRETOWER)
        {
            playerManager.AddElementalGem(0, -1);
        }

        if (TowerType == EnumTowerType.SHORTWATERTOWER || TowerType == EnumTowerType.MIDWATERTOWER || TowerType == EnumTowerType.LONGWATERTOWER)
        {
            playerManager.AddElementalGem(1, -1);
        }

        if (TowerType == EnumTowerType.SHORTEARTHTOWER || TowerType == EnumTowerType.MIDEARTHTOWER || TowerType == EnumTowerType.LONGEARTHTOWER)
        {
            playerManager.AddElementalGem(2, -1);
        }

        ExitConfirmMenu();
        ExitActionMenu();
    }

    //Cheaper but with Build time
    public void BuildTower()
    {
        Transform currentPos = SelectedObject.transform;

        GameObject tower = Instantiate(ListTower[(int)TowerType], currentPos.position, currentPos.rotation);
        tower.GetComponent<TowerScript>().isBuild = false;


        Destroy(SelectedObject);
        SelectedObject = null;
        playerManager.ModifyGold(-ListTower[(int)TowerType].GetComponent<TowerScript>().TowerCost);

        if (TowerType == EnumTowerType.SHORTFIRETOWER || TowerType == EnumTowerType.MIDFIRETOWER || TowerType == EnumTowerType.LONGFIRETOWER)
        {
            playerManager.AddElementalGem(0, -1);
        }

        if (TowerType == EnumTowerType.SHORTWATERTOWER || TowerType == EnumTowerType.MIDWATERTOWER || TowerType == EnumTowerType.LONGWATERTOWER)
        {
            playerManager.AddElementalGem(1, -1);
        }

        if (TowerType == EnumTowerType.SHORTEARTHTOWER || TowerType == EnumTowerType.MIDEARTHTOWER || TowerType == EnumTowerType.LONGEARTHTOWER)
        {
            playerManager.AddElementalGem(2, -1);
        }

        ExitConfirmMenu();
        ExitActionMenu();
    }

    //Sell Tower
    public void SellTower()
    {
        playerManager.ModifyGold(SelectedObject.GetComponent<TowerScript>().TowerCost / 2);

        Destroy(SelectedObject);
        SelectedObject = null;

        ExitConfirmMenu();
        ExitActionMenu();
    }

    //Set All Submenus Active = false
    void SetUnactiveAllSubMenus()
    {
        ConstructionMenu.SetActive(true);
        UpgradeMenu.SetActive(false);
        ConfirmMenu.SetActive(false);
        SellMenu.SetActive(false);
    }

    //Close and soft reset the action menu
    public void ExitActionMenu()
    {
        SetUnactiveAllSubMenus();
        ActiveMenu = null;

        if(SelectedObject != null 
            && SelectedObject.TryGetComponent<HighLightScript>(out var component))
        {
            component.UnhighLight();
        }

        SelectedObject = null;
    }

    //Only close the Confirm Menu and his own SubMenus without taking any current data
    public void ExitConfirmMenu()
    {
        ConfirmMenu.SetActive(false);
        UpgradeMenu.SetActive(true);
        TowerType = EnumTowerType.NONE;
    }

    public bool CheckCondition(bool buy)
    {
        bool can = true;

        if(TowerType == EnumTowerType.SHORTFIRETOWER || TowerType == EnumTowerType.MIDFIRETOWER || TowerType == EnumTowerType.LONGFIRETOWER)
        {
            if (playerManager.GemsFire == 0) { can = false; }
        }

        if (TowerType == EnumTowerType.SHORTWATERTOWER || TowerType == EnumTowerType.MIDWATERTOWER || TowerType == EnumTowerType.LONGWATERTOWER)
        {
            if (playerManager.GemsWater == 0) { can = false; }
        }

        if (TowerType == EnumTowerType.SHORTEARTHTOWER || TowerType == EnumTowerType.MIDEARTHTOWER || TowerType == EnumTowerType.LONGEARTHTOWER)
        {
            if (playerManager.GemsEarth == 0) { can = false; }
        }

        if (buy)
        {
            if(ListTower[(int)TowerType].GetComponent<TowerScript>().TowerCost > playerManager.Gold)
            {
                can = false;
            }
            if (1 > playerManager.Gems)
            {
                can = false;
            }
        }
        else
        {
            if (ListTower[(int)TowerType].GetComponent<TowerScript>().TowerCost > playerManager.Gold)
            {
                can = false;
            }
        }

        return can;
    }

    void UpdateInteractButtons()
    {
        BuyUpgradeButton.interactable = CheckCondition(true);
        BuildUpgradeButton.interactable = CheckCondition(false);
    }
}
