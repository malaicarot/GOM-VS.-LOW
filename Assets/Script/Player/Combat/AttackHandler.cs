using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] GameObject[] attackLogic;


    public void OnEnableAttack()
    {
        foreach (GameObject attack in attackLogic)
        {
            attack.SetActive(true);
        }
    }

    public void OnDisableAttack()
    {
        foreach (GameObject attack in attackLogic)
        {
            attack.SetActive(false);
        }
    }

}
