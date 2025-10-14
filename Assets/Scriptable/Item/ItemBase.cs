using System;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Brew,
    Medicine,
    Default
}

[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]
public class ItemBase : ScriptableObject
{
    [Header("Infomation")]
    public string ItemName;
    public int SellingPrice;
    public ItemType _ItemType;

    [Header("Logic")]
    public Sprite Thumbnail;
    public GameObject ModelPrefab;
    public bool isExits = false;
}
