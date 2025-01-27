using System.Threading.Tasks;
using UnityEngine;


public class Deflect : Skill, IDeflector
{
    public void OnDeflect(Projectile projectile)
    {
        projectile.Speed *= -1;
        projectile.transform.localScale *= 1.2f;
        projectile.SetMaterialDeflected();
        projectile.IsDeflected = true;
    }

    protected override void ActiveSkillEffect()
    {
        IsActive = true;
        Renderer.enabled = true;
        Debug.Log("Skill activated!");
        ChangeBackStatus();
    }
}
