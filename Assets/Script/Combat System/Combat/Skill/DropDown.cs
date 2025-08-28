using UnityEngine;

public class DropDown : MonoBehaviour
{
    [SerializeField] float dropSpeed = 10f;

    void Update()
    {
        Drop();
    }

    void Drop()
    {
        gameObject.transform.position += Vector3.down * dropSpeed * Time.deltaTime;
    }
}
