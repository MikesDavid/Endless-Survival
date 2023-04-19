using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    

    private static readonly int[] experiencePerLevel = new[] { 100, 120, 155, 180, 210, 250, 280, 315, 355, 400, 460, 500, 545, 595, 680, 720, 800, 870, 920, 1000};

    public int level;
    public int experience;
    
    public LevelSystem() 
    {
        level = 0;
        experience = 0;
    }

    public void AddExperience(int amount)
    {
        
        if (!IsMaxLevel())
        {
            experience += amount;
            while (!IsMaxLevel() && experience >= GetExperienceToNextLevel(level))
            {
                // Enough experience to lvl up
                experience -= GetExperienceToNextLevel(level);
                level++;                
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }
            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }
    public int GetExperience()
    {
        return experience;
    }
    public void SetLevel(int amount)
    {
        level = amount;
    }
    public int GetExperienceToNextLevel(int level)
    {
        if(level < experiencePerLevel.Length)
            return experiencePerLevel[level];
        else
        {
            Debug.LogError("Level invalid: " + level);
            return 100;
        }
        
    }
    public bool IsMaxLevel()
    {
        return IsMaxLevel(level);
    }
    public bool IsMaxLevel(int level)
    {
        return level == experiencePerLevel.Length - 1;
    }
    public float GetExperienceNormalized()
    {
        if (IsMaxLevel())
            return 1f;
        else
            return (float)experience / GetExperienceToNextLevel(level);
    }

    //public void LoadData(GameData data)
    //{
    //    this.level = data.level;
    //    this.experience = data.experience;
    //}

    //public void SaveData(GameData data)
    //{
    //    data.level = this.level;
    //    data.experience = this.experience;
    //}
}
