using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    public Button level1, level2, level3;

    void Start()
    {
        level1.interactable = true;
        level2.interactable = true;
        level3.interactable = true;
    }

    //hard coded version
    public void LevelToLoad(int level)
    {

        SceneManager.LoadScene(level);

    }
}
