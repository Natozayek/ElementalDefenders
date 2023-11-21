using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(EntityHealthScript))]
public class PathfindingEnemy : MonoBehaviour
{

    [SerializeField]
    public EnemyWaypoint StartingPoint;

    [SerializeField] public Animator animator;
    [SerializeField]
    public int Health = 100;
    public int hpValue;

    [SerializeField]
    public int DropMoney = 5;

    [SerializeField]
    public int Score = 5;
    float s_Score;

    public NavMeshAgent agent;

    [SerializeField]
    public float speed = 1.0f;
    //protected float speedOriginalValue;
    protected float speedSlowValue;

    public bool test = false;
    //SpawnerManager spawnManager;

    [SerializeField]
    public List<Transform> Path;
    public int currentDest = 0;

    public EntityHealthScript entityHealth;
    public SkinnedMeshRenderer renderer;

    public bool slow = false;
    protected float slowTimer = 2;

    public bool testIsSpawner = false;
    public bool isSpawnerCreep = false;
    
    protected PlayerManager playerManager;

    public SpawnerManager spawnManager;

    public EnumElement ElementType;

    [SerializeField] public AudioClip DeathSound;

    public ResourceImageScript? ResourceImage;
    public bool playingDeadSequence = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        entityHealth = GetComponentInChildren<EntityHealthScript>();
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        spawnManager = FindObjectOfType<SpawnerManager>();

        agent = GetComponentInChildren<NavMeshAgent>();
        agent.speed = speed;
        playerManager = FindObjectOfType<PlayerManager>();
        DeathSound = Resources.Load<AudioClip>("Audio/SoundEffect/DeathSound");

        this.gameObject.AddComponent<HealthDisplayScript>();
    }

    private void Start()
    {
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
            if(slowTimer<=0)
            {
                slow = false;
                slowTimer = 2; //HardCoded WIP
                agent.speed = speed;
            }
        }
    }

    public void TakeDamage(int damage, EnumElement attackElemenType)
    {
        float dmg = damage * ElementalDamageMultiplier(attackElemenType);
        hpValue -= (int)dmg;
        if(hpValue <= 0)
        {
            agent.speed = 0;
            StartCoroutine(DeathSequence());
        }
        else if(hpValue > Health) //For when Healer Heals over the max cap
        {
            hpValue = Health;
        }
    }

    public float ElementalDamageMultiplier(EnumElement DamageType)
    {
        float multiplier = 1;
        if (ElementType == EnumElement.FIRE)
        {
            switch (DamageType)
            {
                case EnumElement.WATER:
                    multiplier = 2.0f;
                    break;
                case EnumElement.EARTH:
                    multiplier = 0.5f;
                    break;
            }
        }
        else if (ElementType == EnumElement.WATER)
        {
            switch (DamageType)
            {
                case EnumElement.EARTH:
                    multiplier = 2.0f;
                    break;
                case EnumElement.FIRE:
                    multiplier = 0.5f;
                    break;
            }
        }
        else if (ElementType == EnumElement.EARTH)
        {
            switch (DamageType)
            {
                case EnumElement.FIRE:
                    multiplier = 2.0f;
                    break;
                case EnumElement.WATER:
                    multiplier = 0.5f;
                    break;
            }
        }

        return multiplier;

    }

    public void Dead(bool deadByEndZone)
    {
       //AudioManager.Instance.PlayAudio(DeathSound);

        hpValue = Health;

        if (testIsSpawner && !deadByEndZone)
        {
            spawnManager.SpawnMinnion(this);
        }

        if (isSpawnerCreep)
        {
            Path.Clear();
        }
        else
        {
            ResourceImage.gameObject.SetActive(false);
        }
       if(!testIsSpawner)
        {
            playerManager.WavesEnemiesKilled++;
        }    

       playingDeadSequence = false;

        agent.updateRotation = true;
        agent.speed = speed;
        this.gameObject.SetActive(false);
    }

    IEnumerator  DeathSequence()
    {
        agent.speed = 0;
        agent.updateRotation = false;
        if(!playingDeadSequence)
        {
            int rand = 0;
            animator.SetBool("IsDeath", true);
            if (!isSpawnerCreep)
            {
                rand = Random.Range(0, 12);
                playerManager.ModifyGold(DropMoney);
                playerManager.RandomResourceGain(rand);
                playerManager.AddScore((int)s_Score);
                ResourceImage.AnimStart(this.transform, rand);
            }
            playingDeadSequence = true;
        }

        //DeathSound.Play();
        yield return new WaitForSeconds(1.0f);

        Dead(false);
    }

    private void OnEnable()
    {
        StatScale();
    }

    private void OnDisable()
    {
        currentDest = 0;
        agent.speed = 0;
        hpValue = Health;
        slow = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndZone")
        {
            playerManager.Lifes--;
            Dead(true);
        }
    }

    public void StatScale()
    {
        float s_wave = spawnManager.currentWave;

        //hp
        float s_Hp = Health + 0.5f * Health * s_wave;
        hpValue = (int)s_Hp;
        //speed
        float s_Speed = speed + 0.2f * speed * s_wave;
        agent.speed = s_Speed;
        //score
        s_Score = Score + 0.25f * Score * s_wave;
    }
}
