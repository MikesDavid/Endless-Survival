using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject options;

    private void Start()
    {
        StateManager.OnSceneChanged += SceneManager_OnSceneChanged;
    }

    private void SceneManager_OnSceneChanged(GameScene state)
    {
        if (state == GameScene.Start)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Timescale: 1");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
