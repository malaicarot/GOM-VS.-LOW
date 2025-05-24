using UnityEngine;
using UnityEngine.InputSystem;


public class InputManagers : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool attack;
    public int combo = 0;
    public bool combat_mode;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpInput(context.performed);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        SprintInput(context.performed);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (cursorInputForLook)
        {
            LookInput(context.ReadValue<Vector2>());
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            combo++;
            combo = Mathf.Clamp(combo, 1, 5);
            if (combo > 4)
            {
                combo = 1;
            }
            Debug.Log(combo);
        }
        AttackInput(context.performed);
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        // AttackInput(context.performed);
    }

    public void OnEquip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CombatModeOn(!combat_mode);
        }
    }
#endif

    void MoveInput(Vector2 _move)
    {
        move = _move;
    }

    void JumpInput(bool _jump)
    {
        jump = _jump;
    }

    void SprintInput(bool _sprint)
    {
        sprint = _sprint;
    }
    void AttackInput(bool _attack)
    {
        attack = _attack;
    }
    void LookInput(Vector2 _look)
    {
        look = _look;
    }

    void CombatModeOn(bool _combat_mode)
    {
        combat_mode = _combat_mode;
    }

    void OnApplicationFocus(bool focus)
    {
        SetCursorState(cursorLocked);
    }

    void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
