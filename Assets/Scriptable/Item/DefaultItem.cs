using UnityEngine;
[CreateAssetMenu(fileName = "NewDefaultItem", menuName = "Scriptable Objects/Item")]
public class DefaultItem : ItemBase
{
    public void Awake()
    {
        _ItemType = global::ItemType.Default;
    }
}
