using System;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Enemy owner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Deflected Projectiles"))
        {
            OnDeflectedProjectileCollided(other);
        }
    }

    private void OnDeflectedProjectileCollided(Collider other)
    {
        if (!other.TryGetComponent(out Projectile projectile))
            return;
        if (!projectile.IsDeflectable)
            return;
        if (!projectile.IsDeflected)
            return;
        
        owner.OnDamaged(projectile.BaseDamage);
        
        projectile.Destroy();
    }
}