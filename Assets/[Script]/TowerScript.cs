using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerScript : MonoBehaviour
{
    public GameObject TargetGameObject;
    public GameObject ShootingStartingPosition;
    [SerializeField] AudioSource ConstructionSound, ConstructionSound2;
    [SerializeField] AudioSource ShootSound;

    [Header("Shooting Data")]
    public bool canShoot = true;
    public EnumTowerType type;
    public EnumElement element = EnumElement.NONE;
    public EnumTowerEffect effect = EnumTowerEffect.NONE;
    public int Damage;
    public float BulletSpeed;
    public float Range;
    public float ShootCoolDown;

    float timer = 0;

    [Header("Other Data")]
    public string DescriptionString;
    public int TowerCost;
    public float constructionTime;
    public float constructionTimer = 0;

    [Header("MultiShoot")]
    public bool canShoot2 = true;
    float timer2 = 0;
    public GameObject TargetGameObject2;


    public GameObject Turret;
    public GameObject? particles;
    public GameObject constructionParticles;
    public bool isBuild;

    bool debuff = false;
    float debuffDuration = 20;
    float debuffExtraCooldown;
    float debuffCount = 10;

    //
    private ActionMenuManager actionMenuManager;
    private BulletManager bulletManager;

    private HighLightScript highLighComponent;

    private void Awake()
    {
        actionMenuManager = GameObject.FindObjectOfType<ActionMenuManager>().GetComponent<ActionMenuManager>();
        bulletManager = FindObjectOfType<BulletManager>();
        highLighComponent = gameObject.AddComponent<HighLightScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SphereCollider>().radius = Range;
        debuffExtraCooldown = ShootCoolDown * 2f;
        
        switch (type)
        {
            case EnumTowerType.NONE:
                break;
            case EnumTowerType.SHORTRANGETOWER:
                break;
            case EnumTowerType.MIDRANGETOWER:
                break;
            case EnumTowerType.LONGRANGETOWER:
                break;
            case EnumTowerType.SHORTFIRETOWER:
                effect = EnumTowerEffect.AOEDAMAGELOW;
                break;
            case EnumTowerType.SHORTWATERTOWER:
                effect = EnumTowerEffect.SLOWLOW;
                break;
            case EnumTowerType.SHORTEARTHTOWER:
                effect = EnumTowerEffect.MULTISHOOT;
                break;
            case EnumTowerType.MIDFIRETOWER:
                effect = EnumTowerEffect.AOEDAMAGE;
                break;
            case EnumTowerType.MIDWATERTOWER:
                effect = EnumTowerEffect.SLOW;
                break;
            case EnumTowerType.MIDEARTHTOWER:
                effect = EnumTowerEffect.MULTISHOOT;
                break;
            case EnumTowerType.LONGFIRETOWER:
                effect = EnumTowerEffect.AOEDAMAGEHUGE;
                break;
            case EnumTowerType.LONGWATERTOWER:
                effect = EnumTowerEffect.SLOWHUGE;
                break;
            case EnumTowerType.LONGEARTHTOWER:
                effect = EnumTowerEffect.MULTISHOOT;
                break;
            default:
                break;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if(TargetGameObject != null && !TargetGameObject.activeSelf)
        {
            TargetGameObject = null;
        }

        if (isBuild)
        {
            if (particles != null)
            {
                particles.SetActive(true);
            }
            constructionParticles.SetActive(false);
        }
        else
        {
            if (particles != null)
            {
                particles.SetActive(false);
            }
            constructionParticles.SetActive(true);
        }

        if (isBuild)
        {
            if (canShoot == false)
            {
                timer += Time.deltaTime;
                if (!debuff)
                {
                    if (timer > ShootCoolDown)
                    {
                        canShoot = true;
                        timer = 0;
                    }
                }
                else
                {
                    if (timer > debuffExtraCooldown)
                    {
                        canShoot = true;
                        timer = 0;
                    }
                }

            }
            else if (TargetGameObject != null)
            {
                Shoot(TargetGameObject);
                canShoot = false;
            }

            if (element == EnumElement.EARTH)
            {
                if (TargetGameObject2 != null && !TargetGameObject2.activeSelf)
                {
                    TargetGameObject2 = null;
                }

                if (canShoot2 == false)
                {
                    timer2 += Time.deltaTime;
                    if (!debuff)
                    {
                        if (timer2 > ShootCoolDown)
                        {
                            canShoot2 = true;
                            timer2 = 0;
                        }
                    }
                    else
                    {
                        if (timer2 > debuffExtraCooldown)
                        {
                            canShoot2 = true;
                            timer2 = 0;
                        }
                    }

                }
                else if (TargetGameObject2 != null)
                {
                    Debug.Log("SHOOT2");
                    Shoot(TargetGameObject2);
                    canShoot2 = false;
                }
            }

            if (TargetGameObject != null)
            {
                RotateTurrer();
            }

            if (debuff)
            {
                Debug.Log(name + " is Debuffed");
                debuffCount += Time.deltaTime;
                if (debuffCount >= debuffDuration)
                {
                    debuffCount = 0;
                    debuff = false;
                    Debug.Log(name + " is not Debuffed");
                }
            }
        }
        else
        {
            constructionTimer += Time.deltaTime;
            if(constructionTimer >= constructionTime)
            {
                constructionTimer = 0;
                isBuild = true;
            }
        }

        
    }

    public void Shoot(GameObject destination)
    {
        var bullet = bulletManager.GetBullet(element);
        bullet.Shoot(Damage, BulletSpeed, ShootingStartingPosition, destination, effect);
        ShootSound.Play();
    }

    public void RotateTurrer()
    {
        Vector3 relativePos = TargetGameObject.transform.position - Turret.transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        Turret.transform.rotation = rotation;
    }

    public void AddDebuff()
    {
        debuffExtraCooldown = ShootCoolDown + (ShootCoolDown / 2);
        debuff = true;
    }


    private void OnTriggerStay(Collider collision)
    {
        if (TargetGameObject == null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log("TargetGet");
                TargetGameObject = collision.gameObject;
            }
        }
        else
        {
            if(TargetGameObject.GetComponent<PathfindingEnemy>().hpValue <= 0)
            {
                TargetGameObject = null;
            }    
        }
        if (element == EnumElement.EARTH)
        {
            if (TargetGameObject2 == null)
            {
                if (collision.gameObject.tag == "Enemy" && collision.gameObject != TargetGameObject)
                {
                    Debug.Log("TargetGet2");
                    TargetGameObject2 = collision.gameObject;
                }
            }
            else
            {
                if (TargetGameObject2.GetComponent<PathfindingEnemy>().hpValue <= 0)
                {
                    TargetGameObject2 = null;
                }
            }
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == TargetGameObject)
        {
            TargetGameObject = null;
        }
        if (element == EnumElement.EARTH)
        {
            if (collision.gameObject == TargetGameObject2)
            {
                TargetGameObject2 = null;
            }
        }
    }

    public void ToggleActionMenu()
    {
        if (actionMenuManager.UpgradeMenu.activeSelf == false)
        {
            actionMenuManager.SetTowerUpgradeData(type);
            if(element == EnumElement.NONE)
            {
                actionMenuManager.ToggleMenu(EnumActionMenuSubMenus.UPGRADEMENU, this.gameObject);
            }
            else
            {
                actionMenuManager.ToggleMenu(EnumActionMenuSubMenus.SELLMENU, this.gameObject);
            }
            highLighComponent.HighLight();
        }
    }
}
