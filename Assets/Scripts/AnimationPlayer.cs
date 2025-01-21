using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private TimePause timePause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Task.Run(() => StartAnimation(1));
    }

    private async UniTaskVoid StartAnimation(float delay)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay), ignoreTimeScale: false);
        
        // timePause.PlayAnimationAndPauseTime();
    }

}
