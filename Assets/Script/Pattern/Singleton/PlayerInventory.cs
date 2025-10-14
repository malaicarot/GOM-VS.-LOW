using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
public class PlayerInventory : Singleton<PlayerInventory>
{
    public Dictionary<ItemBase, int> Item { get; set; }
    public InventoryObject inventoryObject;

    void Start()
    {
        Item = new Dictionary<ItemBase, int>();
    }
    public void AddItemByType(ItemBase itemBase)
    {
        if (!CheckExited(itemBase))
        {
            Item.Add(itemBase, 1);
        }
    }

    bool CheckExited(ItemBase itemBase)
    {
        foreach (var item in Item)
        {
            if (item.Key.ItemName == itemBase.ItemName)
            {
                Item[item.Key]++;
                return true;
            }
        }
        return false;
    }
}
