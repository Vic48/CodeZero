using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject howtoplayPanel;

    void Start()
    {
        howtoplayPanel.SetActive(false);     
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void HowToPlay()
    {
        howtoplayPanel.SetActive(true);
    }
}
