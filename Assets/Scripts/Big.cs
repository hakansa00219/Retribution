using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Task = System.Threading.Tasks.Task;
using Vector3 = UnityEngine.Vector3;

public class Big : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private int projectileBaseDamage;
    private Vector3 _afterSpawnPosition;
    private ProjectileSpawnCombinations.CombinedData _selectedCombination;

    private void Start()
    {
        AI();
    }

    private async UniTaskVoid AI()
    {
        await UniTask.WhenAll(MoveInitialPosition());
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        await UniTask.WhenAll(ProjectileSpawningBehaviour(), MovementBehaviour());
    }

    protected override void SetProjectileSpawnCombination()
    {
        _selectedCombination = ProjectileCombinations.combinations.Find((x) => x.skillName == EnemyDetails.ProjectileSpawnCombination);
    }
    protected override async UniTask MoveInitialPosition()
    {
        // Lerp to the position in the data
        await UniTask.SwitchToMainThread();
        await MovementHelper.MoveTransformAsync(transform, EnemyDetails.AfterSpawnPosition, InitialDuration);
    }
    
    protected override async UniTask MovementBehaviour()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));
    }

    protected override async UniTask ProjectileSpawningBehaviour()
    {
        //TODO: Before spawning maybe some visible effect that you know enemy attacking.
        await UniTask.WhenAll(SpawnProjectiles());
    }

    private async UniTask SpawnProjectiles()
    {
        for (int i = 0; i < _selectedCombination.SpawnedData.Length; i++)
        {
            await UniTask.SwitchToMainThread();
            var spawnedData = _selectedCombination.SpawnedData[i];
            var rng = Random.Range(0, projectilePrefab.Length);
            for (int j = 0; j < spawnedData.Projectiles.Length; j++)
            {
                var projectileDetails = spawnedData.Projectiles[j];
                Projectile projectile = Instantiate(projectilePrefab[rng], transform.position + _selectedCombination.offsetSpawnPosition, Quaternion.identity);
                projectile.SetStats(projectileDetails.Direction, projectileDetails.Speed, projectileBaseDamage);
            }
            await UniTask.DelayFrame(_selectedCombination.DelayFrameEachSpawn);
        }
    }
}
