using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    public RandomGeneratingObstacles obstacleGeneration;
    public float restartDelay = 1f;

    //things to hide
    public GameObject healthBar;
    public Image missileIndicator;

    public GameObject replayButton;
    public GameObject goToShopButton;

    public void Replay()
    {
        Invoke("Restart", restartDelay);
    }
    public void Restart()
    {
        obstacleGeneration.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void ShowButtons()
    {
        replayButton.SetActive(true);
        goToShopButton.SetActive(true);
    }
    public void HideHealthBar()
    {
        healthBar.SetActive(false);
        missileIndicator.enabled = false;
    }
}