using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBReadWrite : MonoBehaviour
{
    private void FixedUpdate()
    {
        Debug.Log(DBManager.username);
    }
    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new();
        //form.AddField("save", DBManager.save);
        form.AddField("name", DBManager.username);
        form.AddField("szint", DBManager.szint);
        form.AddField("tulelesido", DBManager.tulelesido);
        form.AddField("death", DBManager.death);
        form.AddField("primaryWeapon", DBManager.primary);
        form.AddField("SecondaryWeapon", DBManager.secondary);
        form.AddField("damageTaken", DBManager.damageTagen);
        form.AddField("kills", DBManager.kills);

        WWW www = new("http://localhost/sqlconnect/savedata.php");

        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Game data saved");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }
    }
}
