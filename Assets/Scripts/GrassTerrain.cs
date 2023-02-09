using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTerrain : MonoBehaviour
{
    public void Grass()
    {
        PlayerScript.playerSpeed += 3;
        PlayerScript.dashDistance += 0.5f;
    }
}
