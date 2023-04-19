using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSkills;

[System.Serializable]
public class GameData 
{
    public int level;
    public int experience;
    public int skillPoints;
    public List<SkillType> unlockedSkills;

    public GameData() 
    { 
        level = 0;
        experience = 0;
        skillPoints = 0;
    }
}
