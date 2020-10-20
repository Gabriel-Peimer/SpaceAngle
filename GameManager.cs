using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameHasEnded = false;
    
    //for the gameover animation
    public GameObject gameEndUI;
    public ScoreDisplayAtCollision scoreDisplayAtCollision;//keeps the score

    public Rigidbody player;//so that the player doesn't stay frozen (turning off constraints)
    
    //gameMaster adManager and timeManager 
    private GameObject gameMasterObject;
    private GameMaster gameMaster;
    public TimeManager timeManager;
    public AdManager adManager;

    //scripts and objects that need to be turned off/on at the beginning/end of the game
    public PlayerMovement movement;
    public RandomGeneratingObstacles obstacleGeneration;
    public ObstacleMovement obstacleMovement;
    public Missile missileScript;
    public Intro introScript;
    public GameObject joystick;

    //for ads/ratings
    public int gamesBetweenAds = 5;

    private void Awake()
    {
        gameMasterObject = GameObject.Find("GameMaster");
        gameMaster = gameMasterObject.GetComponent<GameMaster>();
        //for intro
        if (gameMaster.isIntroScene)
        {
            introScript.enabled = true;
        }
    }
    public void GameOver()
    {
        if (!gameHasEnded)
        {
            //sets gameHasEnded to true so that we don't keep repeating this proccess
            gameHasEnded = true;

            player.constraints = RigidbodyConstraints.None;

            //disables movement, healthbar, obstacles, missile indicator etc.
            movement.enabled = false;
            obstacleGeneration.enabled = false;
            obstacleMovement.enabled = false;
            missileScript.missileIndicator.SetActive(false);
            joystick.SetActive(false);

            //in case the player loses while in slow-motion
            Time.timeScale = 1f;
            timeManager.shouldSlowMotionStop = true;

            //Displays score at the end of the game
            scoreDisplayAtCollision.TextUpdate();
            CheckForHighScore();
            scoreDisplayAtCollision.SetHighScore(gameMaster);
            scoreDisplayAtCollision.CoinCountUpdate(gameMaster);

            //checking if to play ads
            gameMaster.gameCount++;
            if (gameMaster.gameCount % gamesBetweenAds == 0)//play ad after x games
            {
                GameObject.Find("AdManager").GetComponent<AdManager>().DisplayVideoAd();
            }
            else if (gameMaster.isAskForRatingOff != true && gameMaster.gameCount % gameMaster.gamesBetweenRatingRequest == 0)
            {
                adManager.askForRatingObject.SetActive(true);
            }
            //save progress (for high-score & gameCount)
            SaveProgress(gameMaster);

            gameEndUI.SetActive(true);
        }
    }

    //Save and load system
    public static void SaveProgress(GameMaster gameMaster)
    {
        SavePlayerData.SavePlayerProgress(gameMaster);
    }
    public static void LoadProgress(GameMaster gameMaster)
    {
        PlayerData data = SavePlayerData.LoadPlayerData();
        //upgrades
        gameMaster.missileUpgradeValue = data.missileUpgradeValue;
        gameMaster.shipSpeedUpgradeValue = data.slowMotionUpgradeValue;
        gameMaster.scoreSpeedUpgradeValue = data.scoreSpeedUpgradeValue;
        //high-score
        gameMaster.playerHighScore = data.playerHighScore;
        //coins
        gameMaster.coinCount = data.coinCount;
        //settings
        gameMaster.isMusicEnabled = data.isMusicEnabled;
        gameMaster.areSoundsEnabled = data.areSoundsEnabled;
        //ads
        gameMaster.gameCount = data.gameCount;
        //touch controls
        gameMaster.isJoystickActive = data.isJoystickActive;
        //rating
        gameMaster.isAskForRatingOff = data.isAskForRatingOff;
    }
    private void CheckForHighScore()
    {
        if (obstacleGeneration.score > gameMaster.playerHighScore)
        {
            gameMaster.playerHighScore = obstacleGeneration.score;//new high-score
        }
    }
}
