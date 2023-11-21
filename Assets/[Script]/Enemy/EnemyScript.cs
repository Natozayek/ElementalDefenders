/// EnemyWaypoint.cs
/// Author			:	Zhikang Chen
/// Description		:	Script use for enemy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealthScript))]
public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    public EnemyWaypoint NextPoint;

    [SerializeField]
    protected float speed = 1.0f;

    [SerializeField]
    protected float Health = 1;

    [SerializeField]
    public int Damage = 1;

    [SerializeField]
    protected int DropMoney = 5;

    [SerializeField]
    protected AudioClip DealthSound;

    PlayerManager playerManager;

    private float maxHealth;
    private void Start()
    {
        maxHealth = Health;
        var healthComponent = GetComponent<EntityHealthScript>();
        healthComponent.OnDeath.AddListener(OnDeath);
        healthComponent.Health = Health;

        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void FixedUpdate()
    {
        // Head to the position of the wave point
        // If reach it will switch to the next wave point
        if (NextPoint != null)
        {
            Vector3 Heading = NextPoint.transform.position - transform.position;
            transform.position = transform.position + Vector3.ClampMagnitude(Heading, speed);
            if (transform.position == NextPoint.transform.position)
            {
                NextPoint = NextPoint.NextPoint;
            }
        }
    }

    // Give player money and play dealth sound
    public virtual void OnDeath()
    {
        //GamePlaySceneScript.Instance.PlotManager.Money += DropMoney;
        //GamePlaySceneScript.Instance.UpdateMoneyUI(GamePlaySceneScript.Instance.PlotManager.Money);
        AudioManager.Instance.PlayAudio(DealthSound);
        //WaveManager.AllEnemies.Remove(this);

    }

    public void Dead()
    {
        Debug.Log("reach");
        int rand = Random.Range(0, 2);
        playerManager.RandomResourceGain(rand);

        AudioManager.Instance.PlayAudio(DealthSound);
        Destroy(gameObject);
    }

    public void AddToHealth(float healthPoints)
    {
        if(Health < maxHealth)
        {
            Health += healthPoints;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "Collision");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "Trigger");
    }

}
