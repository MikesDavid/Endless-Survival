using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    public void GoToRegistration()
    {
        Application.OpenURL("http://127.0.0.1:8000/");
    }
}
