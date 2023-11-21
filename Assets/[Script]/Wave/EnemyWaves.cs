using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesInWave
{
    public EnumEnemyTypes Type;
    public int Path;
}

[CreateAssetMenu(fileName = "Waves", menuName = "ScriptableObjects/Waves")]
public class EnemyWaves : ScriptableObject
{
    //public List<EnumEnemyTypes> Wave;
    public List<EnemiesInWave> Wave;

    public EnumEnemyTypes GetEnemyType(int i)
    {
        return Wave[i].Type;
    }    

    public int GetEnemyPath(int i)
    {
        return Wave[i].Path;
    }
}
