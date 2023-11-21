using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    BulletManager bulletManager;
    [Header("Automatic")]
    public int Damage;
    public float Speed;
    public GameObject Destination;

    [Header("TowerType")]
    public EnumElement element = EnumElement.NONE; //WIP
    public EnumTowerEffect effect = EnumTowerEffect.NONE; //WIP

    [Header("AoeRelated")]
    public bool hasAoe;
    GameObject aoe;
    bool ignore = false;

    int life = 2;
    float count = 0;

    private void Awake()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!hasAoe)
        {
            aoe = null;
        }
        else
        {
            aoe = transform.Find("Aoe").gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!ignore && gameObject.activeSelf)
        {
            count += Time.deltaTime;

            if (count > life)
            {
                SetInActive();
            }
            else if (Destination == null)
            {
                SetInActive();
            }
            else if (!Destination.gameObject.activeInHierarchy)
            {
                SetInActive();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Destination.transform.position, Speed * Time.deltaTime);
            }
        }

    }

    public void Shoot(int dmg, float speed, GameObject start, GameObject dest, EnumTowerEffect eff)
    {
        Damage = dmg;
        Speed = speed;
        transform.position = start.transform.position;
        Destination = dest;
        effect = eff;

        gameObject.SetActive(true);
    }

    public void SetInActive()
    {
        if(ignore)
        {
            ignore = false;
            GetComponent<MeshRenderer>().enabled = true;
            aoe.SetActive(false);
        }
        gameObject.SetActive(false);
        Destination = null;
        gameObject.transform.position = bulletManager.gameObject.transform.position;
        count = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Destination)
        {
            if (hasAoe)
            {
                ignore = true;
                aoe.GetComponent<AoeScript>().Effect = effect;
                aoe.SetActive(true);
                GetComponent<MeshRenderer>().enabled = false;
                if(element == EnumElement.WATER)
                {
                    collision.gameObject.GetComponent<PathfindingEnemy>().TakeDamage(Damage, element);
                }
            }
            else
            {
                SetInActive();
                collision.gameObject.GetComponent<PathfindingEnemy>().TakeDamage(Damage, element);
            }
        }
    }
}
