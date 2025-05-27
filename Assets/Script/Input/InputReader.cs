using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputControls.IPlayerActions
{
    public Vector2 Movement { get; private set; }
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelTargetEvent;


    public Vector2 Look { get; private set; }
    // public bool Target { get; private set; }
    bool isTarget = false;
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
    InputControls controls;
    void Start()
    {
        controls = new InputControls();
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }


    void OnDestroy()
    {
        controls.Disable();
    }



    public void OnAttack(InputAction.CallbackContext context)
    {
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }

    public void OnEquip(InputAction.CallbackContext context)
    {
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        JumpEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (cursorInputForLook)
        {
            Look = context.ReadValue<Vector2>();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        isTarget = !isTarget;
        if (isTarget)
        {
            TargetEvent?.Invoke();
        }
        else
        {
            CancelTargetEvent?.Invoke();
        }
    }
    void OnApplicationFocus(bool forcus)
    {
        SetCursorState(cursorLocked);
    }

    void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
