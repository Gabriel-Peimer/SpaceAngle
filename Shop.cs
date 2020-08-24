using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    //shop and endscreen objects
    public GameObject shop;
    private GameObject audioManager;
    private GameMaster gameMaster;

    //in order to see how many coins we have
    private int coinCount;
    public Text coinCountText;
    //no funds...
    public GameObject NotEnoughFunds;
    private float showNotEnoughFundsStart;
    private float showNotEnoughFundsTime = 1f;

    //missile
    public int missileUpgradeValue;
    public int maxMissileUpgradeValue = 4;
    private int[] missileUpgradePrice = { 0, 10, 250, 400, 800 };
    public GameObject[] missileUpgradeIndicators;
    public GameObject missileMaxText;
    //slow-motion
    public int slowMotionUpgradeValue;
    private int maxSlowMotionUpgradeValue = 4;
    private int[] slowMotionUpgradePrice = { 0, 10, 60, 180, 360 };
    public GameObject[] slowMotionUpgradeIndicators;
    public GameObject slowMotionMaxText;
    //colors for indicators
    Color32 whiteColor = new Color32(255, 255, 255, 255);

    private void Awake()
    {
        //setting max upgrades
        maxMissileUpgradeValue = missileUpgradePrice.Length - 1;
        maxSlowMotionUpgradeValue = slowMotionUpgradePrice.Length - 1;

        //gameMaster
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        //audioManager
        audioManager = GameObject.Find("AudioManager");

        GameManager.gameHasEnded = false;//otherwise the game cannot end once a player goes to shop

        //showing the coin count
        coinCountText.text = gameMaster.coinCount.ToString();

        //checking if to show max text for upgrades
        CheckMaxMissile();
        CheckMaxSlowMotion();
        //lighting up indicators
        LightUpIndicators(missileUpgradeIndicators, gameMaster.missileUpgradeValue);
        LightUpIndicators(slowMotionUpgradeIndicators, gameMaster.slowMotionUpgradeValue);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(audioManager);
    }
    public void UpgradeSlowMotion()
    {
        if (gameMaster.slowMotionUpgradeValue < maxSlowMotionUpgradeValue)
        {
            //checking that the player has the appropriate number of coins
            if (gameMaster.coinCount >= slowMotionUpgradePrice[gameMaster.slowMotionUpgradeValue])
            {

                //taking away the coins for the purchase
                //adding 1 because that at the moment the upgrade value is 1 less than the 
                //one the player bought
                gameMaster.coinCount -= slowMotionUpgradePrice[gameMaster.slowMotionUpgradeValue + 1];

                gameMaster.slowMotionUpgradeValue++;
                GameManager.SaveProgress(gameMaster);

                //updating coin count on screen
                coinCountText.text = gameMaster.coinCount.ToString();

                CheckMaxSlowMotion();
                LightUpIndicators(slowMotionUpgradeIndicators, gameMaster.slowMotionUpgradeValue);
            }
            else
            {
                NotEnoughFunds.SetActive(true);
                showNotEnoughFundsStart = Time.timeSinceLevelLoad;
            }
        }
    }
    public void UpgradeMissile()
    {
        if (gameMaster.missileUpgradeValue < maxMissileUpgradeValue)
        {
            //checking that the player has the appropriate number of coins
            if (gameMaster.coinCount >= missileUpgradePrice[gameMaster.missileUpgradeValue])
            {

                //taking away the coins for the purchase
                //adding 1 because that at the moment the upgrade value is 1 less than the 
                //one the player bought
                gameMaster.coinCount -= missileUpgradePrice[gameMaster.missileUpgradeValue + 1];

                gameMaster.missileUpgradeValue++;
                GameManager.SaveProgress(gameMaster);

                //updating coin count on screen
                coinCountText.text = gameMaster.coinCount.ToString();

                CheckMaxMissile();
                LightUpIndicators(missileUpgradeIndicators, gameMaster.missileUpgradeValue);
            }
            else
            {
                NotEnoughFunds.SetActive(true);
                showNotEnoughFundsStart = Time.timeSinceLevelLoad;
            }
        }
    }
    private void Update()
    {
        if (Time.timeSinceLevelLoad - showNotEnoughFundsStart >= showNotEnoughFundsTime)
        {
            NotEnoughFunds.SetActive(false);
        }
    }
    private void CheckMaxSlowMotion()
    {
        //checking if we now have max upgrade
        if (gameMaster.slowMotionUpgradeValue == maxSlowMotionUpgradeValue)
        {
            slowMotionMaxText.SetActive(true);
        }
    }
    private void CheckMaxMissile()
    {
        //checking if we now have max upgrade
        if (gameMaster.missileUpgradeValue == maxMissileUpgradeValue)
        {
            missileMaxText.SetActive(true);
        }
    }
    private void LightUpIndicators(GameObject[] indicators, int upgradeValue)
    {
        if (upgradeValue > 0)
        {
            for (int i = 0; i < upgradeValue; i++)
            {
                if (indicators[i].GetComponent<Image>().color != whiteColor)
                {
                    indicators[i].GetComponent<Image>().color = whiteColor;
                }
            }
        }
    }
}
