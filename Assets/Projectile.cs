using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private Player _target;
    public float Speed { get => _speed; set => _speed = value; }

    public void SetStats(Player player, float speed)
    {
        _target = player;
        _speed = speed;
    } 

    void FixedUpdate()
    {
        if (_target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }
}
