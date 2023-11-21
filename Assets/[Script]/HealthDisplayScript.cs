using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplayScript : MonoBehaviour
{
    static private Canvas canvas = null;

    private GameObject HealthBarPrefab;

    [SerializeField]
    private RectTransform healthBar;
    private Slider health;
    private float maxHealth;


    [SerializeField]
    private Vector3 healthBarOffset;

    [SerializeField]
    private PathfindingEnemy owner;

    private void Awake()
    {
        HealthBarPrefab = Resources.Load<GameObject>("Prefab/HealthBar");

        if (!canvas)
        {
            canvas = FindObjectOfType<HealthBarTarget>().GetComponent<Canvas>();

            if (!canvas)
            {
                Debug.LogError("Canvas Not Found");
            }
        }

        var tempRef = Instantiate(HealthBarPrefab, canvas.transform);
        healthBar = tempRef.GetComponent<RectTransform>();
        health = tempRef.GetComponentInChildren<Slider>();

        if(!healthBar || !health)
        {
            Debug.LogError("Something wrong with the prefab");
        }


        owner = GetComponentInChildren<PathfindingEnemy>();

        if (owner)
        {
            maxHealth = owner.Health;
        }

        var pos = Camera.main.WorldToScreenPoint(transform.position);
        healthBar.position = pos + healthBarOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        healthBar.position = pos + healthBarOffset;
        health.value = owner.hpValue / maxHealth;
        healthBar.gameObject.SetActive(owner.gameObject.activeInHierarchy);
        //this.gameObject.SetActive()
    }

    private void OnEnable()
    {
        healthBar.gameObject.SetActive(true);
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        healthBar.position = pos + healthBarOffset;
    }

    private void OnDisable()
    {
        healthBar.gameObject.SetActive(false);
    }
}
