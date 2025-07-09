using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("General Infor")]
    public string SkillName;
    [TextArea, SerializeField] string Description;
    public Sprite Sprite;

    [Header("Logic")]
    public AnimationClip Animation;
    public float ManaCost;
    public float CoolDown;
    public int LevelNeeded;

    [Header("Effect")]
    public ParticleSystem Effect;
    public AudioClip Sound;

    [Header("Targeting")]
    public float Range;
    public bool IsAOE;

}
