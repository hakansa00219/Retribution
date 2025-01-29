using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image[] healthImages;
    [SerializeField] private Player player;

    private void OnEnable()
    {
        player.HealthDropped += OnHealthDropped;
    }

    private void OnDisable()
    {
        player.HealthDropped -= OnHealthDropped;
    }

    private void OnHealthDropped(int currentHealth)
    {
        healthImages[currentHealth].gameObject.SetActive(false);
    }
}
