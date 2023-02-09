using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTerrain : MonoBehaviour
{

    public void Sand()
    {
        PlayerScript.playerSpeed -= 3;
        PlayerScript.dashDistance -= 0.5f;
    }
}
