using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoreAnimation : MonoBehaviour
{
    [SerializeField]
    private Image[] loreImages;

    [SerializeField] private float imageDuration;
    [SerializeField] private float alphaDuration;
    [SerializeField] private float blackScreenDuration;
    
    public void Start()
    {
        Animation();
    }

    private async UniTaskVoid Animation()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(blackScreenDuration));
        
        for (int i = 0; i < loreImages.Length; i++)
        {
            loreImages[i].gameObject.SetActive(true);
            float estimatedTime = 0;
            while (estimatedTime < alphaDuration)
            {
                loreImages[i].color = new Color(1,1,1, estimatedTime / alphaDuration);
                
                estimatedTime += Time.deltaTime;
                await UniTask.Yield();
            }
            loreImages[i].color = new Color(1, 1, 1, 1);

            await UniTask.Delay(TimeSpan.FromSeconds(imageDuration));
            estimatedTime = 0;
            while (estimatedTime < alphaDuration)
            {
                loreImages[i].color = new Color(1,1,1, 1 - (estimatedTime / alphaDuration));
                
                estimatedTime += Time.deltaTime;
                await UniTask.Yield();
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(blackScreenDuration));
            loreImages[i].gameObject.SetActive(false);
        }
        
        AfterAnimation();
    }

    private void AfterAnimation()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
