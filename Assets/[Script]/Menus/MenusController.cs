using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject _MinionDescription, _HealerDescription_, _Spawner_Description, _FreezerDescription, _CreepDescription;
    [SerializeField] GameObject _CloseRange_Description, _MidRange_Description, _LongRangeDescription;


    void OnHoverMinion()
    {
        _MidRange_Description.SetActive(true);
    }
    void OnNotHoverMinion()
    {
        _MidRange_Description.SetActive(false);
    }
}
