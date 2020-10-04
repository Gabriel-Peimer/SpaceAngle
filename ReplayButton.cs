using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{   
    public SceneLoader sceneLoader;
    private GameMaster gameMaster;//to check if missile upgrade is greater than 0

    //things to hide/show
    public GameObject healthBar;
    public Image missileIndicator;
    public GameObject replayButton;
    public GameObject goToShopButton;
    public GameObject rewardedVideoAdButton;

    private void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
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
        if (gameMaster.missileUpgradeValue > 0) missileIndicator.enabled = false;
    }
    public void BackButton()
    {
        sceneLoader.LoadSceneByName("MainMenu", "Start");
    }
}