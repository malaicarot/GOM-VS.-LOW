using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]
public class ItemBase : ScriptableObject
{
    [Header("Infomation")]
    public string ItemName;
    public string ItemType;
    public int SellingPrice;

    [Header("Logic")]
    public Sprite Thumbnail;
    public GameObject ModelPrefab;

}
