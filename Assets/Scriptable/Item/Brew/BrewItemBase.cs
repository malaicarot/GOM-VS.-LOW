using UnityEngine;

[CreateAssetMenu(fileName = "NewBrewItem", menuName = "Scriptable Objects/Item/Brew")]
public class BrewItemBase : ItemBase
{
    public void Awake()
    {
        _ItemType = global::ItemType.Brew;
    }
}
