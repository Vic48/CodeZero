using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject howtoplayPanel;

    public bool pausedGame;
    public GameObject pauseMenu; 
    public GameObject gameOver;

    void Start()
    {
        howtoplayPanel.SetActive(false);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausedGame == false)
            {
                PauseGame();
            }
            else
            {
                Resume();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pausedGame = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pausedGame = false;
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
