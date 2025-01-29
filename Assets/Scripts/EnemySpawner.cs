using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : SerializedMonoBehaviour
{
    [SerializeField] private Vector3 enemyInitSpawnPosition;
    [OdinSerialize, SerializeField]
    private Dictionary<EnemyType, GameObject> _enemies = new Dictionary<EnemyType, GameObject>();
    
    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private EnemySpawnCombinations _combinations;

    public event Action<int> TurnChanged;
    
    private void Awake()
    {
        _combinations = Resources.Load<EnemySpawnCombinations>("Enemy Spawn Combinations");
        UniTask.SwitchToMainThread();
        StartLevelSpawning();
    }

    private async UniTaskVoid StartLevelSpawning()
    {
        //Started
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        await UniTask.SwitchToMainThread();
        foreach (var level in _combinations.Levels)
        {
            for (var index = 0; index < level.Turns.Length; index++)
            {
                var turn = level.Turns[index];
                TurnChanged?.Invoke(index);
                foreach (var enemy in turn.Enemies)
                {
                    GameObject enemyObj = enemy.EnemyType switch
                    {
                        EnemyType.Mini => Instantiate(_enemies[EnemyType.Mini], enemyInitSpawnPosition,
                            Quaternion.identity),
                        EnemyType.Mid => Instantiate(_enemies[EnemyType.Mid], enemyInitSpawnPosition,
                            Quaternion.identity),
                        EnemyType.Big => Instantiate(_enemies[EnemyType.Big], enemyInitSpawnPosition,
                            Quaternion.identity),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    if (!enemyObj.TryGetComponent(out Enemy e))
                    {
                        Debug.LogError("Enemy not found");
                        return;
                    }

                    e.Initialize(enemy, _spawnedEnemies);
                    _spawnedEnemies.Add(enemyObj.gameObject);
                }

                await UniTask.WaitUntil(() => _spawnedEnemies.Count == 0);
                _spawnedEnemies.Clear();
                
            }
        }
    }
}