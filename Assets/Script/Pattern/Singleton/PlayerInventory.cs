using System;
using Unity.VisualScripting;
using UnityEngine;

enum BrewItemName
{
    HeartOfHercules,
    ObsessionWithDefense,
    RelicOfSolomon,
    Resentment
}

public class PlayerInventory : Singleton<PlayerInventory>
{
    public InventoryObject inventoryObject;

    public event Action OnUltimateHealing;
    public event Action OnRestoreMana;
    public event Action OnCriticalTime;
    public event Action OnSetCooldown;


    public bool isUltimateHealing;

    public void GetItem(Sprite itemSprite)
    {
        foreach (var item in inventoryObject.Contains)
        {
            if (itemSprite == item.itemBase.Thumbnail)
            {
                Debug.Log(item.itemBase.ItemName);
                InvokeEffect(item.itemBase.ItemName);

            }
        }
    }


    void InvokeEffect(string name)
    {
        switch (name)
        {
            case nameof(BrewItemName.HeartOfHercules):
                isUltimateHealing = true;
                break;
            case nameof(BrewItemName.ObsessionWithDefense):
                OnRestoreMana?.Invoke();
                break;
            case nameof(BrewItemName.RelicOfSolomon):
                OnCriticalTime?.Invoke();

                break;
            case nameof(BrewItemName.Resentment):
                OnSetCooldown?.Invoke();
                break;
        }
    }

}
