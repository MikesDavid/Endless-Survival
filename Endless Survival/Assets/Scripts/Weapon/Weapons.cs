using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapons : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float fireRate, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, burstAmount;
    public bool allowButtonHold, allowBurst, allowDifferentMode, burstNormalMode, burstAutoMode,
        autoNormalMode, isShotgun;
}
