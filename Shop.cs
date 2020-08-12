using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    //shop and endscreen objects
    public GameObject shop;
    public GameObject endScreen;
    public GameObject goToShopButton;
    public GameObject[] buttons;
    public GameObject closeShopButton;
    //public Animation closeShop;
    //public GameObject closeShopController;
    
    //save and load
    public GameManager gameManager;

    //For shop upgrades:
    //public TimeManager timeManager;

    //missile
    public int missileUpgradeValue;
    public int maxMissileUpgradeValue = 1;
    //slow-motion
    public int slowMotionUpgradeValue;
    private int maxSlowMotionUpgradeValue;

    public void ShowShop()
    {
        shop.SetActive(true);
        endScreen.SetActive(false);
    }
    public void ShowShopButton()
    {
        goToShopButton.SetActive(true);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowShopButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }
    public void UpgradeSlowMotion()
    {
        //adds 10% to slowdown factor
        //timeManager.slowDownFactor += timeManager.slowDownFactor / 10;
        //slowMotionUpgradeValue += 1;
        gameManager.SaveProgress();
    }
    public void UpgradeMissile()
    {
        if (missileUpgradeValue < maxMissileUpgradeValue)
        {
            missileUpgradeValue += 1;
            gameManager.SaveProgress();
        }
    }
}
