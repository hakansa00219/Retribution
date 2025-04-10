using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoreAnimation : MonoBehaviour
{
    [SerializeField]
    private Image[] loreImages;

    [SerializeField] private bool isStartLore;

    [HideIf(nameof(isStartLore)), SerializeField]
    private Image yesLore;
    [HideIf(nameof(isStartLore)), SerializeField]
    private Image noLore;

    [HideIf(nameof(isStartLore)), SerializeField]
    private Image endLore;
    [HideIf(nameof(isStartLore)), SerializeField]
    private Image yesButtonImage;
    [HideIf(nameof(isStartLore)), SerializeField]
    private Image noButtonImage;
    
    [SerializeField] private float imageDuration;
    [SerializeField] private float alphaDuration;
    [SerializeField] private float blackScreenDuration;

    private bool _isAnswered;
    
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
            if (!isStartLore && i == loreImages.Length - 1)
            {
                float estimatedTime2 = 0;
                while (estimatedTime2 < alphaDuration)
                {
                    yesButtonImage.color = new Color(1,1,1, estimatedTime2 / alphaDuration);
                    noButtonImage.color = new Color(1,1,1, estimatedTime2 / alphaDuration);
                
                    estimatedTime2 += Time.deltaTime;
                    await UniTask.Yield();
                }
                yesButtonImage.color = new Color(1, 1, 1, 1);
                noButtonImage.color = new Color(1, 1, 1, 1);
                Cursor.lockState = CursorLockMode.None;
                break;
            }
                
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
        if (isStartLore)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void EndYesAnswer()
    {
        if (_isAnswered) return;
        EndAnimation(yesLore, endLore, "Start");
        
    }

    public void EndNoAnswer()
    {
        if (_isAnswered) return;
        EndAnimation(noLore, endLore, "Gameplay");
    }

    private async UniTaskVoid EndAnimation(Image firstImage, Image secondImage, string afterSceneName)
    {
        _isAnswered = true;
        Cursor.lockState = CursorLockMode.Locked;
        await UniTask.WhenAll(EndImageAnimation(loreImages[^1]), EndImageAnimation(yesButtonImage), EndImageAnimation(noButtonImage));
        await ImageAnimation(firstImage);
        await ImageAnimation(secondImage);
        await LoadAfterScene(afterSceneName);

    }

    private async UniTask ImageAnimation(Image img)
    {
        img.gameObject.SetActive(true);
        float estimatedTime = 0;
        while (estimatedTime < alphaDuration)
        {
            img.color = new Color(1,1,1, estimatedTime / alphaDuration);
                
            estimatedTime += Time.deltaTime;
            await UniTask.Yield();
        }
        img.color = new Color(1, 1, 1, 1);

        await UniTask.Delay(TimeSpan.FromSeconds(imageDuration));

        estimatedTime = 0;
        while (estimatedTime < alphaDuration)
        {
            img.color = new Color(1,1,1, 1 - (estimatedTime / alphaDuration));
                
            estimatedTime += Time.deltaTime;
            await UniTask.Yield();
        }
            
        await UniTask.Delay(TimeSpan.FromSeconds(blackScreenDuration));
        img.gameObject.SetActive(false);
    }

    private async UniTask EndImageAnimation(Image img)
    {
        float estimatedTime = 0;
        while (estimatedTime < alphaDuration)
        {
            img.color = new Color(1,1,1, 1 - (estimatedTime / alphaDuration));
                
            estimatedTime += Time.deltaTime;
            await UniTask.Yield();
        }
            
        await UniTask.Delay(TimeSpan.FromSeconds(blackScreenDuration));
        img.gameObject.SetActive(false);
    }

    private async UniTask LoadAfterScene(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName);
    }
}
