using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jones", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public enum Element
    {
        None = 0,
        Fire = 1,
        Water = 2,
        Earth = 3
    }

    [Header("Game Player Data")]
    public int health;
    public int damage;
    public int moneyDrop;
    public float speed;
    public float armor;
    public Element element;

    [Header("Appearance")]
    public Mesh mesh;
    public Material material;

}
