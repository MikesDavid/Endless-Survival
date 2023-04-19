using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondarySelection : MonoBehaviour
{

    public GameObject[] secondary;

    public int selectedSecondary = 0;

    
    public void NextSecondary()
    {
        secondary[selectedSecondary].SetActive(false);
        selectedSecondary = (selectedSecondary + 1) % secondary.Length;
        secondary[selectedSecondary].SetActive(true);
    }

    public void PreviousSecondary()
    {
        secondary[selectedSecondary].SetActive(false);
        selectedSecondary--;
        if (selectedSecondary < 0)
        {
            selectedSecondary += secondary.Length;
        }
        secondary[selectedSecondary].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedSecondary", selectedSecondary);
    }
}
