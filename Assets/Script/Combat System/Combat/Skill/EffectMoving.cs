using UnityEngine;

public class EffectMoving : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float positionSpeed;
    [SerializeField] Quaternion targetQuanternion;
    [SerializeField] Vector3 targetPostion;
    [SerializeField] float timeWaitToMove;
    [SerializeField] Vector3 rootPosition;
    [SerializeField] Quaternion rootRotation;
    public float time;
    bool isActive = false;

    void OnEnable()
    {
        // rootPosition = gameObject.transform.localPosition;
        // rootRotation = gameObject.transform.localRotation;
        Debug.Log(rootPosition);

        isActive = true;
    }


    void OnDisable()
    {
        transform.localPosition = rootPosition;
        transform.localRotation = rootRotation;
        isActive = false;
        time = 0;

    }

    void Update()
    {
        if (isActive)
        {
            time += Time.deltaTime;
            if (time >= timeWaitToMove)
            {
                ChangeRotation();
                ChangePosition();
            }
        }

    }


    void ChangeRotation()
    {
        Vector3 currentAngle = transform.eulerAngles;
        Debug.Log(transform.eulerAngles);
        Vector3 nextAngle = targetQuanternion.eulerAngles;
        float newY = Mathf.MoveTowardsAngle(currentAngle.y, nextAngle.y, speed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0f, newY, 0f);
    }

    void ChangePosition()
    {
        if (transform.localPosition.x > targetPostion.x)
        {
            transform.localPosition += Vector3.left * positionSpeed * Time.deltaTime;
        }
    }
}
