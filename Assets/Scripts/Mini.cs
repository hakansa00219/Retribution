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
    private CamType _currentCamType;

    private void Start()
    {
        CameraAnimationPlayer.Instance.CameraChanged += OnCameraChanged;
        _currentCamType = CameraAnimationPlayer.Instance.CamType;
        AI();
    }

    private void OnCameraChanged(CamType camType)
    {
        if (_currentCamType == camType)
            return;
        
        _currentCamType = camType;

        if (camType == CamType.Side)
        {
            ChangePosition();
        }
    }

    private void ChangePosition()
    {
        MovementHelper.MoveTransformAsyncUnscaled(transform,
            new Vector3(0f, 
                transform.position.y + EnemyDetails.AfterSpawnPosition.x,
                EnemyDetails.AfterSpawnPosition.z + 4f),
                1f);
    }

    private void OnDisable()
    {
        CameraAnimationPlayer.Instance.CameraChanged -= OnCameraChanged;
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
        if (_currentCamType == CamType.Side)
            await MovementHelper.MoveTransformAsyncUnscaled(transform,
                new Vector3(0f, 
                    transform.position.y + EnemyDetails.AfterSpawnPosition.x,
                    EnemyDetails.AfterSpawnPosition.z + 4f),
                1f);
        else
            await MovementHelper.MoveTransformAsync(transform, EnemyDetails.AfterSpawnPosition, InitialDuration);
    }
    
    protected override async UniTask MovementBehaviour()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));
    }

    protected override async UniTask ProjectileSpawningBehaviour()
    {
        //TODO: Before spawning maybe some visible effect that you know enemy attacking.
        while (enabled)
        {
            await UniTask.WhenAll(SpawnProjectiles());
            await UniTask.Delay(TimeSpan.FromSeconds(4));
            await UniTask.Yield();
        }
        
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
                projectile.SetStats(_currentCamType == CamType.Orthographic 
                    ? projectileDetails.Direction 
                    : new Vector3(0f, projectileDetails.Direction.x, projectileDetails.Direction.z)
                    , projectileDetails.Speed, projectileBaseDamage);
            }
            await UniTask.DelayFrame(_selectedCombination.DelayFrameEachSpawn);
        }
    }
}
