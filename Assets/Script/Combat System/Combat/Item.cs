using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemBase item;

    public ItemBase ReturnItem()
    {
        return item;
    }

    public void ReturnToPool()
    {
        Destroy(this);
    }
}


