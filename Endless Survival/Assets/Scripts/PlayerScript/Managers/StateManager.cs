using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public static event Action<GameScene> OnSceneChanged;

    public GameScene State;

    private void Awake()
    {
        instance = this;
    }
    public void CurrentScene(GameScene gameScene)
    {
        State=gameScene;
        switch (gameScene)
        {
            case GameScene.Lobby:
                UnlockCursor();
            break;
            case GameScene.Start:
            break;
            case GameScene.Defeat:
                UnlockCursor();
            break;
            case GameScene.Wictory:
                UnlockCursor();
            break;
        }
        OnSceneChanged?.Invoke(gameScene);

    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

public enum GameScene{
    Lobby,
    Start,
    Defeat,
    Wictory,

}
