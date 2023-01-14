using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    // the only gameData in the game will be accessible for convenience
    private static GameData gameData;

    public static GameData GetGameData()
    {
        if (gameData == null)
        {
            gameData = new GameData();
        }

        return gameData;
    }
}
