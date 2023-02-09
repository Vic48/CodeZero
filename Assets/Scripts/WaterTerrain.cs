using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTerrain : MonoBehaviour
{
    public void Water()
    {
        PlayerScript.playerSpeed = 1f;
        PlayerScript.dashDistance = 0f;
    }
}
