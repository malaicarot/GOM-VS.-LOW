using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    [SerializeField] List<SkillData> bossSkill;

    public SkillData GetSkillBaseName(string name)
    {
        foreach (SkillData skill in bossSkill)
        {
            if (skill.SkillName == name)
            {
                return skill;
            }
        }
        return null;
    }
}
