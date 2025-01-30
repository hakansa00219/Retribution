using System;
using TMPro;
using UnityEngine;

public class SoulsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soulCountText;

    public void IncreaseSoulCount(int amount)
    {
        soulCountText.text = (amount + int.Parse(soulCountText.text)).ToString();
    }
}