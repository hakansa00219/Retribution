using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions inputActions;
    public bool IsParryable { get; set; }
    public List<Projectile> projectilesInArea = new List<Projectile>();
    
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Parry.performed += OnParry;
    }
    
    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Parry.performed -= OnParry;
    }
    
    private void OnParry(InputAction.CallbackContext context)
    {
        if (!IsParryable) return;
        
        Debug.Log("Parried");
        
        foreach (var projectile in projectilesInArea)
        {
            projectile.Speed *= -1;

            projectile.transform.localScale *= 1.2f;
        }
        
    }
}
