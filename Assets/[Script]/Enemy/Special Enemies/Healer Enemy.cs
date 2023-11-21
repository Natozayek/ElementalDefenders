// Healer Enemy.cs
// Author: Jakub Kulawiak - 101 251 309
/*
   This Script contains the functions for the 
   Healer enemy. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealerEnemy : MonoBehaviour
{
    public List<PathfindingEnemy> m_enemiesToHeal = new List<PathfindingEnemy>();

    private int m_healTotal = 1;
    private float m_healTimer = 5;
    private float m_maxToHeal = 4;
    private float m_timerReset = 4;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(GetComponentInParent<PathfindingEnemy>().hpValue > 0)
        {
            if (m_healTimer <= 0)
            {
                HealEnemies();
                m_healTimer = m_timerReset;
            }
            else m_healTimer -= Time.deltaTime;
        }
    }

    private void HealEnemies()
    {
        foreach (PathfindingEnemy enemy in m_enemiesToHeal)
        {
            if (enemy.hpValue > 0)
            {
                enemy.GetComponent<PathfindingEnemy>().TakeDamage(-m_healTotal, EnumElement.NONE);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isOnList = false;

        foreach (var enemy in m_enemiesToHeal)
        {
            if(enemy.gameObject == other.gameObject)
            {
                isOnList = true;
            }
        }

        if (m_enemiesToHeal.Count < m_maxToHeal)
        {
            if(!isOnList && other.gameObject.tag == "Enemy")
            {
                m_enemiesToHeal.Add(other.gameObject.GetComponent<PathfindingEnemy>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (PathfindingEnemy enemy in m_enemiesToHeal)
        {
            if (enemy.gameObject == other.gameObject)
            {
                m_enemiesToHeal.Remove(enemy);
                break;
            }
        }
    }
}
