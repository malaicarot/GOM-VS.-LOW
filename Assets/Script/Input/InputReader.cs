using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputControls.IPlayerActions
{
    public Vector2 Movement { get; private set; }
    public bool IsAttack { get; set; }
    public bool IsSecondaryAttack { get; set; }
    public bool IsSprint { get; private set; }
    public bool IsBlocking { get; private set; }
    public int ButtonIndex { get; private set; }
    public bool IsInteract { get; private set; }



    public event Action SkillEvent;
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelTargetEvent;
    public event Action HealingEvent;


    public Vector2 Look { get; private set; }

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
        if (context.started)
        {
            IsAttack = true;
        }
    }
    public void OnSecondary_Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsSecondaryAttack = true;
        }
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }

    public void OnEquip(InputAction.CallbackContext context)
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
        if (context.performed)
        {
            IsSprint = true;
        }
        else if (context.canceled)
        {
            IsSprint = false;
        }
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

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsBlocking = true;
        }
        else if (context.canceled)
        {
            IsBlocking = false;
        }
    }

    public void OnHealing(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        HealingEvent?.Invoke();
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }


        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            ButtonIndex = 0;
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            ButtonIndex = 1;

        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            ButtonIndex = 2;

        }
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            ButtonIndex = 3;

        }
        SkillEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsInteract = true;
        }
        else if (context.canceled)
        {
            IsInteract = false;
        }
    }

    public void OnApplicationFocus(bool forcus)
    {
        SetCursorState(cursorLocked);
    }


    void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

}
