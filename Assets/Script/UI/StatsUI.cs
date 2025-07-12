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
