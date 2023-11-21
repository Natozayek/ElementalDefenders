/// WaveInfo.cs
/// Author			:	Zhikang Chen
/// Description		:	Scriptable object use to store wave info

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/WaveInfo", order = 1)]
public class WaveInfo : ScriptableObject
{
    // Each wave has phases
    // Each phase can only spawn one type of enemy
    // This allow a wave to have different type of enemies
    [System.Serializable]
    public struct WavePhase
    {
        public EnemyData EnemyType;
        public int Count;
        public float SpawnDelay;
        public float TimeBeforeNextPhase;
        public int StartPoint;
    }

    [FormerlySerializedAs("Wave")]
    [SerializeField]
    public List<WavePhase> Phases;

    [SerializeField]
    public float TimeBeforeNextWave;
}
