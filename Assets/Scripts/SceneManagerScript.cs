using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void QuitGame()
    {

        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("berkcantest");


    }


    public void MainMenu()
    {

        SceneManager.LoadScene("MainMenuScreen");
    }


    public void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);

        


    }
}
