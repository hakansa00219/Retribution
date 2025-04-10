using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float moveSpeed;

    private bool _isGameStarted;
    
    private void OnEnable()
    {
        GameManager.GameStarted += OnGameStarted;
    }
    
    private void OnDisable()
    {
        GameManager.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        _isGameStarted = true;
    }

    private void Update()
    {
        if (!_isGameStarted) return;
        
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z - Time.deltaTime * moveSpeed);
    }
}
