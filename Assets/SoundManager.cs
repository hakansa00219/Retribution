using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private GameObject audioSourcePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple SoundManager instances detected. Destroying extra instance.");
            Destroy(gameObject);
            return;
        }
    }

    public void PlaySound(AudioClip sound, float pitch = 1.0f)
    {
        AudioSource audioSource = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(sound);
        Destroy(audioSource.gameObject, sound.length + 0.5f);
        
        
    }
}

