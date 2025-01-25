using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected Projectile[] projectilePrefab;
    protected ProjectileSpawnCombinations ProjectileCombinations;
    protected EnemySpawnCombinations.Mob EnemyDetails;
    protected readonly float InitialDuration = 1f;
    protected abstract UniTask MovementBehaviour();
    protected abstract UniTask ProjectileSpawningBehaviour();
    protected abstract void SetProjectileSpawnCombination();
    protected abstract UniTaskVoid MoveInitialPosition();
    protected virtual void Awake()
    {
        ProjectileCombinations = Resources.Load<ProjectileSpawnCombinations>("ProjectileSpawnCombinations");
    }

    public void Initialize(EnemySpawnCombinations.Mob enemyDetails)
    {
        EnemyDetails = enemyDetails;
        
        SetProjectileSpawnCombination();
    }

    public void OnDamaged(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            //TODO: or some effect
            Destroy(gameObject);
        }
    }
    


}
