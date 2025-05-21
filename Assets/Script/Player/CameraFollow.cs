
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform cameraFollow;
    [SerializeField] float rotationSpeed = 40f;
    [SerializeField] float topClamp = 70f;
    [SerializeField] float bottomClamp = -40f;
    InputManagers input;

    [Header("Local Variables")]
    float cinemachineTargetPitch;
    float cinemachineTargetYaw;

    void Start()
    {
        input = GetComponent<InputManagers>();
    }
    void LateUpdate()
    {
        CameraLogic();
    }
    void CameraLogic()
    {
        float xMouse = input.look.x * rotationSpeed * Time.deltaTime;
        float yMouse = input.look.y * rotationSpeed * Time.deltaTime;

        cinemachineTargetPitch = UpdateRotation(cinemachineTargetPitch, yMouse, bottomClamp, topClamp, true);
        cinemachineTargetYaw = UpdateRotation(cinemachineTargetYaw, xMouse, float.MinValue, float.MaxValue, false);

        ApplyRotation(cinemachineTargetPitch, cinemachineTargetYaw);
    }


    void ApplyRotation(float pitch, float yaw)
    {
        cameraFollow.rotation = Quaternion.Euler(pitch, yaw, cameraFollow.transform.eulerAngles.z);
    }

    float UpdateRotation(float currentRotation, float inputValue, float min, float max, bool isXMouse)
    {
        currentRotation += isXMouse ? -inputValue : inputValue;
        currentRotation = Mathf.Clamp(currentRotation, min, max);
        return currentRotation;
    }

}
