using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] Transform cam;
    void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
