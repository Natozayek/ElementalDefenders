using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructDataScript : MonoBehaviour
{
    public EnumTowerType towerType;
    ActionMenuManager actionMenuManager;

    void Start()
    {
        actionMenuManager = FindObjectOfType<ActionMenuManager>().GetComponent<ActionMenuManager>();
    }

    public void SendTowerTypeData()
    {
        actionMenuManager.SetTowerConstructionData(towerType);
    }

    //Used To get what type the tower we want to upgrade is to give the correct data fro the upgradable options for that tower
    public void SetTowerType(int i)
    {
        switch (i)
        {
            case 3:
                towerType = EnumTowerType.SHORTFIRETOWER;
                break;
            case 4:
                towerType = EnumTowerType.SHORTWATERTOWER;
                break;
            case 5:
                towerType = EnumTowerType.SHORTEARTHTOWER;
                break;
            case 6:
                towerType = EnumTowerType.MIDFIRETOWER;
                break;
            case 7:
                towerType = EnumTowerType.MIDWATERTOWER;
                break;
            case 8:
                towerType = EnumTowerType.MIDEARTHTOWER;
                break;
            case 9:
                towerType = EnumTowerType.LONGFIRETOWER;
                break;
            case 10:
                towerType = EnumTowerType.LONGWATERTOWER;
                break;
            case 11:
                towerType = EnumTowerType.LONGEARTHTOWER;
                break;
        }
    }
}
