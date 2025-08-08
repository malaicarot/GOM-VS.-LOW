using System;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] GameObject attackRightLogic;
    [SerializeField] GameObject attackLeftLogic;

    public event Action PlayAttackSound;

    public void OnEnableAttackRight()
    {
        if (attackRightLogic == null)
        {
            return;
        }
        attackRightLogic.SetActive(true);
        PlayAttackSound?.Invoke();
    }

    public void OnDisableAttackRight()
    {
        if (attackRightLogic == null)
        {
            return;
        }
        attackRightLogic.SetActive(false);
    }

    public void OnEnableAttackLeft()
    {
        if (attackLeftLogic == null)
        {
            return;
        }
        attackLeftLogic.SetActive(true);
        PlayAttackSound?.Invoke();

    }

    public void OnDisableAttackLeft()
    {
        if (attackLeftLogic == null)
        {
            return;
        }
        attackLeftLogic.SetActive(false);
    }
}
