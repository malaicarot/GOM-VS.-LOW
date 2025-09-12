using System.Collections.Generic;
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
    public AudioClip WeaponImpact;
    public Attack[] Attacks;
    public List<SkillData> SkillsOfWeapon;
}
