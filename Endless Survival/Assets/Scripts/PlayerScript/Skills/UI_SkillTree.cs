using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_SkillTree : MonoBehaviour
{
    private static UI_SkillTree instance;
    private PlayerSkills playerSkills;
    private List<SkillButton> skillButtonList;
    private TMPro.TextMeshProUGUI skillPointsText;
    [SerializeField] private SkillUnlockPath[] skillUnlockPathArray;

    private void Awake()
    {
        instance = this;
        skillPointsText = transform.Find("skillPointsText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
        skillButtonList.Add(new SkillButton(transform.Find("dashBtn"), playerSkills, PlayerSkills.SkillType.Dash));
        skillButtonList.Add(new SkillButton(transform.Find("health1Btn"), playerSkills, PlayerSkills.SkillType.Health_1));
        skillButtonList.Add(new SkillButton(transform.Find("health2Btn"), playerSkills, PlayerSkills.SkillType.Health_2));
        skillButtonList.Add(new SkillButton(transform.Find("shield1Btn"), playerSkills, PlayerSkills.SkillType.Shield_1));
        skillButtonList.Add(new SkillButton(transform.Find("shield2Btn"), playerSkills, PlayerSkills.SkillType.Shield_2));

        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        playerSkills.OnSkillPointsChanged += PlayerSkills_OnSkillPointsChanged;
        UpdateVisuals();
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillPointsChanged(object sender, System.EventArgs e)
    {
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        UpdateVisuals();
    }

 

    public void UpdateSkillPoints()
    {
        skillPointsText.SetText(playerSkills.GetSkillPoints().ToString());
    }
    public void UpdateVisuals()
    {
        foreach (SkillButton skillButton in skillButtonList)
            skillButton.UpdateVisual();

        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray)
        {
            foreach (Image linkImage in skillUnlockPath.linkImageArray)
            {
                linkImage.color = new Color(.5f, .5f, .5f);
            }
        }
        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray)
        {
            if (playerSkills.IsSkillUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType))
            {
                foreach (Image linkImage in skillUnlockPath.linkImageArray)
                {
                    linkImage.color = Color.white;
                }
            }
        }

    }





    private class SkillButton
    {

        private Transform transform;
        private Image image;
        private Image backgroundImage;
        private PlayerSkills playerSkills;
        private PlayerSkills.SkillType skillType;

        public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType)
        {
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            transform.GetComponent<Button_UI>().ClickFunc = () => {
                if (!playerSkills.IsSkillUnlocked(skillType))
                {
                    // Skill not yet unlocked
                    if (!playerSkills.TryUnlockSkill(skillType))
                    {
                        //error message if can't unlock
                    }
                }

            };

        }
        public void UpdateVisual()
        {
            //if (playerSkills.IsSkillUnlocked(skillType))
            //{
            //    image.color = Color.white;
            //    backgroundImage.color = Color.white;
            //}
            //else
            //{
            //    if (playerSkills.CanUnlock(skillType))
            //    {
            //        image.color = Color.gray;
            //        backgroundImage.color = Color.gray;
            //    }
            //    else
            //    {
            //        image.color = Color.black;
            //        backgroundImage.color = Color.black;
            //    }
            //}
        }

    }
    [System.Serializable]
    public class SkillUnlockPath
    {
        public PlayerSkills.SkillType skillType;
        public Image[] linkImageArray;
    }

    //[SerializeField] private SkillUnlockPath[] skillUnlockPathArray;
    //private PlayerSkills playerSkills;

    //private List<SkillButton> skillButtonList;

    //private TMPro.TextMeshProUGUI skillPointsText;

    //private void Awake()
    //{
    //    skillPointsText = transform.Find("skillPointsText").GetComponent<TMPro.TextMeshProUGUI>();
    //}

    //public void SetPlayerSkills(PlayerSkills playerSkills)
    //{
    //    this.playerSkills = playerSkills;

    //    skillButtonList = new List<SkillButton>();
    //    skillButtonList.Add(new SkillButton(transform.Find("SkillBtn"), playerSkills, PlayerSkills.SkillType.Skill));
    //    skillButtonList.Add(new SkillButton(transform.Find("health1Btn"), playerSkills, PlayerSkills.SkillType.Health_1));
    //    skillButtonList.Add(new SkillButton(transform.Find("health2Btn"), playerSkills, PlayerSkills.SkillType.Health_2));
    //    skillButtonList.Add(new SkillButton(transform.Find("shield1Btn"), playerSkills, PlayerSkills.SkillType.Shield_1));
    //    skillButtonList.Add(new SkillButton(transform.Find("shield2Btn"), playerSkills, PlayerSkills.SkillType.Shield_2));

    //    playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
    //    playerSkills.OnSkillPointsChanged += PlayerSkills_OnSkillPointsChanged;

    //    UpdateVisuals();
    //    UpdateSkillPoints();
    //}

    //private void PlayerSkills_OnSkillPointsChanged(object sender, System.EventArgs e)
    //{
    //    UpdateSkillPoints();
    //}

    //private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    //{
    //    UpdateVisuals();
    //}

    //private void UpdateSkillPoints()
    //{
    //    skillPointsText.SetText(playerSkills.GetSkillPoints().ToString());
    //}

    //private void UpdateVisuals()
    //{
    //    foreach (SkillButton skillButton in skillButtonList)
    //    {
    //        skillButton.UpdateVisual();
    //    }

    //    foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray)
    //    {
    //        foreach (Image linkImage in skillUnlockPath.linkImageArray)
    //        {
    //            linkImage.color = new Color(.5f, .5f, .5f);
    //        }
    //    }
    //    foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray)
    //    {
    //        if (playerSkills.IsSkillUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType))
    //        {
    //            foreach (Image linkImage in skillUnlockPath.linkImageArray)
    //            {
    //                linkImage.color = Color.white;
    //            }
    //        }
    //    }

    //}

    //private class SkillButton
    //{

    //    private Transform transform;
    //    private Image image;
    //    private Image backgroundImage;
    //    private PlayerSkills playerSkills;
    //    private PlayerSkills.SkillType skillType;

    ////    public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType)
    ////    {
    ////        this.transform = transform;
    ////        this.playerSkills = playerSkills;
    ////        this.skillType = skillType;

    ////        //transform.GetComponent<Button_UI>().ClickFunc = () => {
    ////        //    if (!playerSkills.IsSkillUnlocked(skillType))
    ////        //    {
    ////        //        // Skill not yet unlocked
    ////        //        if (!playerSkills.TryUnlockSkill(skillType))
    ////        //        {
    ////        //            //Tooltip_Warning.ShowTooltip_Static("Cannot unlock " + skillType + "!");
    ////        //        }
    ////        //    }
    ////        //};
    ////    }
    //    //public void UpdateVisual()
    //    //{
    //    //    if (playerSkills.IsSkillUnlocked(skillType))
    //    //    {
    //    //        image.material = null;
    //    //        backgroundImage.material = null;
    //    //    }
    //    //    else
    //    //    {
    //    //        if (playerSkills.CanUnlock(skillType))
    //    //        {
    //    //            image.material = skillUnlockableMaterial;
    //    //            backgroundImage.material = skillUnlockableMaterial;
    //    //        }
    //    //        else
    //    //        {
    //    //            image.material = skillLockedMaterial;
    //    //            backgroundImage.material = skillUnlockableMaterial;
    //    //        }
    //    //    }
    //    //}
    //    public void UpdateVisual()
    //    {
    //        if (playerSkills.IsSkillUnlocked(skillType))
    //        {
    //            image.material = null;
    //            backgroundImage.material = null;
    //        }
    //        else
    //        {
    //            if (playerSkills.CanUnlock(skillType))
    //            {
    //                backgroundImage.color = UtilsClass.GetColorFromString("4B677D");
    //                transform.GetComponent<Button_UI>().enabled = true;
    //            }
    //            else
    //            {
    //                backgroundImage.color = new Color(.3f, .3f, .3f);
    //                transform.GetComponent<Button_UI>().enabled = false;
    //            }
    //        }
    //    }
    //}
    ////[System.Serializable]
    ////public class SkillUnlockPath
    ////{
    ////    public PlayerSkills.SkillType skillType;
    ////    public Image[] linkImageArray;
    ////}

}
