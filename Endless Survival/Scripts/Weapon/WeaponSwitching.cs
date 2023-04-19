using System;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int SelectedWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    
    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = SelectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            SelectedWeapon = 1;
        }

        if (previousSelectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }
    }
    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == SelectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
