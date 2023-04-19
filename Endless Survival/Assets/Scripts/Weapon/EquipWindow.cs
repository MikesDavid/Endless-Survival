using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWindow : MonoBehaviour
{
    [SerializeField] private Player player;

    private LevelSystem levelSystem;
    private void Awake()
    {
        //transform.Find("Shotgun").GetComponent<Button_UI>().ClickFunc = () =>{
        //if(levelSystem.GetLevelNumber() >= 3)
        //  player.SetEquip(player.Equip.'Shotgun')};
        //transform.Find("Rifle").GetComponent<Button_UI>().ClickFunc = () => player.SetEquip(player.Equip.'Rifle');
        //transform.Find("Minigun").GetComponent<Button_UI>().ClickFunc = () => {
        //if(levelSystem.GetLevelNumber() >= 4)
        //   player.SetEquip(player.Equip.'Minigun')};
        //transform.Find("SubmachineGun").GetComponent<Button_UI>().ClickFunc = () => {
        //if(levelSystem.GetLevelNumber() >= 3)
        //  player.SetEquip(player.Equip.'SubmachineGun')};
        //transform.Find("Pistol").GetComponent<Button_UI>().ClickFunc = () => player.SetEquip(player.Equip.'Pistol');

    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

}
