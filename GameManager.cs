using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public ScoreDisplayAtCollision scoreDisplayAtCollision;//keeps the score
    public GameObject gameEndUI;//called when player dies
    public Rigidbody player;//so that the player doesn't stay frozen (turning off constraints)
    private GameObject gameMasterObject;
    private GameMaster gameMaster;

    //scripts and objects that need to be turned off at the end off the game
    public PlayerMovement movement;
    public RandomGeneratingObstacles obstacleGeneration;
    public HealthBar healthBar;
    public PlayerHealthHandling playerHealthHandling;

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
            healthBar.gameObject.SetActive(false);
            obstacleGeneration.enabled = false;
            
            //sets gameHasEnded to true so that we don't keep repeating this proccess
            gameHasEnded = true;

            //Displays score at the end of the game
            scoreDisplayAtCollision.TextUpdate();
            CheckForHighScore();
            scoreDisplayAtCollision.SetHighScore(gameMaster);

            //save progress (for high-score)
            SaveProgress(gameMaster);
        }
    }
    public void PlayerLost()
    {
        gameEndUI.SetActive(true);
    }

    //Save and load system
    public static void SaveProgress(GameMaster gameMaster)
    {
        SavePlayerData.SavePlayerProgress(gameMaster);
    }
    public static void LoadProgress(GameMaster gameMaster)
    {
        PlayerData data = SavePlayerData.LoadPlayerData();
        gameMaster.missileUpgradeValue = data.missileUpgradeValue;
        gameMaster.slowMotionUpgradeValue = data.slowMotionUpgradeValue;
        gameMaster.playerHighScore = data.playerHighScore;
    }
    private void CheckForHighScore()
    {
        if (obstacleGeneration.score > gameMaster.playerHighScore)
        {
            gameMaster.playerHighScore = obstacleGeneration.score;//new high-score
        }
    }
}
