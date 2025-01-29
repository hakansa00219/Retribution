using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraAnimationPlayer : MonoBehaviour
{
    public static CameraAnimationPlayer Instance;
    
    [SerializeField] private TimePause timePause;
    [SerializeField] private Player player;

    [SerializeField] private EnemySpawnCombinations turnCameraChanges;
    [SerializeField] private EnemySpawner enemySpawner;
    public event Action<CamType> CameraChanged;
    public CamType CamType { get; private set; } = CamType.Orthographic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple CameraAnimationPlayer instances detected. Destroying extra instance.");
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        enemySpawner.TurnChanged += OnTurnChanged;
    }

    private void OnDisable()
    {
        enemySpawner.TurnChanged -= OnTurnChanged;
    }
    
    private void OnTurnChanged(int turnIndex)
    {
        var newCamType = turnCameraChanges.Levels[0].Turns[turnIndex].CamType;

        if (CamType == newCamType)
            return;
        
        CamType = newCamType;
        CameraChanged?.Invoke(CamType);
        timePause.PlayAnimationAndPauseTime(CamType);
    }

    // private async UniTaskVoid StartAnimation(float delay)
    // {
    //     await UniTask.Delay(TimeSpan.FromSeconds(delay), ignoreTimeScale: false);
    //     
    //     timePause.PlayAnimationAndPauseTime();
    // }
    // private async UniTaskVoid RandomCameraChanges()
    // {
    //     while (player != null && !player.IsDead)
    //     {
    //         float randomSeconds = UnityEngine.Random.Range(8f, 10f);
    //         await UniTask.Delay(TimeSpan.FromSeconds(randomSeconds), ignoreTimeScale: false);
    //         CameraChanged?.Invoke(CamType.Side);
    //         timePause.PlayAnimationAndPauseTime();
    //     }
    //     
    // }
    //
}
public enum CamType
{
    Orthographic = 0,
    Side = 1,
}