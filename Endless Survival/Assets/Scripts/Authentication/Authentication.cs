using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Authentication : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    [SerializeField] private TMP_Text ErrorMessage;
    [SerializeField] private GameObject MainPage;
    [SerializeField] private GameObject MainMenu;

    public Button submitButton;

    public void CallLogIn()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new ("http://localhost/sqlconnect/Login.php", form);
        yield return www;
        Debug.Log(www.text[0]);
        if (www.text[0] == '0')
        {
            DBManager.username = nameField.text;
            MainPage.SetActive(false);
            MainMenu.SetActive(true);
            Debug.Log(DBManager.username);
        }
        if (www.text[0] == '4' || www.text[0] == '3')
        {
            ErrorMessage.text = "Wrong Username or Password!";
        }

    }
}
