using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shop;
    public GameObject endScreen;
    public GameObject goToShopButton;
    public GameObject[] buttons;
    public GameObject closeShopButton;
    private Animation closeShop;
    public GameObject closeShopController;

    //For shop upgrades:
    public TimeManager timeManager;

    private int missileUpgradeValue;
    private int slowMotionUpgradeValue;
    private bool hasMissileUpgrade;

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
        //closeShop = closeShopButton.GetComponent<Animation>();
        //closeShopController.SetActive(true);
        //closeShop.Play("CloseShop");
        //if (closeShop.isPlaying == false)
        //{
            shop.SetActive(false);
            endScreen.SetActive(true);
        //}
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
        timeManager.slowDownFactor += timeManager.slowDownFactor / 10;
        slowMotionUpgradeValue += 1;
    }
    public void UpgradeMissile()
    {
        if (missileUpgradeValue < 1)// this is the first upgrade
        {
            hasMissileUpgrade = true;
        }
    }
}
