using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    public GameObject levelSelection;
    public GameObject playerSelection;

    private void Start()
    {
        playerSelection.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void LevelSelect()
    {
        playerSelection.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void player1()
    {
        LevelSelect();
        PlayerPrefs.SetString("Player", "P1");
    }

    public void player2()
    {
        LevelSelect();
        PlayerPrefs.SetString("Player", "P2");
    }

    public void player3()
    {
        LevelSelect();
        PlayerPrefs.SetString("Player", "P3");
    }

    public void levelOne()
    {
        PlayerPrefs.SetString("levelId", "1");
        SceneManager.LoadScene("Level 1");
    }

    public void levelTwo()
    {
        PlayerPrefs.SetString("levelId", "2");
        SceneManager.LoadScene("Level 1");
    }

    public void levelThree()
    {
        PlayerPrefs.SetString("levelId", "3");
        SceneManager.LoadScene("Level 1");
    }
}
