using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeScript : MonoBehaviour
{
    BulletScript bulletData;

    float timer = 0;
    float damage = 0;
    float damageDivider = 1;
    public float AoeDuration = 0.3f;
    public EnumTowerEffect Effect = EnumTowerEffect.NONE;

    private void Awake()
    {
        bulletData = GetComponentInParent<BulletScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnEnable()
    {
        switch (Effect)
        {
            case EnumTowerEffect.NONE:
                break;
            case EnumTowerEffect.AOEDAMAGE:
                AoeDuration = 0.3f;
                damageDivider = 2.0f;
                damage = bulletData.Damage / damageDivider;
                GetComponent<SphereCollider>().radius = 15;
                break;
            case EnumTowerEffect.SLOW:
                AoeDuration = 0.4f;
                GetComponent<SphereCollider>().radius = 15;
                break;
            case EnumTowerEffect.AOEDAMAGELOW:
                AoeDuration = 0.3f;
                damageDivider = 2.5f;
                damage = bulletData.Damage / damageDivider;
                GetComponent<SphereCollider>().radius = 10;
                break;
            case EnumTowerEffect.SLOWLOW:
                AoeDuration = 0.1f;
                GetComponent<SphereCollider>().radius = 10;
                break;
            case EnumTowerEffect.AOEDAMAGEHUGE:
                AoeDuration = 0.3f;
                damageDivider = 2;
                damage = bulletData.Damage / damageDivider;
                GetComponent<SphereCollider>().radius = 20;
                break;
            case EnumTowerEffect.SLOWHUGE:
                AoeDuration = 0.8f;
                GetComponent<SphereCollider>().radius = 20;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            timer += Time.deltaTime;
            if (timer > AoeDuration)
            {
                timer = 0;
                bulletData.SetInActive();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject.activeSelf)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                switch (Effect)
                {
                    case EnumTowerEffect.AOEDAMAGE:
                        collision.gameObject.GetComponent<PathfindingEnemy>().TakeDamage((int)damage, EnumElement.FIRE);
                        break;
                    case EnumTowerEffect.SLOW:
                        collision.gameObject.GetComponent<PathfindingEnemy>().slow = true;
                        break;
                    case EnumTowerEffect.AOEDAMAGELOW:
                        collision.gameObject.GetComponent<PathfindingEnemy>().TakeDamage((int)damage, EnumElement.FIRE);
                        break;
                    case EnumTowerEffect.SLOWLOW:
                        collision.gameObject.GetComponent<PathfindingEnemy>().slow = true;
                        break;
                    case EnumTowerEffect.AOEDAMAGEHUGE:
                        collision.gameObject.GetComponent<PathfindingEnemy>().TakeDamage((int)damage, EnumElement.FIRE);
                        break;
                    case EnumTowerEffect.SLOWHUGE:
                        collision.gameObject.GetComponent<PathfindingEnemy>().slow = true;
                        break;
                }

            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (gameObject.activeSelf)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                switch (Effect)
                {
                    case EnumTowerEffect.SLOW:
                        collision.gameObject.GetComponent<PathfindingEnemy>().slow = false;
                        break;
                    case EnumTowerEffect.SLOWLOW:
                        collision.gameObject.GetComponent<PathfindingEnemy>().slow = false;
                        break;
                    case EnumTowerEffect.SLOWHUGE:
                        collision.gameObject.GetComponent<PathfindingEnemy>().slow = false;
                        break;

                }

            }
        }
    }
}
