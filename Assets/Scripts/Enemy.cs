using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected Projectile[] projectilePrefab;

    private List<GameObject> _spawnedEnemies;
    
    protected ProjectileSpawnCombinations ProjectileCombinations;
    protected EnemySpawnCombinations.Mob EnemyDetails;
    protected const float InitialDuration = 1f;
    protected abstract UniTask MovementBehaviour();
    protected abstract UniTask ProjectileSpawningBehaviour();
    protected abstract void SetProjectileSpawnCombination();
    protected abstract UniTaskVoid MoveInitialPosition();
    protected virtual void Awake()
    {
        ProjectileCombinations = Resources.Load<ProjectileSpawnCombinations>("ProjectileSpawnCombinations");
    }

    public void Initialize(EnemySpawnCombinations.Mob enemyDetails, List<GameObject> spawnedEnemies)
    {
        EnemyDetails = enemyDetails;
        _spawnedEnemies = spawnedEnemies;
        
        SetProjectileSpawnCombination();
    }

    public void OnDamaged(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            //TODO: or some effect
            Destroy(gameObject);
            _spawnedEnemies.Remove(gameObject);
        }
    }
    


}
