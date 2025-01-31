using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected MeshRenderer immunityRenderer;
    [SerializeField] protected int health;
    [SerializeField] protected Projectile[] projectilePrefab;
    [SerializeField] protected AudioClip projectileSound;
    [SerializeField] protected AudioClip getHitSound;

    private List<GameObject> _spawnedEnemies;
    
    protected ProjectileSpawnCombinations ProjectileCombinations;
    protected EnemySpawnCombinations.Mob EnemyDetails;
    protected SoulsView SoulsView;
    protected const float InitialDuration = 1f;
    private const float DamagedCooldown = 3f;
    private const float AllImmunityDuration = 5f;
    public bool IsDamagedRecently = false;
    public bool IsImmune = true;
    protected abstract UniTask MovementBehaviour();
    protected abstract UniTask ProjectileSpawningBehaviour();
    protected abstract void SetProjectileSpawnCombination();
    protected abstract UniTask MoveInitialPosition();
    protected virtual void Awake()
    {
        ProjectileCombinations = Resources.Load<ProjectileSpawnCombinations>("ProjectileSpawnCombinations");
        IsDamagedRecently = true;
        IsImmune = true;
        immunityRenderer.enabled = true;
        StartImmunity();
    }

    public void Initialize(EnemySpawnCombinations.Mob enemyDetails, List<GameObject> spawnedEnemies, SoulsView soulsView)
    {
        EnemyDetails = enemyDetails;
        _spawnedEnemies = spawnedEnemies;
        SoulsView = soulsView;
        SetProjectileSpawnCombination();
    }

    public void OnDamaged(int dmg)
    {
        health -= dmg;

        IsDamagedRecently = true;
        
        if (health <= 0)
        {
            //TODO: or some effect
            SoulsView.IncreaseSoulCount(EnemyDetails.EnemyType switch
            {
                EnemyType.Mini => 10,
                EnemyType.Mid => 50,
                EnemyType.Big => 250
            });
            Destroy(gameObject);
            _spawnedEnemies.Remove(gameObject);
        }
        else
        {
            Vibration v = new Vibration(gameObject.GetComponent<Rigidbody>(), 0.3f, 0.1f);
            StartCoroutine(v.Vibrate());
            SoundManager.Instance.PlaySound(getHitSound, 2f);
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
        await UniTask.Delay(TimeSpan.FromSeconds(AllImmunityDuration));
        immunityRenderer.enabled = false;
        IsDamagedRecently = false;
        IsImmune = false;
    }
}
