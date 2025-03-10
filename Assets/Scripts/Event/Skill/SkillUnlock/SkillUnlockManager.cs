using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillUnlockManager : Singleton<SkillUnlockManager>, ISaveable
{
    public SavePriority LoadPriority => SavePriority.BackPack;
    private HashSet<SkillType> unlockedSkills = new HashSet<SkillType>();

    // 实现LoadData方法
    public void LoadData(GameData data)
    {
        unlockedSkills.Clear();
        foreach (string skillName in data.unlockedSkills)
        {
            if (System.Enum.TryParse<SkillType>(skillName, out SkillType skillType))
            {
                unlockedSkills.Add(skillType);
            }
        }
    }

    // 实现SaveData方法
    public void SaveData(ref GameData data)
    {
        data.unlockedSkills.Clear();
        foreach (SkillType skill in unlockedSkills)
        {
            data.unlockedSkills.Add(skill.ToString());
        }
    }

    public void UnlockSkill(SkillType skillType)
    {
        unlockedSkills.Add(skillType);
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkills.Contains(skillType);
    }

    [ContextMenu("解锁所有技能")]
    public void UnlockAllSkills()
    {
        var allSkills = System.Enum.GetValues(typeof(SkillType))
                             .Cast<SkillType>();
        foreach (var skill in allSkills)
        {
            UnlockSkill(skill);
        }
        Debug.Log("已解锁所有技能");
    }

    [ContextMenu("锁定所有技能")]
    public void LockAllSkills()
    {
        unlockedSkills.Clear();
        Debug.Log("已锁定所有技能");
    }
}