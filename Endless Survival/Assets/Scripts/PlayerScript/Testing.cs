using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private Player player;
    [SerializeField] private SaveLoad saveLoad;

    public int enemyKilled = 0;
    LevelSystem levelSystem = new();
    // [SerializeField] private EquipWindow equipWindow;
    [SerializeField] private UI_SkillTree uiSkillTree;
    private void OnValidate()
    {
        enemyKilled = 0;
    }
    private void Start()
    {
        //i = enemyKilled;
        levelWindow.SetLevelSystem(levelSystem);

        //equipWindow.SetLevelSystem(levelSystem);
        saveLoad.SetLevelSystem(levelSystem);
        saveLoad.SetPlayerSkills(player.GetPlayerSkills());

        LevelSystemAnimated levelSystemAnimated = new(levelSystem);
        levelWindow.SetLevelSystemAnimated(levelSystemAnimated);
        player.SetLevelSystemAnimated(levelSystemAnimated);

        uiSkillTree.SetPlayerSkills(player.GetPlayerSkills());

        //Debug
        //Debug.Log(levelSystem.GetLevelNumber());
        //levelSystem.AddExperience(50);
        //Debug.Log(levelSystem.GetLevelNumber());
        //levelSystem.AddExperience(60);
        //Debug.Log(levelSystem.GetLevelNumber());

    }

}
