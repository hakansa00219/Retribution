using System;
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
}