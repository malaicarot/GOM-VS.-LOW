using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("General Infor")]
    [SerializeField] string skillName;
    [TextArea, SerializeField] string description;
    [SerializeField] Sprite icon;

    [Header("Logic")]
    [SerializeField] string skillClassName;
    [SerializeField] float manaCost;
    [SerializeField] float coolDown;

    [Header("Effect")]
    [SerializeField] GameObject effect;
    [SerializeField] AudioClip sound;

    [Header("Targeting")]
    [SerializeField] float range;
    [SerializeField] bool isAOE;

}
