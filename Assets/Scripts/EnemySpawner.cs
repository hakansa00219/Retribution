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
    
    private void Awake()
    {
        Debug.Log("A1");
        _combinations = Resources.Load<EnemySpawnCombinations>("Enemy Spawn Combinations");
        Debug.Log("A2");
        UniTask.SwitchToMainThread();
        Debug.Log("A21");
        StartLevelSpawning();
    }

    private async UniTaskVoid StartLevelSpawning()
    {
        //Started
        Debug.Log("A3");
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        Debug.Log("A4");
        await UniTask.SwitchToMainThread();
        Debug.Log("A5");
        foreach (var level in _combinations.Levels)
        {
            foreach (var turn in level.Turns)
            {
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
                Debug.Log("A6");
                await UniTask.WaitUntil(() => _spawnedEnemies.Count == 0);
                Debug.Log("A7");
                _spawnedEnemies.Clear();
            }
        }
    }
}