using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("General Infor")]
    public string skillName;
    [TextArea, SerializeField] string description;
    public Sprite sprite;

    [Header("Logic")]
    [SerializeField] string skillClassName;
    public float manaCost;
    public float coolDown;

    [Header("Effect")]
    public ParticleSystem effect;
    public AudioClip sound;

    [Header("Targeting")]
    public float range;
    public bool isAOE;

}
