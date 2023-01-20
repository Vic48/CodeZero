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
    public GameObject optionsPanel;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuBGM");
        howtoplayPanel.SetActive(false);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        optionsPanel.SetActive(false);
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
        FindObjectOfType<AudioManager>().StopPlaying("GameBGM");
        FindObjectOfType<AudioManager>().Play("MenuBGM");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pausedGame = false;
        FindObjectOfType<AudioManager>().StopPlaying("MenuBGM");
        FindObjectOfType<AudioManager>().Play("GameBGM");

    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<AudioManager>().Play("MenuBGM");
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
        FindObjectOfType<AudioManager>().Play("MenuBGM");
    }

    public void HowToPlay()
    {
        howtoplayPanel.SetActive(true);
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }
}
