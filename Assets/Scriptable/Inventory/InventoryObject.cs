using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "Scriptable Objects/InventoryObject")]
public class InventoryObject : ScriptableObject
{
    public List<InventoriesSlot> Contains = new List<InventoriesSlot>();
    

    public void AddItem(ItemBase itemBase, int quantity)
    {
        bool hasItem = false;
        for (int i = 0; i < Contains.Count; i++)
        {
            if (Contains[i].itemBase == itemBase)
            {
                Contains[i].AddQuantity(quantity);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Contains.Add(new InventoriesSlot(itemBase, quantity));
            // itemBase.isExits = true;
        }

    }
}


[Serializable]
public class InventoriesSlot
{
    public ItemBase itemBase;
    public int quantity;
    public event Action OnGetItem;

    public InventoriesSlot(ItemBase itemBase, int quantity)
    {
        this.itemBase = itemBase;
        this.quantity = quantity;
    }

    public void AddQuantity(int quantity)
    {
        this.quantity += quantity;
    }
}