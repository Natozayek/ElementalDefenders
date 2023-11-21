// Freezer Enemy.cs
// Author: Jakub Kulawiak - 101 251 309
/*
   This Script contains the functions for the 
   Freezer Enemy. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezerEnemy : MonoBehaviour
{
    public float m_freezeTimer = 1;

    private void Update()
    {
        m_freezeTimer -= Time.deltaTime;
    }

    private void OnEnable()
    {
        m_freezeTimer = 1;
    }

    private void FreezeTarget(GameObject target)
    {
        target.GetComponent<TowerScript>().AddDebuff();
    }

    void SetFreezeTimer()
    {
        m_freezeTimer = Random.Range(1, 6);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "TargetGetButNotAcceptedYet");
        if (other.tag == "Tower" && m_freezeTimer <= 0)
        {
            Debug.Log(other.name + "TargetGetAndAccepted");
            FreezeTarget(other.gameObject);
            SetFreezeTimer();
        }
    }
}
