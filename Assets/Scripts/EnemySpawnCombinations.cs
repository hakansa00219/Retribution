using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnCombinations : ScriptableObject
{

    public Level[] Levels;

    public struct Level
    {
        // 3 level if enough time
        public Turn[] Turns;
    }
    
    public struct Turn
    {
        // 5 turns
        // Each turn 10 mobs
        // All turns fixed.
        public Mob[] Enemies;
    }

    public struct Mob
    {
        public EnemyType EnemyType;
        public Vector3 AfterSpawnPosition;
        //public MovementBehaviour MovementBehaviour;
        //public ProjectileSpawnCombinations.CombinedData ProjectileSpawnCombination;
    }

    
}

public enum EnemyType
{
    Mini = 0,
    Mid = 1,
    Big = 2,
}