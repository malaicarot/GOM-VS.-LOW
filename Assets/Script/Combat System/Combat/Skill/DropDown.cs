using UnityEngine;

public class DropDown : MonoBehaviour
{
    [SerializeField] float dropSpeed = 10f;
    [SerializeField] float startYPosition = 20f;



    void OnDisable()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, startYPosition, gameObject.transform.localPosition.z);
    }

    void Update()
    {
        Drop();
    }

    void Drop()
    {
        gameObject.transform.localPosition += Vector3.down * dropSpeed * Time.deltaTime;
    }
}
