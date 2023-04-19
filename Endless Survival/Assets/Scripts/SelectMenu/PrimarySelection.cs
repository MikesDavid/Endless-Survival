using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrimarySelection : MonoBehaviour
{

    public GameObject[] primary;


    public int selectedPrimary = 0;

    public void NextPrimary()
    {
        primary[selectedPrimary].SetActive(false);
        selectedPrimary = (selectedPrimary + 1) % primary.Length;
        primary[selectedPrimary].SetActive(true);
    }

    public void PreviousPrimary()
    {
        primary[selectedPrimary].SetActive(false);
        selectedPrimary--;
        if (selectedPrimary < 0)
        {
            selectedPrimary += primary.Length;
        }
        primary[selectedPrimary].SetActive(true);
    }


    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedPrimary", selectedPrimary);
    }
}
