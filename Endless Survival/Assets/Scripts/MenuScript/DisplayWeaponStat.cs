using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayWeaponStat : MonoBehaviour
{
    [SerializeField] private Weapons weapon;
    private TextMeshProUGUI weaponStat;
    private string differentmode;
    private int damage;

    private void Start()
    {
        weaponStat = this.GetComponent<TextMeshProUGUI>();
        differentmode = weapon.allowDifferentMode ? "Yes" : "No";
        if (weapon.isShotgun)
            damage = weapon.damage * weapon.bulletsPerTap;
        else 
            damage = weapon.damage;
        weaponStat.text = $"\t{weapon.weaponName}\n" +
            $"Sebzés: {damage}, Magazine: {weapon.magazineSize}\n" +
            $"Firerate: {weapon.timeBetweenShots}, Reloadtime: {weapon.reloadTime}\n" +
            $"Different fire mode: {differentmode}";
    }

}
