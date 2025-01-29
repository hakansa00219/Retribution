using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Enemy Spawn Combinations", fileName = "Enemy Spawn Combinations")]
public class EnemySpawnCombinations : ScriptableObject
{

    public Level[] Levels;

    [Serializable]
    public struct Level
    {
        // 3 level if enough time
        public Turn[] Turns;
    }
    [Serializable]
    public struct Turn
    {
        // 5 turns
        // Each turn 10 mobs
        // All turns fixed.
        public Mob[] Enemies;
        public CamType CamType;
    }

    [Serializable]
    public struct Mob
    {
        public EnemyType EnemyType;
        public Vector3 AfterSpawnPosition;
        public string MovementBehaviour;
        public string ProjectileSpawnCombination;
    }

    
}

public enum EnemyType
{
    Mini = 0,
    Mid = 1,
    Big = 2,
}