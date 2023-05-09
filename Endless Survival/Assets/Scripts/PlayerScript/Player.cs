using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float PlayerHealth = 100f;
    public float CurrentHealth;
    public float PlayerShield = 50f;
    public float CurrentShield;
    public float seconds, RegenSpeed, amount;
    public bool damaged, ingame;
    public static float damagetaken;
    

    public LevelSystemAnimated LevelSystemAnimated;

    [SerializeField]private Image shieldBarImage;
    [SerializeField]private Image healthBarImage;

    private PlayerSkills playerSkills;

    [SerializeField] PlayerController playerController;
    [SerializeField] Dashing dashing;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] private StateManager levelManager;
    private LevelSystem levelSystem;


    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }


        private void Awake()
    {
        playerController.enabled = true;
        dashing.enabled = true;
        mouseLook.enabled = true;
        playerSkills = new PlayerSkills();
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlodked;
        if (ingame)
        {
            SetShieldBar(GetShieldNormalized());
            SetHealthBar(GetHealthNormalized());
        }
        dashing.enabled = false;
        StartCoroutine(RestoreShield(RegenSpeed));
        //SetShieldBar(GetShieldNormalized());
        //SetHealthBar(GetHealthNormalized());
    }
    private async void Start()
    {
        await Task.Delay(11);
        CurrentHealth = PlayerHealth;
        CurrentShield = PlayerShield;
        StateManager.OnSceneChanged += SceneManager_OnSceneChanged; ;

    }

    private void SceneManager_OnSceneChanged(GameScene state)
    {
        if(state == GameScene.Start || state == GameScene.Lobby)
        {
            damagetaken = 0;
        }
        if(state == GameScene.Defeat ||state == GameScene.Wictory)
        {
            DBManager.damageTagen = (int)damagetaken;

        }
    }

    private void Update()
    {
        if (CanUseDash())
            dashing.enabled = true;
        seconds = seconds += Time.deltaTime;
        if (damaged)
            seconds = 0;
        if (seconds >= 5)
            seconds = 5;
        damaged = false;
        if (ingame)
        {
            SetShieldBar(GetShieldNormalized());
            SetHealthBar(GetHealthNormalized());
        }


    }
    private void OnDestroy()
    {

        playerSkills.OnSkillUnlocked -= PlayerSkills_OnSkillUnlodked;
    }

    private void PlayerSkills_OnSkillUnlodked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch (e.skillType)
        {
            case PlayerSkills.SkillType.Health_1:
                AddHealthAmount(50f);
                break;
            case PlayerSkills.SkillType.Health_2:
                AddHealthAmount(100f);
                break;
            case PlayerSkills.SkillType.Shield_1:
                AddShieldAmount(50f);
                break;
            case PlayerSkills.SkillType.Shield_2:
                AddShieldAmount(100f);
                break;
        }
    }

    public void TakeDamage(float amount)
    {

        if (CurrentShield < 0) CurrentShield= 0;
        if (CurrentShield > 0f)
        {
            CurrentShield -= amount;
            damagetaken += amount;
        }
        if (CurrentShield <= 0f)
        {
            CurrentHealth -= amount;
            damagetaken += amount;

            if (CurrentHealth <= 0f)
            {

                levelManager.CurrentScene(GameScene.Defeat);
                DisableMovement();
                LoadSummaryScene();
            }
        }
    }



    private void DisableMovement()
    {
        playerController.enabled = false;
        dashing.enabled = false;
        mouseLook.enabled = false;
    }

    private async void LoadSummaryScene()
    {
        await Task.Delay(5000);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3, LoadSceneMode.Single);
        
    }
    public PlayerSkills GetPlayerSkills()
    {
        return playerSkills;
    }
    public float GetHealthNormalized()
    {
        return CurrentHealth / PlayerHealth;
    }
    public float GetShieldNormalized()
    {
        return CurrentShield / PlayerShield;
    }
    private void SetShieldBar(float ShieldNormalized)
    {
        shieldBarImage.fillAmount = ShieldNormalized;
    }
    private void SetHealthBar(float HealthNormalized)
    {

        healthBarImage.fillAmount = HealthNormalized;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystem)
    {
        this.LevelSystemAnimated = levelSystem;

        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        playerSkills.AddSkillPoints(levelSystem.GetLevelNumber());
        Debug.Log(levelSystem.GetLevelNumber());
    }
    public bool CanUseDash()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Dash);
    }

    private void AddHealthAmount(float HealthAmountAdd)
    {
        PlayerHealth += HealthAmountAdd;
    }
    private void AddShieldAmount(float ShieldAmountAdd)
    {
        PlayerShield += ShieldAmountAdd;
    }
    private IEnumerator RestoreShield(float interval)
    {
        yield return new WaitForSeconds(interval);
        

        if(seconds >= 5 && !damaged)
        {
            if(CurrentShield < PlayerShield)
            {
                CurrentShield += amount ;

            }
        }
        StartCoroutine(RestoreShield(interval));
    }
}
