using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public ScoreDisplayAtCollision scoreDisplayAtCollision;
    public RandomGeneratingObstacles obstacleGeneration;
    public GameObject gameEndUI;
    public PlayerMovement movement;
    public Rigidbody player;
    public HealthBar healthBar;

    public void GameOver()
    {
        if (!gameHasEnded)
        {
            player.constraints = RigidbodyConstraints.None;

            //disables movement, healthbar and obstacles
            movement.enabled = false;
            healthBar.gameObject.SetActive(false);
            obstacleGeneration.enabled = false;
            gameHasEnded = true;

            //Displays score at the end of the game
            scoreDisplayAtCollision.TextUpdate();
        }
    }
    public void PlayerLost()
    {
        gameEndUI.SetActive(true);
    }
}
