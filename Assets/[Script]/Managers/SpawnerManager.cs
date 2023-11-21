using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Paths
{
    public List<Transform> WayPoints;
}

public class SpawnerManager : MonoBehaviour
{
    //[SerializeField]
    //private EnemyWaypoint StartPoint;

    //[SerializeField]
    //private EnemyWaypoint EndPoint;
    [SerializeField]
    //public List<Transform> WayPoints;
    public List<Paths> Paths;

    [SerializeField]
    private int enemyCount = 30;

    [SerializeField]
    private GameObject PrefabNormal;
    private GameObject PrefabSpawner;
    private GameObject PrefabHealer;
    private GameObject PrefabFrezzer;
    private GameObject PrefabMinion;

    public int level = -1;
    //
    PlayerManager playerManager;
    public List<EnemyWaves> Waves;
    bool isRoundGoing = false;
    public int currentWave = 0;
    float timer = 0.0f;
    public float spawnWaitTime = 1.0f;
    int spawnIndex = 0;
    public int roundEnemies;
    public float BuildTime = 5.0f;
    private float BuildResetTime;


    public List<PathfindingEnemy> EnemyNormal = new List<PathfindingEnemy>();
    public List<PathfindingEnemy> EnemySpawner = new List<PathfindingEnemy>();
    public List<PathfindingEnemy> EnemyHealer = new List<PathfindingEnemy>();
    public List<PathfindingEnemy> EnemyFrezzer = new List<PathfindingEnemy>();
    public List<PathfindingEnemy> EnemyMinion = new List<PathfindingEnemy>();

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        PrefabNormal = Resources.Load<GameObject>("Prefab/Minion");
        PrefabSpawner = Resources.Load<GameObject>("Prefab/Spawner");
        PrefabHealer = Resources.Load<GameObject>("Prefab/Healer");
        PrefabFrezzer = Resources.Load<GameObject>("Prefab/Freezer");
        PrefabMinion = Resources.Load<GameObject>("Prefab/SpawnerCreep");
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeEnemies();
        StartRound();
    }

    void Update()
    {
        if(isRoundGoing)
        {
            timer += Time.deltaTime;

            if(timer>=spawnWaitTime)
            {
                var enemy = GetEnemy(Waves[currentWave].Wave[spawnIndex].Type);
               
                if (enemy.Path.Count > 0)
                {
                    enemy.Path.Clear();
                }
                int path = Waves[currentWave].GetEnemyPath(spawnIndex);
                for (int i = 0; i < Paths[path].WayPoints.Count; i++)
                {
                    enemy.Path.Add(Paths[path].WayPoints[i]);
                }
                enemy.transform.position = enemy.Path[enemy.currentDest].position;
                //enemy.onActivation();
                enemy.gameObject.SetActive(true);
                spawnIndex++;
                timer = 0;
            }

            if(spawnIndex >= roundEnemies)
            {
                    isRoundGoing = false;
            }
        }
        else if (currentWave >= Waves.Count)
        {
            Debug.Log("Win");
            playerManager.GameOver(false, level);
        }
        else if (playerManager.WavesEnemiesKilled >= roundEnemies)
        {
            currentWave++;
            Debug.Log("nextRound");
            StartRound();
        }

    }

    private void StartRound()
    {
        timer = 0;
        spawnIndex = 0;
        roundEnemies = Waves[currentWave].Wave.Count;
        playerManager.WavesEnemiesKilled = 0;
        isRoundGoing = true;
    }

    private void MakeEnemies()
    {
        if (PrefabNormal == null)
            return;
        if (PrefabSpawner == null)
            return;
        if (PrefabHealer == null)
            return;
        if (PrefabFrezzer == null)
            return;
        if (PrefabMinion == null)
            return;

        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(PrefabNormal, this.transform);
            var script = enemy.GetComponent<PathfindingEnemy>();

            //script.StartingPoint = StartPoint;
            EnemyNormal.Add(script);

            enemy.SetActive(false);
        }
        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(PrefabSpawner, this.transform);
            var script = enemy.GetComponent<PathfindingEnemy>();

            //script.StartingPoint = StartPoint;
            EnemySpawner.Add(script);

            enemy.SetActive(false);
        }
        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(PrefabHealer, this.transform);
            var script = enemy.GetComponent<PathfindingEnemy>();

            //script.StartingPoint = StartPoint;
            EnemyHealer.Add(script);

            enemy.SetActive(false);
        }
        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(PrefabFrezzer, this.transform);
            var script = enemy.GetComponent<PathfindingEnemy>();

            //script.StartingPoint = StartPoint;
            EnemyFrezzer.Add(script);

            enemy.SetActive(false);
        }
        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(PrefabMinion, this.transform);
            var script = enemy.GetComponent<PathfindingEnemy>();

            //script.StartingPoint = StartPoint;
            EnemyMinion.Add(script);

            enemy.SetActive(false);
        }
    }

    public PathfindingEnemy GetEnemy(EnumEnemyTypes Type)
    {
        switch (Type)
        {
            case EnumEnemyTypes.NORMAL:
                foreach (var enemy in EnemyNormal)
                {
                    if (!enemy.gameObject.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;
            case EnumEnemyTypes.SPAWNER:
                foreach (var enemy in EnemySpawner)
                {
                    if (!enemy.gameObject.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;
            case EnumEnemyTypes.HEALER:
                foreach (var enemy in EnemyHealer)
                {
                    if (!enemy.gameObject.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;
            case EnumEnemyTypes.FREZZER:
                foreach (var enemy in EnemyFrezzer)
                {
                    if (!enemy.gameObject.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;
            case EnumEnemyTypes.MINNION:
                foreach (var enemy in EnemyMinion)
                {
                    if (!enemy.gameObject.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;
        }

        return null;
    }

    public void SpawnMinnion(PathfindingEnemy deadData)
    {
        var enemy = GetEnemy(EnumEnemyTypes.MINNION);
        enemy.Path.Add(deadData.gameObject.transform);
        for (int i = deadData.currentDest; i < deadData.Path.Count; i++)
        {
            enemy.Path.Add(deadData.Path[i]);
        }
        enemy.transform.position = enemy.Path[enemy.currentDest].position;
        //enemy.onActivation();
        enemy.gameObject.SetActive(true);
    }


}
