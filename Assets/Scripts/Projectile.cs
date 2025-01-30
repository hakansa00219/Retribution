using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Material deflectedMaterial;
    [SerializeField] private Material material;

    [SerializeField] private bool isDeflectable;
    private Player _target;
    private Vector3 _direction;

    private float _bornTime;

    public int BaseDamage { get; set; }
    public float Speed { get; set; }
    public bool IsDeflected { get; set; }
    
    public bool IsDeflectable => isDeflectable;

    private void Update()
    {
        _bornTime += Time.deltaTime;
        
        if(_bornTime >= 30f) 
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector3 movement = _direction.normalized * (Speed * Time.deltaTime);
        transform.Translate(movement, Space.World);
    }

    public void SetStats(Vector3 direction, float speed, int baseDamage)
    {
        _direction = direction;
        Speed = speed;
        BaseDamage = baseDamage;
    }

    public void SetMaterialDeflected()
    {
        if(TryGetComponent(out Renderer rnd))
            rnd.material = deflectedMaterial;
    }

    public void Destroy()
    {
        //TODO:effect or animation for projectile destroying
        
        // ----
        Destroy(gameObject);
    }


}
