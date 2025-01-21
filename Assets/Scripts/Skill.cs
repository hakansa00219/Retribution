using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class Skill : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float skillCooldown;
    private bool IsSkillCastable { get; set; } = true;
    public bool IsActive { get; protected set; }
    protected Renderer Renderer { get; private set; }

    protected void Awake()
    {
        Renderer = GetComponent<Renderer>();
    }

    public virtual void Use()
    {
        if (!IsSkillCastable)
        {
            //TODO: maybe sound effect
            Debug.LogError("Skill is on cooldown!");
            return;
        }
        
        ActiveSkillEffect();
        IsSkillCastable = false;
        Task.Run(Cooldown);
    }

    protected abstract void ActiveSkillEffect();
    

    protected virtual async UniTaskVoid ChangeBackStatus()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(duration), DelayType.DeltaTime);

        Debug.Log("Skill disabled!");
        IsActive = false;
        Renderer.enabled = false;
    }
    
    protected virtual async UniTaskVoid Cooldown()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(skillCooldown), DelayType.DeltaTime);

        Debug.Log("Skill enabled to use!");
        IsSkillCastable = true;
    }
}