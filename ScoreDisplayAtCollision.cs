using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayAtCollision : MonoBehaviour
{
    //Text objects to update 
    public Text scoreAtEndGameplay;//score for this game
    public Text highScoreTextAtEndGameplay;//high score in general
    public Text coinsEarnedThisRound;//to show the coins earned
    public Text rewardedVideoAdText;//to show how much the player will get
    public GameObject askToWatchAd;//to show the question whether or not they want to watch the ad
    public GameObject rewardedVideoGameObject;

    public RandomGeneratingObstacles generator;//to get the score for this round

    //for saving coins
    private decimal coinsThisRound;
    //rewarded video
    public AdManager adManager;
    public int videoAdReward;

    private void Start()
    {
        decimal randomNumberForReward = (decimal)UnityEngine.Random.Range(1.25f, 2f);
        videoAdReward = Convert.ToInt32(Math.Round(coinsThisRound / randomNumberForReward));
        rewardedVideoAdText.text = "+" + Convert.ToString(videoAdReward);
    }
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
    public void REwardedVideoButtonQuestion()
    {
        askToWatchAd.SetActive(true);
    }
    public void RewardedVideoButton()
    {
        adManager.DisplayVideoAd();
        rewardedVideoGameObject.SetActive(false);
        askToWatchAd.SetActive(false);
        //to show the money that the player made from the ad
        coinsEarnedThisRound.text = Convert.ToString(coinsThisRound + videoAdReward);
    }
    public void CloseAdQuestion()
    {
        askToWatchAd.SetActive(false);
    }
}
