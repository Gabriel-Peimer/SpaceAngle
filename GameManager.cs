using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public ScoreDisplayAtCollision scoreDisplayAtCollision;//keeps the score
    public GameObject gameEndUI;//called when player dies
    public Shop shop;//for save and load
    public Rigidbody player;//so that the player doesn't stay frozen (turning off constraints)

    //scripts and objects that need to be turned off at the end off the game
    public PlayerMovement movement;
    public RandomGeneratingObstacles obstacleGeneration;
    public HealthBar healthBar;
    public PlayerHealthHandling playerHealthHandling;

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
        }
    }
    public void PlayerLost()
    {
        gameEndUI.SetActive(true);
    }

    //Save and load system
    public void SaveProgress()
    {
        SavePlayerData.SavePlayerProgress(shop);
    }
    public void LoadProgress()
    {
        PlayerData data = SavePlayerData.LoadPlayerData();
        shop.missileUpgradeValue = data.missileUpgradeValue;
        shop.slowMotionUpgradeValue = data.slowMotionUpgradeValue;
    }
}
