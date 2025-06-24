using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPooling Instance;
    public ObjectPooling _Instance { get => Instance; set => Instance = value; }


    public void Release()
    {
        if (Instance != null)
        {
            Instance.ReturnToPool(this);
            Debug.Log("Return!");
        }
    }
}
