using UnityEngine;

public class GetTheTarget : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Target target;
    Vector3 direction;

    void OnEnable()
    {
        target = PlayerSkill.Instance?.target;
        if (target != null)
        {
            direction = target.gameObject.transform.position - gameObject.transform.position;
        }
    }

    void Update()
    {
        GetTarget(Time.deltaTime);
    }

    void GetTarget(float deltaTime)
    {
        gameObject.transform.position += direction * speed * deltaTime;
    }
}
