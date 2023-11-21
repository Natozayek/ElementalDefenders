///// WaveManager.cs
///// Author			:	Zhikang Chen
///// Description		:	Script use to spawn enemy in wave

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.Serialization;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.SceneManagement;

//public class WaveManager : MonoBehaviour
//{
//    static public UnityEvent<int> OnDamagePlayer = new UnityEvent<int>();

//    [SerializeField]
//    private PlayerManager playerManager;

//    public List<PathfindingEnemy> AllEnemies = new List<PathfindingEnemy>();
//    static public LinkedList<PathfindingEnemy> ActiveEnemies = new LinkedList<PathfindingEnemy>();

//    [SerializeField]
//    [FormerlySerializedAs("Waves")]
//    private List<WaveInfo> WaveList;

//    [SerializeField]
//    private List<EnemyWaypoint> StartPoint;

//    [SerializeField]
//    private int enemyCount = 100;

//    static public bool hasBegun = false;

//    private void Awake()
//    {
//        MakeEnemies();
//        OnDamagePlayer.AddListener(DealDamageToPlayer);
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        playerManager = FindObjectOfType<PlayerManager>();
//        if (playerManager == null)
//        {
//            Debug.LogWarning("Warning: Player manager not found");
//        }
//        StartCoroutine(SpawnEnemy());
//    }

//    private void OnDestroy()
//    {
//        hasBegun = false;
//        OnDamagePlayer.RemoveListener(DealDamageToPlayer);
//    }

//    // Spawn enemy from the info given by WaveList
//    private IEnumerator SpawnEnemy()
//    {
//        yield return new WaitForFixedUpdate();
//        playerManager.WaveTotal = WaveList.Count;
//        // Reads the waves scriptable object in WaveList
//        for (int i = 0; i < WaveList.Count; i++)
//        {
//            playerManager.Wave = i + 1;
//            // Each wave has phase
//            // Each phase contain data that determines where and what enemy will be spawn
//            var currentWave = WaveList[i];
//            foreach (var phase in currentWave.Phases)
//            {
//                // Looping through phases
//                for (int i2 = 0; i2 < phase.Count; i2++)
//                {
//                    var enemy = GetEnemy();
//                    if (enemy != null)
//                    {
//                        int startpointIndex = phase.StartPoint;
//                        if(startpointIndex > StartPoint.Count - 1)
//                        {
//                            startpointIndex = 0;
//                        }

//                        enemy.StartingPoint = StartPoint[startpointIndex];
//                        enemy.transform.position = enemy.StartingPoint.transform.position;

//                        //set enemies data here
//                        var enemyData = phase.EnemyType;
//                        enemy.Damage = enemyData.damage;
//                        enemy.DropMoney = enemyData.moneyDrop;
//                        enemy.speed = enemyData.speed;
//                        enemy.entityHealth.Health = enemyData.health;
//                        enemy.renderer.material = enemyData.material;
//                        enemy.renderer.sharedMesh = enemyData.mesh;

//                        enemy.gameObject.SetActive(true);
//                        ActiveEnemies.AddLast(enemy);
//                    }
//                    yield return new WaitForSeconds(phase.SpawnDelay);
//                }
//                yield return new WaitForSeconds(phase.TimeBeforeNextPhase);
//            }
//            yield return new WaitForSeconds(currentWave.TimeBeforeNextWave);
//        }
//        yield return new WaitUntil(HasNoEnemy);
//        yield return new WaitForSeconds(1.0f);

//        SceneManager.LoadSceneAsync("WinScene", LoadSceneMode.Additive);
//    }

//    // Create enemy object from prefab
//    // Note to self
//    // Make the wave manager and the enemies object pool 2 different class
//    // Make enemies object pool a singleton so it wouldnt need to load every level
//    // Maybe add code that make it so it create more enemy when it doesn't any more to spare
//    // Also refactor everything so it's isn't messy
//    private void MakeEnemies()
//    {
//        var enemyPrefab = Resources.Load<GameObject>("Prefab/Enemy");
//        if (enemyPrefab == null)
//            return;

//        for(int i = 0; i < enemyCount; i++)
//        {
//            var enemy = Instantiate(enemyPrefab, this.transform);
//            var script = enemy.GetComponent<PathfindingEnemy>();
//            AllEnemies.Add(script);
//            enemy.SetActive(false);
//        }
//    }

//    private PathfindingEnemy GetEnemy()
//    {
//        foreach(var enemy in AllEnemies)
//        {
//            if(!enemy.gameObject.activeInHierarchy)
//            {
//                return enemy;
//            }
//        }
//        return null;
//    }

//    // When the enemies count is 0
//    public bool HasNoEnemy()
//    {
//        return ActiveEnemies.Count == 0;
//    }

//    public bool HasGameBegun()
//    {
//        return hasBegun;
//    }

//    private void DealDamageToPlayer(int damage)
//    {
//        playerManager.TakeDamage();
//    }
//}
