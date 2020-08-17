using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayAtCollision : MonoBehaviour
{
    public Text scoreAtEndGameplay;//score for this game
    public Text highScoreTextAtEndGameplay;//high score in general
    public RandomGeneratingObstacles generator;

    public void TextUpdate()
    {
        scoreAtEndGameplay.text = Convert.ToString(generator.score);
    }
    public void SetHighScore(GameMaster gameMaster)
    {
        highScoreTextAtEndGameplay.text = gameMaster.playerHighScore.ToString();
    }
}
