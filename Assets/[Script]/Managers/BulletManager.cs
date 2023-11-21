using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private int BulletCount = 30;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject bulletPrefab1;
    [SerializeField]
    private GameObject bulletPrefab2;
    [SerializeField]
    private GameObject bulletPrefab3;

    public List<BulletScript> NormalBullet = new List<BulletScript>();
    public List<BulletScript> FireBullet = new List<BulletScript>();
    public List<BulletScript> WaterBullet = new List<BulletScript>();
    public List<BulletScript> EarthBullet = new List<BulletScript>();

    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Bullet/BulletNormal");
        bulletPrefab1 = Resources.Load<GameObject>("Bullet/BulletFire");
        bulletPrefab2 = Resources.Load<GameObject>("Bullet/BulletWater");
        bulletPrefab3 = Resources.Load<GameObject>("Bullet/BulletEarth");
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeBullets();
    }

    void Update()
    {

    }


    private void MakeBullets()
    {

        if (bulletPrefab == null)
            return;
        if (bulletPrefab1 == null)
            return;
        if (bulletPrefab2 == null)
            return;

        for (int i = 0; i < BulletCount; i++)
        {
            var bullet = Instantiate(bulletPrefab, this.transform);
            var script = bullet.GetComponent<BulletScript>();
            NormalBullet.Add(script);

            bullet.SetActive(false);
        }
        for (int i = 0; i < BulletCount; i++)
        {
            var bullet = Instantiate(bulletPrefab1, this.transform);
            var script = bullet.GetComponent<BulletScript>();
            FireBullet.Add(script);

            bullet.SetActive(false);
        }
        for (int i = 0; i < BulletCount; i++)
        {
            var bullet = Instantiate(bulletPrefab2, this.transform);
            var script = bullet.GetComponent<BulletScript>();
            WaterBullet.Add(script);

            bullet.SetActive(false);
        }
        for (int i = 0; i < BulletCount; i++)
        {
            var bullet = Instantiate(bulletPrefab3, this.transform);
            var script = bullet.GetComponent<BulletScript>();
            EarthBullet.Add(script);

            bullet.SetActive(false);
        }
    }

    public BulletScript GetBullet(EnumElement Type)
    {
        switch (Type)
        {
            case EnumElement.NONE:
                foreach (var bullet in NormalBullet)
                {
                    if(!bullet.gameObject.activeInHierarchy)
                    {
                        return bullet;
                    }
                }
                break;
            case EnumElement.FIRE:
                foreach (var bullet in FireBullet)
                {
                    if (!bullet.gameObject.activeInHierarchy)
                    {
                        return bullet;
                    }
                }
                break;
            case EnumElement.WATER:
                foreach (var bullet in WaterBullet)
                {
                    if (!bullet.gameObject.activeInHierarchy)
                    {
                        return bullet;
                    }
                }
                break;
            case EnumElement.EARTH:
                foreach (var bullet in EarthBullet)
                {
                    if (!bullet.gameObject.activeInHierarchy)
                    {
                        return bullet;
                    }
                }
                break;
        }
        return null;
    }
}
