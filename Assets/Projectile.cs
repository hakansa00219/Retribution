using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Player target;

    [SerializeField] private float speed;
    
    public float Speed { get => speed; set => speed = value; }
    
    public Vector3 startPosition { get; set; }

    private void Awake()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
