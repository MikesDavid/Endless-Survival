using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Summary : MonoBehaviour
{
    [SerializeField] private SaveLoad saveLoad;
    [SerializeField] private StateManager levelManager;
    [SerializeField] private Timer timer;
    [SerializeField] private LevelSystemManager levelSysM;
    [SerializeField] private DBReadWrite DBWrite;
    [SerializeField] private LoadingScreen loadingScreen;

    [SerializeField] private TextMeshProUGUI summaryTxt, outcomeTxt;



    private GameScene scene;

    private int selectedSecondary = 0;
    private int selectedPrimary = 0;
    private int enemyKilled = LevelSystemManager.enemyKilledMax;

    private string SecondaryWeapon, PrimaryWeapon;


    private void Start()
    {
        scene = levelManager.State;
        StateManager.OnSceneChanged += LevelManager_OnSceneChanged;

        Outcome(scene);


        
        selectedSecondary = PlayerPrefs.GetInt("selectedSecondary");
        selectedPrimary = PlayerPrefs.GetInt("selectedPrimary");

        SelectedPrimary();
        SelectedSecondary();

        summaryTxt.text = ("Enemyes killed: " + enemyKilled.ToString() + "\n" + 
            "Primary Weapon: " + PrimaryWeapon + "\n" +
            "Secondary Weapon: " + SecondaryWeapon + "\n" );

        DBManager.primary = PrimaryWeapon; 
        DBManager.secondary = SecondaryWeapon;
        DBManager.tulelesido = (Timer.saveHour.ToString().PadLeft(2, '0') + ":" + Timer.saveMin.ToString().PadLeft(2, '0') + ":" + Timer.saveSec.ToString("0").PadLeft(2, '0'));

        DBWrite.CallSaveData();
    }


    private void OnDestroy()
    {
        StateManager.OnSceneChanged -= LevelManager_OnSceneChanged;

    }
    private void Outcome(GameScene state)
    {
        if (state == GameScene.Wictory)
        {
            outcomeTxt.text = "Mission Succes";
            DBManager.death = 0;
        }
        else if (state == GameScene.Defeat)
        {
            outcomeTxt.text = "Mission Failed";
            DBManager.death = 1;
        }
    }
    private void LevelManager_OnSceneChanged(GameScene state)
    {
         
    }

    private void Update()
    {
        SetTimerText();

    }
    public async void Restart()
    {

        DataPersistenceManager.instance.SaveGame();
        levelManager.CurrentScene(GameScene.Start);
        await Task.Delay(100);
        loadingScreen.LoadLevel(2);

    }
    public async void Lobby()
    {
        DataPersistenceManager.instance.SaveGame();
        levelManager.CurrentScene(GameScene.Lobby);
        await Task.Delay(100);
        loadingScreen.LoadLevel(1);
    }
    private void SetTimerText()
    {
        timer.finalTime.text = (Timer.saveHour.ToString().PadLeft(2, '0') + ":" + Timer.saveMin.ToString().PadLeft(2, '0') + ":" + Timer.saveSec.ToString("0").PadLeft(2, '0'));

    }
    private void SelectedPrimary()
    {
        if (selectedPrimary == 0)
            PrimaryWeapon = "Machine Gun";
        else if (selectedPrimary == 1)
            PrimaryWeapon = "Shotgun";
        else if (selectedPrimary == 2)
            PrimaryWeapon = "Galactic Annihilator";
        else if (selectedPrimary == 3)
            PrimaryWeapon = "Ion Crusher";
        else if (selectedPrimary == 4)
            PrimaryWeapon = "Quantum Storm";
    }
    private void SelectedSecondary()
    {
        if (selectedSecondary == 0)
            SecondaryWeapon = "Pistol";
        else if (selectedSecondary == 1)
            SecondaryWeapon = "Ionfire ARX";
        else if (selectedSecondary == 2)
            SecondaryWeapon = "Solaris XR-5";
        else if (selectedSecondary == 3)
            SecondaryWeapon = "Solarflare RX-3000";
    }
}
