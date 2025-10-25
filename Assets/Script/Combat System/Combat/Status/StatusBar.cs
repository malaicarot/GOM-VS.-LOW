using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float changeSpeed = 10f;
    public float value { get; set; } = 1.0f;


    void Update()
    {

        UpdateValue(slider.value, value, changeSpeed);
    }

    public void UpdateValue(float current, float next, float speed)
    {
        slider.value = Mathf.Lerp(current, next, speed * Time.deltaTime);
    }
}
