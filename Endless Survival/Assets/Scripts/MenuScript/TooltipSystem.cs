using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;
    public SkillStat tooltip;
    private void Awake()
    {
        instance = this;
    }
    public static void Show(string content)
    {
        instance.tooltip.SetText(content);
        instance.tooltip.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
    }
}
