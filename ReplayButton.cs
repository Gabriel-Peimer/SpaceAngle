using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    public RandomGeneratingObstacles obstacleGeneration;
    public float restartDelay = 1f;
    public GameObject replayButton;
    public GameObject healthBar;

    public void Replay()
    {
        Invoke("Restart", restartDelay);
    }
    public void Restart()
    {
        obstacleGeneration.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowButton()
    {
        replayButton.SetActive(true);
    }
    public void HideHealthBar()
    {
        healthBar.SetActive(false);
    }
}