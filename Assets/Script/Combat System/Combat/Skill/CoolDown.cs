using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    [SerializeField] Image fillImage;
    public float coolDownTime { get; set; } = 10f;
    public float cooldown{ get; private set; }

    void Start()
    {
        fillImage.fillAmount = 1;
        cooldown = coolDownTime;
    }
    void Update()
    {
        cooldown += Time.deltaTime;
        cooldown = Mathf.Min(cooldown, coolDownTime);
        float fill = cooldown / coolDownTime;
        fillImage.fillAmount = fill;
    }

    public void DisableUseSkill(Button button)
    {
        button.enabled = false;
    }

    public void SetCooldown()
    {
        cooldown = 0;
    }
}
