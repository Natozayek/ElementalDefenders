/// EntityHealthScript.cs
/// Author			:	Zhikang Chen
/// Description		:	Script use for health

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealthScript : MonoBehaviour
{
    [SerializeField]
    public float Health = 1;

    public UnityEvent OnDeath;
    private AudioManager m_soundManagerRef;

    public void Awake()
    {
        m_soundManagerRef = FindObjectOfType<AudioManager>();
    }

    // Function use to apply damage to entity
    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnDeath.Invoke();
            //OnDeath.RemoveAllListeners();
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    // Remove all listeners
    private void OnDestroy()
    {
        OnDeath.RemoveAllListeners();
        //m_soundManagerRef.PlayAudio(""); //PLACE SOUND ID HERE 
    }

    public void HealEnemy(float healthValue)
    {
        Health += healthValue;
    }
}
