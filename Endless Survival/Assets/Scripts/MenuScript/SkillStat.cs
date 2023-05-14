using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillStat : MonoBehaviour
{
    public TextMeshProUGUI statistics;

    public void SetText(string description)
    {
        statistics.text = description;
    }
}
