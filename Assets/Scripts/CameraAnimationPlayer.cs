using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraAnimationPlayer : MonoBehaviour
{
    public static CameraAnimationPlayer Instance;
    
    [SerializeField] private TimePause timePause;
    [SerializeField] private Player player;

    public event Action<CamType> CameraChanged;
    public CamType CamType { get; private set; }

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CamType = CamType.Side;
        CameraChanged?.Invoke(CamType.Side);
        timePause.PlayAnimationAndPauseTime();
    }

    private async UniTaskVoid StartAnimation(float delay)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay), ignoreTimeScale: false);
        
        timePause.PlayAnimationAndPauseTime();
    }
    private async UniTaskVoid RandomCameraChanges()
    {
        while (player != null && !player.IsDead)
        {
            float randomSeconds = UnityEngine.Random.Range(8f, 10f);
            await UniTask.Delay(TimeSpan.FromSeconds(randomSeconds), ignoreTimeScale: false);
            CameraChanged?.Invoke(CamType.Side);
            timePause.PlayAnimationAndPauseTime();
        }
        
    }
    
}
public enum CamType
{
    Orthographic = 0,
    Side = 1,
}