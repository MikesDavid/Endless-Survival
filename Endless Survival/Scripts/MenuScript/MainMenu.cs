using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private Button quitGame;
    public void PlayGame()
    {
        DisableButtons();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        DisableButtons();
        Debug.Log("Quit");
        Application.Quit();
    }

    private void DisableButtons()
    {
        startGame.interactable = false;
        quitGame.interactable = false;
    }
}
