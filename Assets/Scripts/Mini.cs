using System;
using System.Numerics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Task = System.Threading.Tasks.Task;

public class Mini : Enemy
{
    [SerializeField] private Player target;
    [SerializeField] private float speed;
    [SerializeField] private int projectileBaseDamage;
    [SerializeField] private Transform spawnPosition;

    private ProjectileSpawnCombinations _combinations;
    private ProjectileSpawnCombinations.CombinedData _selectedCombination;
    
    private void Awake()
    {
        _combinations = Resources.Load<ProjectileSpawnCombinations>("ProjectileSpawnCombinations");
        
        _selectedCombination = _combinations.combinations[Random.Range(0, _combinations.combinations.Count)];
    }

    protected override void MovementBehaviour()
    {
        // throw new System.NotImplementedException();
    }

    protected override void ProjectileSpawningBehaviour()
    {
        //TODO: Before spawning maybe some visible effect that you know enemy attacking.
        Task.Run(SpawnProjectiles);
    }

    private async UniTaskVoid SpawnProjectiles()
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
