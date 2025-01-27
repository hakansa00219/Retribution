using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Task = System.Threading.Tasks.Task;
using Vector3 = UnityEngine.Vector3;

public class Mini : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private int projectileBaseDamage;
    private Vector3 _afterSpawnPosition;
    private ProjectileSpawnCombinations.CombinedData _selectedCombination;

    private void Start()
    {
        AI();
        Debug.Log("B2");
    }

    private async UniTaskVoid AI()
    {
        Debug.Log("B3");
        await UniTask.WhenAll(MoveInitialPosition());
        Debug.Log("B4");
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        Debug.Log("B5");
        await UniTask.WhenAll(ProjectileSpawningBehaviour(), MovementBehaviour());
        Debug.Log("B6");
    }

    protected override void SetProjectileSpawnCombination()
    {
        _selectedCombination = ProjectileCombinations.combinations.Find((x) => x.skillName == EnemyDetails.ProjectileSpawnCombination);
    }
    protected override async UniTask MoveInitialPosition()
    {
        // Lerp to the position in the data
        Debug.Log("B8");
        await UniTask.SwitchToMainThread();
        Debug.Log("B9");
        await MovementHelper.MoveTransformAsync(transform, EnemyDetails.AfterSpawnPosition, InitialDuration);
        Debug.Log("B10");
    }
    
    protected override async UniTask MovementBehaviour()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));
    }

    protected override async UniTask ProjectileSpawningBehaviour()
    {
        //TODO: Before spawning maybe some visible effect that you know enemy attacking.
        Debug.Log("B7");
        await UniTask.WhenAll(SpawnProjectiles());
        Debug.Log("B11");
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
