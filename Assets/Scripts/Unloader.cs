using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unloader : MonoBehaviour
{
    [SerializeField] private Player player;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && player.IsDead)
        {
            Unload();
        }
    }


    private void Unload()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
