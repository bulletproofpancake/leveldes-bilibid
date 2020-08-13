using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scene0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
