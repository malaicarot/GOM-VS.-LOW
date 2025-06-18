using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] Button[] buttonSkill;

    public void UseSkill(int index)
    {
        Debug.Log("Button Index: " + index);
        if (index >= 0 && index < buttonSkill.Length)
        {
            buttonSkill[index].onClick.Invoke();
            ActiveEffect(buttonSkill[index]);
        }
    }


    void ActiveEffect(Button button)
    {
        Debug.Log(button.name);
    }
}
