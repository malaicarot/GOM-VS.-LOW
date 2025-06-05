using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPooling Instance;
    public ObjectPooling _Instance { get => Instance; set => value = Instance; }

    public void Release()
    {
        if (_Instance != null)
        {
            _Instance.ReturnToPool(this);
        }
    }
}
