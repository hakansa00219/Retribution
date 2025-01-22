using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSpawnCombinations", menuName = "Scriptable Objects/Projectile Spawn Combinations")]
public class ProjectileSpawnCombinations : ScriptableObject
{
    public List<CombinedData> combinations = new List<CombinedData>();

    [Serializable]
    public struct ProjectileDetails
    {
        public float Speed;
        public Vector3 Direction;
    }
    [Serializable]
    public struct SpawnedData
    {
        public ProjectileDetails[] Projectiles;
    }
    [Serializable]
    public struct CombinedData
    {
        public string skillName;
        public EnemyType enemyType;
        public Vector3 offsetSpawnPosition;
        public int DelayFrameEachSpawn;
        public SpawnedData[] SpawnedData;
    }
}