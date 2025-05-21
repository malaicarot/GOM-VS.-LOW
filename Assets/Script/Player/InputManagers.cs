using UnityEngine;
using UnityEngine.InputSystem;


public class InputManagers : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
    void OnMove(InputValue inputValue)
    {
        MoveInput(inputValue.Get<Vector2>());
    }

    void OnJump(InputValue inputValue)
    {
        JumpInput(inputValue.isPressed);
    }

    void OnSprint(InputValue inputValue)
    {
        SprintInput(inputValue.isPressed);

    }

    void OnLook(InputValue inputValue)
    {
        if (cursorInputForLook)
        {
            LookInput(inputValue.Get<Vector2>());
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
    void LookInput(Vector2 _look)
    {
        look = _look;
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
