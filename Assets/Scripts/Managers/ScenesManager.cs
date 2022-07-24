using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    private void OnEnable()
    {
        GameManager.OnGameReset += RestartCurrentScene;
        GameManager.OnMainMenuReturn += MainMenu;
    }

    private void OnDisable()
    {
        GameManager.OnGameReset -= RestartCurrentScene;
        GameManager.OnMainMenuReturn -= MainMenu;
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene(SceneConstants.RACE_SCENE_NAME);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneConstants.MAIN_MENU_SCENE_NAME);
    }

    public void RestartCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
