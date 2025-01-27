using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private TimePause timePause;
    [SerializeField] private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomCameraChanges();
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
        
            timePause.PlayAnimationAndPauseTime();
        }
        
    }
}
