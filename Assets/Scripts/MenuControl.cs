using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject Credits;

    public void ShowCredits()
    {
        Credits.SetActive(true);
    }

    public void ShowTitle()
    {
        Credits.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGmae()
    {
        Application.Quit();
    }
}
