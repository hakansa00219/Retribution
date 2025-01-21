using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class Mini : Enemy
{
    [SerializeField] private Player target;
    [SerializeField] private float speed;
    [SerializeField] private int projectileBaseDamage;
    [SerializeField] private Transform spawnPosition;
    protected override void MovementBehaviour()
    {
        // throw new System.NotImplementedException();
    }

    protected override void ProjectileSpawningBehaviour()
    {
        Task.Run(SpawnProjectiles);
    }

    private async UniTaskVoid SpawnProjectiles()
    {
        for (int i = 0; i < 5; i++)
        {
            await UniTask.SwitchToMainThread();
            Projectile projectile = Instantiate(projectilePrefab, spawnPosition.position, Quaternion.identity);
            projectile.SetStats(target, speed, projectileBaseDamage);
            await UniTask.DelayFrame(20);
        }
    }
}
