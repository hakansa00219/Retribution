using System;
using System.Collections.Generic;
using UnityEngine;

public class ParryTrigger : MonoBehaviour
{
    [SerializeField] private Player owner;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        owner.IsParryable = true;
        owner.projectilesInArea.Add(other.gameObject.GetComponent<Projectile>());
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        owner.IsParryable = false;
        owner.projectilesInArea.Remove(other.gameObject.GetComponent<Projectile>());
    }
}
