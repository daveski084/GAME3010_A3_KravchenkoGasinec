using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButtons : MonoBehaviour
{
    public void OnEasyButtonPressed()
    {
        Tile.difficulty = 2;
        GameManager.moves = 30;
        GameManager.goalSet = 30;
        GameManager.isDifficultySet = true;
        GameManager.canTimerStart = true;
    }

    public void OnMedButtonPressed()
    {
        Tile.difficulty = 3;
        GameManager.moves = 15;
        GameManager.goalSet = 20;
        GameManager.isDifficultySet = true;
        GameManager.canTimerStart = true;
    }

    public void OnHardButtonPressed()
    {
        Tile.difficulty = 4;
        GameManager.moves = 10;
        GameManager.goalSet = 10;
        GameManager.isDifficultySet = true;
        GameManager.canTimerStart = true;
    }
}
