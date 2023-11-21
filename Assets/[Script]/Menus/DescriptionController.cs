using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    [SerializeField] GameObject _MinionDescription, _HealerDescription_, _Spawner_Description, _FreezerDescription, _CreepDescription;
    [SerializeField] GameObject _CloseRange_Description, _MidRange_Description, _LongRangeDescription;


    public void OnHoverMinion()
    {
        _MinionDescription.SetActive(true);
    }
    public void OnNotHoverMinion()
    {
        _MinionDescription.SetActive(false);
    }
    public void OnHoverHealer()
    {
        _HealerDescription_.SetActive(true);
    }
    public void OnNotHoverHealer()
    {
        _HealerDescription_.SetActive(false);
    }
    public void OnHoverFreezer()
    {
        _FreezerDescription.SetActive(true);
    }
    public void OnNotHoverFreezer()
    {
        _FreezerDescription.SetActive(false);
    }
    public void OnHoverSpawner()
    {
        _Spawner_Description.SetActive(true);
    }
    public void OnNotHoverSpawner()
    {
        _Spawner_Description.SetActive(false);
    }
    public void OnHoverCreep()
    {
        _CreepDescription.SetActive(true);
    }
    public void OnNotHoverCreep()
    {
        _CreepDescription.SetActive(false);
    }
    public void OnHoverSHORTWR()
    {
        _CloseRange_Description.SetActive(true);
    }
    public void OnNotHoverSHORTWR()
    {
        _CloseRange_Description.SetActive(false);
    }
    public void OnHoverMIDTWR()
    {
        _MidRange_Description.SetActive(true);
    }
    public void OnNotHoverMIDTWR()
    {
        _MidRange_Description.SetActive(false);
    }
    public void OnHoverLONGTWR()
    {
        _LongRangeDescription.SetActive(true);
    }
    public void OnNotHoverLONGTWR()
    {
        _LongRangeDescription.SetActive(false);
    }
}

