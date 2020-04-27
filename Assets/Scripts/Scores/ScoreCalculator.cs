using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : SingletonMonoBehaviour<ScoreCalculator>
{
    /*
        時間、プレイヤーの残りHP、ステージの難易度とかと基本スコアを良い感じにしてスコア算出
    */

    const int BASIC_SCORE = 340000;

    public int ScoreCalculation(float time, int plHp, int difficultyLevel)
    {
        var calcuTime = time / 10.0f + 1.0f;
        var calcuPlHp = (float)plHp / 10.0f + 1.0f;
        var calcuLevel = (float)difficultyLevel / 10.0f + 1.0f;

        int score = (int)(calcuTime * calcuPlHp * calcuLevel * BASIC_SCORE);

        return score;
    }
}