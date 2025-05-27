using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public float FreeLookMovement { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    public Transform CameraTransfrom { get; private set; }
    void Start()
    {
        CameraTransfrom = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}
