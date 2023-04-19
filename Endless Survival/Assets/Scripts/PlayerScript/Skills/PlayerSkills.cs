using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerSkills 
{

    public event EventHandler OnSkillPointsChanged;
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }
    public enum SkillType
    {
        none,
        Dash,
        Health_1,
        Health_2,
        Shield_1,
        Shield_2,
    }
    

    public List<SkillType> unlockedSkillTypeList;
    public int skillPoints;
    public PlayerSkills()
    {
        unlockedSkillTypeList = new();
    }
    public void AddSkillPoints()
    {
        skillPoints++;
        OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetSkillPoints()
    {
        return skillPoints;
    }



    public void UnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType))
        {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs {  skillType = skillType });
        }
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);
    }
    public bool CanUnlock(SkillType skillType)
    {
        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.none)
        {
            if (IsSkillUnlocked(skillRequirement))
                return true;
            else
                return false;
        }
        else
            return true;
    }

    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch(skillType)
        {
            case SkillType.Health_2: return SkillType.Health_1;
            case SkillType.Shield_2: return SkillType.Shield_1;
        }
        return SkillType.none;
    }

    public bool TryUnlockSkill(SkillType skillType)
    {
        if(CanUnlock(skillType))
        {
            if (skillPoints > 0)
            {
                skillPoints--;
                OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
                UnlockSkill(skillType);
                return true;
            }
            else
                return false;
            
        }
        else
            return false;


    }
    //public void LoadData(GameData data)
    //{
    //    this.skillPoints = data.skillPoints;
    //    this.unlockedSkillTypeList = data.unlockedSkills;
    //}

    //public void SaveData(GameData data)
    //{
    //    data.skillPoints = this.skillPoints;
    //    data.unlockedSkills = this.unlockedSkillTypeList;
    //}



    //

    //public void AddSkillPoint()
    //{
    //    skillPoints++;
    //    OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    //}

    //public int GetSkillPoints()
    //{
    //    return skillPoints;
    //}

    //private void UnlockSkill(SkillType skillType)
    //{
    //    if (!IsSkillUnlocked(skillType))
    //    {
    //        unlockedSkillTypeList.Add(skillType);
    //        OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
    //    }

    //}

    //public bool IsSkillUnlocked(SkillType skillType)
    //{
    //    return unlockedSkillTypeList.Contains(skillType);
    //}
    //public SkillType GetSkillRequirement(SkillType skillType)
    //{
    //    switch (skillType)
    //    {
    //        case SkillType.Health_2: return SkillType.Health_1;
    //        case SkillType.Shield_2: return SkillType.Shield_1;
    //    }
    //    return SkillType.none;
    //}
    //public bool TryUnlockSkill(SkillType skillType)
    //{
    //    if (CanUnlock(skillType))
    //    {
    //        if(skillPoints > 0)
    //        {
    //            UnlockSkill(skillType);
    //            skillPoints--;
    //            OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }

    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //internal bool CanUnlock(SkillType skillType)
    //{
    //    SkillType skillRequirement = GetSkillRequirement(skillType);
    //    if (skillRequirement != SkillType.none)
    //    {
    //        if (IsSkillUnlocked(skillRequirement))
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
}
