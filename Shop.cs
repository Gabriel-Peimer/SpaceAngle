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

    //Upgrades
    //missile
    public int missileUpgradeValue;
    private int maxMissileUpgradeValue = 4;
    private int[] missileUpgradePrice = { 0, 125, 250, 400, 800 };
    public GameObject[] missileUpgradeIndicators;
    public GameObject missileMaxText;
    //slow-motion
    public int slowMotionUpgradeValue;
    private int maxSlowMotionUpgradeValue = 4;
    private int[] slowMotionUpgradePrice = { 0, 10, 60, 180, 360 };
    public GameObject[] slowMotionUpgradeIndicators;
    public GameObject slowMotionMaxText;
    //score-speed
    public int scoreSpeedUpgradeValue;
    private int maxScoreSpeedUpgradeValue = 4;
    private int[] scoreSpeedUpgradePrice = { 0, 30, 75, 220, 500 };
    public GameObject[] scoreSpeedUpgradeIndicators;
    public GameObject scoreSpeedMaxText;

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
        CheckMaxUpgrade(gameMaster.scoreSpeedUpgradeValue, maxScoreSpeedUpgradeValue, scoreSpeedMaxText);
        CheckMaxUpgrade(gameMaster.slowMotionUpgradeValue, maxSlowMotionUpgradeValue, slowMotionMaxText);
        CheckMaxUpgrade(gameMaster.missileUpgradeValue, maxMissileUpgradeValue, missileMaxText);

        //lighting up indicators
        LightUpIndicators(missileUpgradeIndicators, gameMaster.missileUpgradeValue);
        LightUpIndicators(slowMotionUpgradeIndicators, gameMaster.slowMotionUpgradeValue);
        LightUpIndicators(scoreSpeedUpgradeIndicators, gameMaster.scoreSpeedUpgradeValue);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(audioManager);
    }
    private string Upgrade(int upgradeValue, int maxUpgradeValue, int[] upgradePriceArray, GameObject maxText, GameObject[] upgradeIndicators)
    {
        if (upgradeValue < maxUpgradeValue)
        {
            //checking that the player has the appropriate number of coins
            if (gameMaster.coinCount >= upgradePriceArray[upgradeValue])
            {

                //taking away the coins for the purchase
                //adding 1 because that at the moment the upgrade value is 1 less than the 
                //one the player bought
                gameMaster.coinCount -= upgradePriceArray[upgradeValue + 1];

                //updating coin count on screen
                coinCountText.text = gameMaster.coinCount.ToString();

                CheckMaxUpgrade(upgradeValue, maxUpgradeValue, maxText);
                //+1 in the next line because that we didn't upgrade yet
                LightUpIndicators(upgradeIndicators, upgradeValue + 1);

                return "Y";
            }
            else
            {
                NotEnoughFunds.SetActive(true);
                showNotEnoughFundsStart = Time.timeSinceLevelLoad;
                return "N";
            }
        }return "N";
    }
    public void UpgradeScoreSpeed()
    {
        if (Upgrade(gameMaster.scoreSpeedUpgradeValue, maxScoreSpeedUpgradeValue,
            scoreSpeedUpgradePrice, scoreSpeedMaxText, scoreSpeedUpgradeIndicators) == "Y")
        {
            gameMaster.scoreSpeedUpgradeValue++;
            //saving progress
            GameManager.SaveProgress(gameMaster);
        }
    }
    public void UpgradeSlowMotion()
    {
        if (Upgrade(gameMaster.slowMotionUpgradeValue, maxSlowMotionUpgradeValue,
            slowMotionUpgradePrice, slowMotionMaxText, slowMotionUpgradeIndicators) == "Y")
        {
            gameMaster.slowMotionUpgradeValue++;
            //saving progress
            GameManager.SaveProgress(gameMaster);
        }
    }
    public void UpgradeMissile()
    {
        if (Upgrade(gameMaster.missileUpgradeValue, maxMissileUpgradeValue,
            missileUpgradePrice, missileMaxText, missileUpgradeIndicators) == "Y")
        {
            gameMaster.missileUpgradeValue++;
            //saving progress
            GameManager.SaveProgress(gameMaster);
        }
    }
    private void Update()
    {
        if (Time.timeSinceLevelLoad - showNotEnoughFundsStart >= showNotEnoughFundsTime)
        {
            NotEnoughFunds.SetActive(false);
        }
    }
    private void CheckMaxUpgrade(int upgradeValue, int maxUpgradeValue, GameObject maxText)
    {
        //checking if we now have max upgrade
        if (upgradeValue == maxUpgradeValue)
        {
            maxText.SetActive(true);
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
