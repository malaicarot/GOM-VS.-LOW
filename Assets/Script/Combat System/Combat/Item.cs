using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemBase item;

    public ItemBase ReturnItem()
    {
        return item;
    }

    public void ReturnToPool()
    {
        Destroy(this);
    }
}


