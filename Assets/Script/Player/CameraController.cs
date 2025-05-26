using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraFollow;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float topClamp = 70f;
    [SerializeField] float botClamp = -40f;



    [SerializeField] InputReader inputReader;


    float targetPitch;
    float targetYaw;

    void LateUpdate()
    {
        CameraLogic();
    }
    void CameraLogic()
    {
        float xLook = inputReader.Look.x * rotationSpeed;
        float yLook = inputReader.Look.y * rotationSpeed;


        targetPitch = UpdateRotation(targetPitch, topClamp, botClamp, yLook, true);
        targetYaw = UpdateRotation(targetYaw, topClamp, botClamp, xLook, false);

        ApllyRotation(targetPitch, targetYaw);
    }


    void ApllyRotation(float pitch, float yaw)
    {

        cameraFollow.localRotation  = Quaternion.Euler(pitch, yaw, cameraFollow.eulerAngles.z);
    }

    float UpdateRotation(float currentRotation, float max, float min, float inputValue, bool isXAsis)
    {
        currentRotation = isXAsis ? -inputValue : inputValue;
        currentRotation = Mathf.Clamp(currentRotation, min, max);
        return currentRotation;
    }
}
