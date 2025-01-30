using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPanel : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
    }
}