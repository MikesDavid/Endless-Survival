using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSecondary : MonoBehaviour
{
    public int selectedSecondary = 0;
    void Start()
    {
        selectedSecondary = PlayerPrefs.GetInt("selectedSecondary");
        SelectWeapon();
    }
    private void SelectWeapon()
    {
        
        int i = 0;
        foreach (Transform weapon in transform)
        {
            
            if (i == selectedSecondary)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }

    }
}
