using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject options;
    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
            gameMenu.SetActive(true);
            Debug.Log("Timescale: 0");
            options.SetActive(false);
        }

    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ReturnToGame()
    {
        Debug.Log("Timescale: 1");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
