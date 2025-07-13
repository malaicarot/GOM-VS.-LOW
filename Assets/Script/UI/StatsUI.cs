using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] GameObject[] statsBoard;
    PlayerStats playerStats;

    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        SetStastSrpite();
        SetUpText();
    }


    void SetUpText()
    {
        foreach (var item in statsBoard)
        {
            TextMeshProUGUI text = item.GetComponentInChildren<TextMeshProUGUI>();
            text.text = playerStats.ReturnAttribute(text.name).ToString();
        }
    }


    void SetStastSrpite()
    {
        foreach (var item in statsBoard)
        {
            Image image = item.GetComponentInChildren<Image>();
            image.sprite = playerStats.ReturnAttributeSprite(image.name);
        }
    }
}
