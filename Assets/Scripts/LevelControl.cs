using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    public GameObject levelSelection;

    private void Start()
    {
        levelSelection.SetActive(true);
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
