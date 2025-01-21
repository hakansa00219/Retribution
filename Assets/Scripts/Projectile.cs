using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Material deflectedMaterial;
    [SerializeField] private Material material;
    
    private Player _target;
    private Vector3 _direction;
    
    public float Speed { get; set; }
    public bool IsDeflected { get; set; }

    private void Start()
    {
        if (_target == null) return;

        _direction = _target.transform.position - transform.position;
    }

    public void SetStats(Player player, float speed)
    {
        _target = player;
        Speed = speed;
    }

    public void SetMaterialDeflected()
    {
        if(TryGetComponent(out Renderer rnd))
            rnd.material = deflectedMaterial;
    } 

    void FixedUpdate()
    {
        Vector3 movement = _direction.normalized * (Speed * Time.deltaTime);
        transform.Translate(movement, Space.World);
    }
}
