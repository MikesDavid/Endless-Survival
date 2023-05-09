using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class LevelWindow : MonoBehaviour
{
    private TMP_Text levelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;
    private LevelSystemAnimated LevelSystemAnimated;


    private void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<TMP_Text>();
        experienceBarImage = transform.Find("ExperienceBar").Find("Bar").GetComponent<Image>();

        //Debug
        //transform.Find("DebugXP").Find("Experience5Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(5);
        //transform.Find("DebugXP").Find("Experience50Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(50);
        //transform.Find("DebugXP").Find("Experience500Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(500);

        //Debug
        //SetExperienceBarSize(.5f);
        //SetLevel(7);
    }



    public void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    public void SetLevel(int levelNumber) 
    {
        levelText.text = "LEVEL " + (levelNumber + 1);
        DBManager.szint = levelNumber + 1;
    }
    public void SetLevelSystem(LevelSystem LevelSystem)
    {
        this.levelSystem = LevelSystem;   
    }
    public void SetLevelSystemAnimated(LevelSystemAnimated LevelSystemAnimated)
    {
        this.LevelSystemAnimated = LevelSystemAnimated;

        SetLevel(levelSystem.GetLevelNumber());
        SetExperienceBarSize(LevelSystemAnimated.GetExperienceNormalized());

        LevelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        LevelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevel(LevelSystemAnimated.GetLevelNumber());
    }

    private void LevelSystemAnimated_OnExperienceChanged(object seder, System.EventArgs e)
    {
        SetExperienceBarSize(LevelSystemAnimated.GetExperienceNormalized());
    }
   
}
