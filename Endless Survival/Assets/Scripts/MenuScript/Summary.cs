using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Summary : MonoBehaviour
{
    [SerializeField] private SaveLoad saveLoad;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Timer timer;
    [SerializeField] private LevelSystemManager levelSysM;

    [SerializeField] private TextMeshProUGUI summaryTxt, outcomeTxt;



    private GameScene scene;

    private int selectedSecondary = 0;
    private int selectedPrimary = 0;
    private int enemyKilled = LevelSystemManager.enemyKilledMax;

    private string SecondaryWeapon, PrimaryWeapon;


    private void Start()
    {
        scene = levelManager.State;
        LevelManager.OnSceneChanged += LevelManager_OnSceneChanged;

        Outcome(scene);


        
        selectedSecondary = PlayerPrefs.GetInt("selectedSecondary");
        selectedPrimary = PlayerPrefs.GetInt("selectedPrimary");

        SelectedPrimary();
        SelectedSecondary();

        summaryTxt.text = ("Enemyes killed: " + enemyKilled.ToString() + "\n" + 
            "Primary Weapon: " + PrimaryWeapon + "\n" +
            "Secondary Weapon: " + SecondaryWeapon + "\n" );
    }


    private void OnDestroy()
    {
        LevelManager.OnSceneChanged -= LevelManager_OnSceneChanged;

    }
    private void Outcome(GameScene state)
    {
        if (state == GameScene.Wictory)
            outcomeTxt.text = "Mission Succes";
        else if (state == GameScene.Defeat)
            outcomeTxt.text = "Mission Failed";
    }
    private void LevelManager_OnSceneChanged(GameScene state)
    {
         
    }

    private void Update()
    {
        SetTimerText();

    }
    public void Restart()
    {
        DataPersistenceManager.instance.SaveGame();
        levelManager.CurrentScene(GameScene.Start);
        SceneManager.LoadScene(2, LoadSceneMode.Single);

    }
    public void Lobby()
    {
        DataPersistenceManager.instance.SaveGame();
        levelManager.CurrentScene(GameScene.Lobby);
        SceneManager.LoadScene(1, LoadSceneMode.Single);

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
    }
    private void SelectedSecondary()
    {
        if (selectedSecondary == 0)
            SecondaryWeapon = "Pistol";
        else if (selectedSecondary == 1)
            SecondaryWeapon = "Auto Pistol";
    }
}
