using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] float maxMana = 100f;
    [SerializeField] StatusBar statusBar;

    public float currentMana { get; set; }

    void Start()
    {
        currentMana = maxMana;
    }

    void Update()
    {
        if (statusBar != null)
        {
            statusBar.fillAmount = currentMana / maxMana;

        }
    }

    public void IncreaseMana(float amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);

    }


    public void ReduceMana(float amount)
    {
        currentMana = Mathf.Max(currentMana - amount, 0);
    }
}
