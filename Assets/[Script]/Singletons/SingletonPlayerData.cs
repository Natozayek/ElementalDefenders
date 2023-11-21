using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPlayerData : MonoBehaviour
{
    public static SingletonPlayerData Instance { get; private set; }

    public bool didPlayerLose = false;

    [Header("Level1")]
    public int S_enemiesKilled = 0;
    public int S_wavesClear = 0;
    public int S_totalMoney = 0;
    public int S_totalHp = 0;





    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
