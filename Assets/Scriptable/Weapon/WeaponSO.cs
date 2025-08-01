using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [Header("General Infor")]
    public string Name;
    public Sprite Thumbnail;
    public GameObject WeaponPrefab;
    [Header("Logic")]
    public string AnimationTag;
    public Attack[] Attacks;
}
