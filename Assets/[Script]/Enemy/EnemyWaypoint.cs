/// EnemyWaypoint.cs
/// Author			:	Zhikang Chen
/// Description		:	Script use for wave point 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[ExecuteInEditMode]
public class EnemyWaypoint : MonoBehaviour
{
    [SerializeField]
    public EnemyWaypoint NextPoint;

    private BoxCollider collider;

    private void Awake()
    {
        if(NextPoint == null)
        {
            //gameObject.AddComponent(typeof(BoxCollider));
            //collider = GetComponent<BoxCollider>();
            //collider.isTrigger = true;
            //collider.size = new Vector3(0.25f, 0.25f, 0.25f);
        }
    }

    // Draw gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
        if (NextPoint != null)
        {
            Gizmos.DrawLine(transform.position, NextPoint.transform.position);
        }
    }

    // If the game object has a trigger it will apply damage to player when the enemy colide with it
    private void OnTriggerEnter(Collider collision)
    {
        PathfindingEnemy enemy;
        if (collision.gameObject.TryGetComponent<PathfindingEnemy>(out enemy))
        {
            //GamePlaySceneScript.Instance.HealthManager.OnDamage(enemy.Damage);
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
        }
    }
}
