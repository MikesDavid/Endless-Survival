using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelSystemManager : MonoBehaviour
{
    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private Player player;
    [SerializeField] private SaveLoad saveLoad;
    [SerializeField] private UI_SkillTree uiSkillTree;
    [SerializeField] private Summary summary;
    // [SerializeField] private EquipWindow equipWindow;

    public bool uiSkilltreeOnOff,lvlWindowOn, summaryscreen;
    private bool Wictory;


    [SerializeField]public int enemyKilled;
    [SerializeField] public static int enemyKilledMax;
    readonly LevelSystem levelSystem = new();
    private async void Start()
    {

        StateManager.OnSceneChanged += LevelManager_OnSceneChanged;
        if(lvlWindowOn)
            levelWindow.SetLevelSystem(levelSystem);

        //equipWindow.SetLevelSystem(levelSystem);
        saveLoad.SetLevelSystem(levelSystem);
        saveLoad.SetPlayerSkills(player.GetPlayerSkills());

        player.SetLevelSystem(levelSystem);

        LevelSystemAnimated levelSystemAnimated = new(levelSystem);
        if(lvlWindowOn)
            levelWindow.SetLevelSystemAnimated(levelSystemAnimated);
        player.SetLevelSystemAnimated(levelSystemAnimated);

        if (uiSkilltreeOnOff)
        {
            uiSkillTree.SetPlayerSkills(player.GetPlayerSkills());
        }
        if (summaryscreen)
        {
            await Task.Delay(15);
            if (!Wictory)
                levelSystem.AddExperience(LevelSystemManager.enemyKilledMax * 2);
            if (Wictory)
                levelSystem.AddExperience(LevelSystemManager.enemyKilledMax * 4);

        }
    }
    //private void FixedUpdate()
    //{
    //    Debug.Log(enemyKilledMax);
    //}
    private void OnDestroy()
    {
        StateManager.OnSceneChanged -= LevelManager_OnSceneChanged;

    }
    private void LevelManager_OnSceneChanged(GameScene state)
    {
        if(state == GameScene.Start)
        {
            enemyKilled = 0;
            enemyKilledMax = 0;
        }
        if(state == GameScene.Defeat || state == GameScene.Wictory)
        {
            enemyKilledMax = enemyKilled;
            DBManager.kills = enemyKilledMax;
        }
        if (state == GameScene.Defeat)
            Wictory = false;
        if (state == GameScene.Wictory)
            Wictory = true;
    }
}
