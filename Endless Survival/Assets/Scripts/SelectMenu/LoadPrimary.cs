using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPrimary : MonoBehaviour
{
    public int selectedPrimary = 0;
    void Start()
    {
        selectedPrimary = PlayerPrefs.GetInt("selectedPrimary");
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            
            if (i == selectedPrimary)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }

    }
}
