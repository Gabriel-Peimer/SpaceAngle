using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{   
    public SceneLoader sceneLoader;

    //things to hide/show
    public GameObject healthBar;
    public Image missileIndicator;
    public GameObject replayButton;
    public GameObject goToShopButton;
    public GameObject rewardedVideoAdButton;

    public void Replay()
    {
        sceneLoader.LoadSceneByName("Gameplay", "Start");
        GameManager.gameHasEnded = false;
    }
    public void GoToShop()
    {
        sceneLoader.LoadSceneByName("Shop", "Start");
    }
    public void ShowButtons()
    {
        replayButton.SetActive(true);
        goToShopButton.SetActive(true);
        rewardedVideoAdButton.SetActive(true);

    }
    public void HideHealthBar()
    {
        healthBar.SetActive(false);
        missileIndicator.enabled = false;
    }
}