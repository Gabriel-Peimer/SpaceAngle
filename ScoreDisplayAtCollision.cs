using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayAtCollision : MonoBehaviour
{
    //Text objects to update 
    public Text scoreAtEndGameplay;//score for this game
    public Text highScoreTextAtEndGameplay;//high score in general
    public Text coinsEarnedThisRound;//to show the coins earned

    public RandomGeneratingObstacles generator;//to get the score for this round

    //for saving coins
    private decimal coinsThisRound;

    public void TextUpdate()
    {
        scoreAtEndGameplay.text = Convert.ToString(generator.score);
    }
    public void SetHighScore(GameMaster gameMaster)
    {
        highScoreTextAtEndGameplay.text = gameMaster.playerHighScore.ToString();
    }
    public void CoinCountUpdate(GameMaster gameMaster)
    {
        CountCoins(gameMaster);
        coinsEarnedThisRound.text = Convert.ToString(coinsThisRound);
    }
    //coin system
    private void CountCoins(GameMaster gameMaster)
    {
        //so that it takes some effort to get coins...
        coinsThisRound += Math.Round((decimal)generator.score / 2);
        gameMaster.coinCount += (float)coinsThisRound;
        
        GameManager.SaveProgress(gameMaster);//saving coins
    }
}
