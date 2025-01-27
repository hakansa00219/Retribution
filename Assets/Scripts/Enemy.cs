using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected MeshRenderer immunityRenderer;
    [SerializeField] protected int health;
    [SerializeField] protected Projectile[] projectilePrefab;

    private List<GameObject> _spawnedEnemies;
    
    protected ProjectileSpawnCombinations ProjectileCombinations;
    protected EnemySpawnCombinations.Mob EnemyDetails;
    protected const float InitialDuration = 1f;
    private const float DamagedCooldown = 3f;
    private const float StartImmunityDuration = 5f;
    public bool IsDamagedRecently = false;
    protected abstract UniTask MovementBehaviour();
    protected abstract UniTask ProjectileSpawningBehaviour();
    protected abstract void SetProjectileSpawnCombination();
    protected abstract UniTask MoveInitialPosition();
    protected virtual void Awake()
    {
        ProjectileCombinations = Resources.Load<ProjectileSpawnCombinations>("ProjectileSpawnCombinations");
        IsDamagedRecently = true;
        immunityRenderer.enabled = true;
        StartImmunity();
    }

    public void Initialize(EnemySpawnCombinations.Mob enemyDetails, List<GameObject> spawnedEnemies)
    {
        EnemyDetails = enemyDetails;
        _spawnedEnemies = spawnedEnemies;
        Debug.Log("B1");
        SetProjectileSpawnCombination();
    }

    public void OnDamaged(int dmg)
    {
        health -= dmg;

        IsDamagedRecently = true;
        
        if (health <= 0)
        {
            //TODO: or some effect
            Destroy(gameObject);
            _spawnedEnemies.Remove(gameObject);
        }
        else
        {
            Vibration v = new Vibration(gameObject.GetComponent<Rigidbody>(), 0.3f, 0.1f);
            StartCoroutine(v.Vibrate());
            immunityRenderer.enabled = true;
            DamageCooldownReset();
        }
    }

    private async UniTaskVoid DamageCooldownReset()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(DamagedCooldown));
        immunityRenderer.enabled = false;
        IsDamagedRecently = false;
    }

    private async UniTaskVoid StartImmunity()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(StartImmunityDuration));
        immunityRenderer.enabled = false;
        IsDamagedRecently = false;
    }
}
