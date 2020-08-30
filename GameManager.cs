using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameHasEnded = false;
    
    //for the gameover animation
    public GameObject gameEndUI;
    public ScoreDisplayAtCollision scoreDisplayAtCollision;//keeps the score

    public Rigidbody player;//so that the player doesn't stay frozen (turning off constraints)
    
    //gameMaster and timeManager
    private GameObject gameMasterObject;
    private GameMaster gameMaster;
    public TimeManager timeManager;

    //scripts and objects that need to be turned off at the end off the game
    public PlayerMovement movement;
    public RandomGeneratingObstacles obstacleGeneration;
    public HealthBar healthBar;
    public PlayerHealthHandling playerHealthHandling;
    public ObstacleMovement obstacleMovement;
    public Missile missileScript;

    private void Awake()
    {
        gameMasterObject = GameObject.Find("GameMaster");
        gameMaster = gameMasterObject.GetComponent<GameMaster>();
    }
    public void GameOver()
    {
        if (!gameHasEnded)
        {
            player.constraints = RigidbodyConstraints.None;

            //disables movement, healthbar and obstacles
            movement.enabled = false;
            playerHealthHandling.enabled = false;
            healthBar.enabled = false;
            obstacleGeneration.enabled = false;
            obstacleMovement.enabled = false;

            //in case the player loses while in slow-motion
            Time.timeScale = 1f;
            timeManager.shouldSlowMmotionStop = true;
            
            //sets gameHasEnded to true so that we don't keep repeating this proccess
            gameHasEnded = true;

            //Displays score at the end of the game
            scoreDisplayAtCollision.TextUpdate();
            CheckForHighScore();
            scoreDisplayAtCollision.SetHighScore(gameMaster);
            scoreDisplayAtCollision.CoinCountUpdate(gameMaster);

            //save progress (for high-score)
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
        gameMaster.slowMotionUpgradeValue = data.slowMotionUpgradeValue;
        gameMaster.scoreSpeedUpgradeValue = data.scoreSpeedUpgradeValue;
        //high-score
        gameMaster.playerHighScore = data.playerHighScore;
        //coins
        gameMaster.coinCount = data.coinCount;
    }
    private void CheckForHighScore()
    {
        if (obstacleGeneration.score > gameMaster.playerHighScore)
        {
            gameMaster.playerHighScore = obstacleGeneration.score;//new high-score
        }
    }
}
