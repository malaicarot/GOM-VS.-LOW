using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] float changeSpeed = 10f;
    public float fillAmount { get; set; } = 1.0f;

    void Update()
    {
        UpdateFill(fill.fillAmount, fillAmount, changeSpeed);
    }

    public void UpdateFill(float current, float next, float speed)
    {
        fill.fillAmount = Mathf.Lerp(current, next, changeSpeed * Time.deltaTime);
    }
}
