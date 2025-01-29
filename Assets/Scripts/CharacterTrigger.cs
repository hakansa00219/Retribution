using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterTrigger : MonoBehaviour
{
    [SerializeField] private Player owner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Projectiles"))
        {
            OnProjectileTriggerEntered(other);   
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            OnSwordCollidedWithEnemy(other);
        }
    }

    private void OnProjectileTriggerEntered(Collider other)
    {
        // Are you projectile? kinda
        if (!other.TryGetComponent<Projectile>(out var projectile))
            return;
        // Its deflected, ally projectile
        if (projectile.IsDeflected)
            return;
        // Damage
        owner.OnHit(projectile.BaseDamage);
        // Destroy
        projectile.Destroy();

    }

    private void OnSwordCollidedWithEnemy(Collider other)
    {
        // Are you enemy?
        if (!other.TryGetComponent<Enemy>(out var enemy))
            return;

        if (enemy.IsImmune)
            return;

        if (enemy.IsDamagedRecently)
            return;
        // Damage
        enemy.OnDamaged(1);
    }

}