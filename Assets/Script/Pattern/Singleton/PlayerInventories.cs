using System.Collections.Generic;
using UnityEngine;
using System;
public enum ItemType
{
    Inlay,
    Material,
    Using
}

public class PlayerInventories : Singleton<PlayerInventories>
{

    [SerializeField] List<ItemBase> InlayItem;
    [SerializeField] List<ItemBase> MaterialItem;
    [SerializeField] List<ItemBase> UsingItem;

    public List<ItemBase> ReturnListInlayItem(string typeList)
    {
        switch (typeList)
        {
            case "Inlay":
                return InlayItem;
            case "Material":
                return MaterialItem;
            case "Using":
                return UsingItem;
            default:
                return null;
        }
    }


    public void AddItemByType(ItemBase itemBase)
    {
        switch (itemBase.ItemType)
        {
            case "Inlay":
                InlayItem.Add(itemBase);
                break;
            case "Material":
                MaterialItem.Add(itemBase);
                break;
            case "Using":
                UsingItem.Add(itemBase);
                break;
            default:
                break;
        }
    }



}
