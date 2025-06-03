using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float drag = 0.3f;
    [SerializeField] float minImpactToStop = 0.2f;

    float verticalVelocity;
    Vector3 impact;
    Vector3 dampingVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;


    void Update()
    {
        if (verticalVelocity <= 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * 2 * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * 2 * Time.deltaTime;
        }
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        if (agent != null)
        {
            if (impact.sqrMagnitude < minImpactToStop * minImpactToStop)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
        if (agent != null)
        {
            agent.enabled = false;
        }
    }

    public void AddJumpForce(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}
