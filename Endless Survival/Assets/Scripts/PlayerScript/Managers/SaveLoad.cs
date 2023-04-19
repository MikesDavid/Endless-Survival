using CodeMonkey;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using static PlayerSkills;

public class SaveLoad : MonoBehaviour, IDataPersistence
{
    //[SerializeField] private Player player;
    //[SerializeField] private LevelWindow levelWindow;
    private PlayerSkills playerSkills;
    private LevelSystem levelSystem;
    [SerializeField] private UI_SkillTree skillTree;
    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private bool needLevelWindow, needSkillTree;
    private int level, experience, skillPoints;
    private List<SkillType> unlockedSkills;
    [SerializeField] private bool menu, summaryscreen;

    public void SaveData(GameData data)
    {
        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperience();
        skillPoints = playerSkills.GetSkillPoints();
        unlockedSkills = playerSkills.unlockedSkillTypeList;
        data.skillPoints = skillPoints;
        data.unlockedSkills = playerSkills.unlockedSkillTypeList;

        data.level = level;
        data.experience = experience;
    }
    public async void LoadData(GameData data)
    {
        unlockedSkills = data.unlockedSkills;
        await Task.Delay(10);
        //Debug.Log("Skill points: " + data.skillPoints + "level: " + data.level + "experience: " + data.experience);
        levelSystem.level = data.level;
        levelSystem.experience = data.experience;
        if (!menu)
        {
            playerSkills.skillPoints = data.skillPoints;

            foreach (SkillType s in unlockedSkills)
            {
                playerSkills.UnlockSkill(s);
            }
        }
        if (needLevelWindow)
        {

            levelWindow.SetLevel(data.level);
            levelWindow.SetExperienceBarSize((float)data.experience / levelSystem.GetExperienceToNextLevel(data.level));
        }
        if (needSkillTree)
        {
            skillTree.UpdateSkillPoints();
            skillTree.UpdateVisuals();
        }


    }


    //private List<SkillType> unlockedSkills;

    public void SetLevelSystem(LevelSystem LevelSystem)
    {
        this.levelSystem = LevelSystem;
    }
    public void SetPlayerSkills(PlayerSkills PlayerSkills)
    {
        this.playerSkills = PlayerSkills;
    }

   

}
