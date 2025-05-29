using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float drag = 0.3f;

    float verticalVelocity;
    Vector3 impact;
    Vector3 dampingVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;


    void Update()
    {
        if (verticalVelocity <= 0 && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;

        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
