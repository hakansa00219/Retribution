using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Deflect))]
public class DeflectTrigger : MonoBehaviour
{
    [SerializeField] private Player owner;
    
    private Deflect _deflectSkill;

    private void Awake()
    {
        _deflectSkill = GetComponent<Deflect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Projectiles"))
        {
            OnProjectileTriggerEntered(other);   
        }
    }

    private void OnProjectileTriggerEntered(Collider other)
    {
        if (!other.TryGetComponent<Projectile>(out var projectile))
            return;
        // Is deflect skill active
        if (!owner.Deflect.IsActive)
            return;
        // Is projectile already deflected one time.
        if (projectile.IsDeflected)
            return;
        _deflectSkill.OnDeflect(projectile);
    }
    
    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("OnTriggerExit");
        // owner.IsParryable = false;
        // owner.projectilesInArea.Remove(other.gameObject.GetComponent<Projectile>());
    }
}
