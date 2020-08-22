using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{   
    public float restartDelay = 1f;//delay so that the player can see the ship exploding

    //things to hide/show
    public GameObject healthBar;
    public Image missileIndicator;
    public GameObject replayButton;
    public GameObject goToShopButton;

    public void Replay()
    {
        Invoke("Restart", restartDelay);
        GameManager.gameHasEnded = false;
    }
    public void Restart()
    {
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