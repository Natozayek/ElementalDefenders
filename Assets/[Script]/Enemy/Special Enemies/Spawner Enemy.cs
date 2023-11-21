// Spawner Enemy.cs
// Author: Jakub Kulawiak - 101 251 309
/*
   This Script contains the functions for the 
   Spawner/Necromancer enemy. Specifically their
   ability to spawn enemies on death.  
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerEnemy : PathfindingEnemy
{
    private float m_numbToSpawn = 2;

    SpawnerManager spawnManager;
    

    private float m_maxToSpawn = 4;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        entityHealth = GetComponentInChildren<EntityHealthScript>();
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        spawnManager = FindObjectOfType<SpawnerManager>();

        agent = GetComponentInChildren<NavMeshAgent>();
        agent.speed = speed;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void Start()
    {
        // Will set on spawn the number of enemies it will spawn on death - 2 minimum | 4 maximum
        m_numbToSpawn = Random.Range(2, 5);
        //AddPath();
        //hpValue = Health;
        speedSlowValue = speed / 2;

    }

    private void Update()
    {
        if (agent.remainingDistance < float.Epsilon)
        {
            currentDest++;
            agent.SetDestination(Path[currentDest].position);
        }

        if (slow)
        {
            agent.speed = speedSlowValue;
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0)
            {
                slow = false;
                slowTimer = 2; //HardCoded WIP
                agent.speed = speed;
            }
        }

    }
}
