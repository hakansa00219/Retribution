using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DeflectView : MonoBehaviour
{
    [SerializeField] private Deflect playerDeflect;
    private Image _deflectImage;

    private void Awake()
    {
        _deflectImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        playerDeflect.SkillCast += OnSkillCast;
    }

    private void OnDisable()
    {
        playerDeflect.SkillCast -= OnSkillCast;
    }

    private void OnSkillCast(float cooldown)
    {
        OnSkillCastAsync(cooldown);
    }

    private async UniTaskVoid OnSkillCastAsync(float cooldown)
    {
        _deflectImage.fillAmount = 0;
        float estimatedTime = 0;
        while (estimatedTime < cooldown)
        {
            estimatedTime += Time.deltaTime;
            _deflectImage.fillAmount = estimatedTime / cooldown;
            await UniTask.Yield();
        }
        
        _deflectImage.fillAmount = 1;
    }

}
