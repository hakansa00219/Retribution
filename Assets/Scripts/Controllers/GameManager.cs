using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraAnimationPlayer cameraAnimationPlayer;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private Image startBlackPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private float gameStartDuration;
    
    public static GameManager Instance;
    public static event Action GameStarted;
    public static event Action GameLost;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple GameManager instances detected. Destroying extra instance.");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartTransition();
    }

    private async UniTaskVoid StartTransition()
    {
        await StartBlackScreen();
        tutorialPanel.SetActive(true);
        await UniTask.WaitUntil(() => !tutorialPanel.activeSelf);
        await cameraAnimationPlayer.StartGameAnimation("Start");
        StartGame();
    }

    private void StartGame()
    {
        GameStarted?.Invoke();
    }

    private void Lost()
    {
        restartPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameLost?.Invoke();
    }

    public void LostGame()
    {
        Lost();
    }

    [Sirenix.OdinInspector.Button]
    public void VictoryDebug()
    {
        Victory();
    }
    
    public async UniTask Victory()
    {
        await EndBlackScreen();
        SceneManager.LoadScene("EndLore");
    }

    private async UniTask StartBlackScreen()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        float estimatedTime = 0;
        while (estimatedTime < gameStartDuration)
        {
            startBlackPanel.color = new Color(0f, 0f, 0f, 1 - estimatedTime / gameStartDuration);
            estimatedTime += Time.deltaTime;
            await UniTask.Yield();
        }
        
        startBlackPanel.gameObject.SetActive(false);
    }
    
    private async UniTask EndBlackScreen()
    {
        startBlackPanel.gameObject.SetActive(true);
        
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        float estimatedTime = 0;
        while (estimatedTime < gameStartDuration)
        {
            startBlackPanel.color = new Color(0f, 0f, 0f, estimatedTime / gameStartDuration);
            estimatedTime += Time.deltaTime;
            await UniTask.Yield();
        }
    }
}
