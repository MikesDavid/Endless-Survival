using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkillChanges : MonoBehaviour
{
    private Button_UI buton;
    private void Start()
    {
        transform.GetComponent<Button_UI>().ClickFunc = () => { DataPersistenceManager.instance.SaveGame(); };
    }
}
