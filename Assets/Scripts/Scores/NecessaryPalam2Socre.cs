using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecessaryPalam2Socre : MonoBehaviour
{
    PlayerHealthManager playerHealthManager;
    SceneTimeCounter sceneTimeCounter;
    LocalSceneManager sceneManager;

    public int GetScore()
    {
        var time = sceneTimeCounter.time;
        var plHp = playerHealthManager.GetHp();
        var difficultyLevel = sceneManager.data.difficultyLevel;

        return ScoreCalculator.Instance.ScoreCalculation(time,plHp,difficultyLevel);
    }
}
