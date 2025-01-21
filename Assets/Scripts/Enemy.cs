using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Projectile projectilePrefab;
    protected abstract void MovementBehaviour();
    protected abstract void ProjectileSpawningBehaviour();

    public virtual void Start()
    {
        ProjectileSpawningBehaviour();
    }

    public virtual void Update()
    {
        MovementBehaviour();
    }
}
