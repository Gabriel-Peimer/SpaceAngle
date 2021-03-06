﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shop;//the actual shop cause duh and stuff you know...
    private GameMaster gameMaster;// to save progress
    public SceneLoader sceneLoader;// for the transitions
    public GameObject[] shopButtons;

    //in order to see how many coins we have
    public Text coinCountText;
    //no funds...
    public GameObject NotEnoughFunds;
    private float showNotEnoughFundsStart;
    private float showNotEnoughFundsTime = 1f;

    //Upgrades
    //missile
    private int maxMissileUpgradeValue = 4;
    private int[] missileUpgradePrice = { 0, 125, 250, 400, 800 };
    public GameObject[] missileUpgradeIndicators;
    public GameObject missileMaxText;
    public GameObject priceTextMissile;
    public GameObject priceTextParentMissile;
    //ship-speed
    private int maxShipSpeedUpgradeValue = 4;
    private int[] shipSpeedUpgradePrice = { 0, 10, 60, 180, 360 };
    public GameObject[] shipSpeedUpgradeIndicators;
    public GameObject shipSpeedMaxText;
    public GameObject priceTextShipSpeed;
    public GameObject priceTextParentShipSpeed;
    //score-speed
    private int maxScoreSpeedUpgradeValue = 4;
    private int[] scoreSpeedUpgradePrice = { 0, 30, 75, 220, 500 };
    public GameObject[] scoreSpeedUpgradeIndicators;
    public GameObject scoreSpeedMaxText;
    public GameObject priceTextScoreSpeed;
    public GameObject priceTextParentScoreSpeed;

    //colors for indicators
    Color32 whiteColor = new Color32(255, 255, 255, 255);

    private void Awake()
    {
        //setting max upgrades
        maxMissileUpgradeValue = missileUpgradePrice.Length - 1;
        maxShipSpeedUpgradeValue = shipSpeedUpgradePrice.Length - 1;

        //gameMaster
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        GameManager.gameHasEnded = false;//otherwise the game cannot end once a player goes to shop

        //showing the coin count
        coinCountText.text = gameMaster.coinCount.ToString();

        UpdateAtBeginning();
    }
    private void UpdateAtBeginning()
    {
        //checking if to show max text for upgrades
        CheckMaxUpgrade(gameMaster.scoreSpeedUpgradeValue, maxScoreSpeedUpgradeValue,
            scoreSpeedMaxText, priceTextParentScoreSpeed);
        CheckMaxUpgrade(gameMaster.shipSpeedUpgradeValue, maxShipSpeedUpgradeValue,
            shipSpeedMaxText, priceTextParentShipSpeed);
        CheckMaxUpgrade(gameMaster.missileUpgradeValue, maxMissileUpgradeValue,
            missileMaxText, priceTextParentMissile);

        //lighting up indicators
        LightUpIndicators(missileUpgradeIndicators, gameMaster.missileUpgradeValue);
        LightUpIndicators(shipSpeedUpgradeIndicators, gameMaster.shipSpeedUpgradeValue);
        LightUpIndicators(scoreSpeedUpgradeIndicators, gameMaster.scoreSpeedUpgradeValue);

        //showing the price
        PriceTextUpdate(priceTextMissile, priceTextParentMissile, gameMaster.missileUpgradeValue,
            maxMissileUpgradeValue, missileUpgradePrice);
        PriceTextUpdate(priceTextScoreSpeed, priceTextParentScoreSpeed, gameMaster.scoreSpeedUpgradeValue,
            maxScoreSpeedUpgradeValue, scoreSpeedUpgradePrice);
        PriceTextUpdate(priceTextShipSpeed, priceTextParentShipSpeed, gameMaster.shipSpeedUpgradeValue,
            maxShipSpeedUpgradeValue, shipSpeedUpgradePrice);
    }
    public void ReturnToMainMenu()
    {
        sceneLoader.LoadSceneByName("MainMenu", "CloseShop");
    }
    private string Upgrade(int upgradeValue, int maxUpgradeValue,
        int[] upgradePriceArray, GameObject maxText,
        GameObject[] upgradeIndicators, GameObject priceText, GameObject priceParent)
    {
        if (upgradeValue < maxUpgradeValue)
        {
            //checking that the player has the appropriate number of coins
            if (gameMaster.coinCount >= upgradePriceArray[upgradeValue + 1])
            {

                //taking away the coins for the purchase
                //adding 1 because that at the moment the upgrade value is 1 less than the 
                //one the player bought
                gameMaster.coinCount -= upgradePriceArray[upgradeValue + 1];

                //updating coin count on screen
                coinCountText.text = gameMaster.coinCount.ToString();

                //+1 in the next line because that we didn't upgrade yet
                LightUpIndicators(upgradeIndicators, upgradeValue + 1);

                //checking if we have maxed the upgrade and updating if yes
                CheckMaxUpgrade(upgradeValue + 1, maxUpgradeValue, maxText, priceParent);
                
                //showing price
                PriceTextUpdate(priceText, priceParent, upgradeValue,
                    maxUpgradeValue, upgradePriceArray);

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
            scoreSpeedUpgradePrice,scoreSpeedMaxText, scoreSpeedUpgradeIndicators,
            priceTextScoreSpeed, priceTextParentScoreSpeed) == "Y")
        {
            gameMaster.scoreSpeedUpgradeValue++;
            //saving progress
            GameManager.SaveProgress(gameMaster);

            //showing the price
            if (gameMaster.scoreSpeedUpgradeValue != maxScoreSpeedUpgradeValue)
            {
                priceTextScoreSpeed.GetComponent<Text>().text =
                    scoreSpeedUpgradePrice[gameMaster.scoreSpeedUpgradeValue + 1].ToString();
            }
        }
    }
    public void UpgradeShipSpeed()
    {
        if (Upgrade(gameMaster.shipSpeedUpgradeValue, maxShipSpeedUpgradeValue,
            shipSpeedUpgradePrice, shipSpeedMaxText, shipSpeedUpgradeIndicators,
            priceTextShipSpeed, priceTextParentShipSpeed) == "Y")
        {
            gameMaster.shipSpeedUpgradeValue++;
            //saving progress
            GameManager.SaveProgress(gameMaster);

            //showing the price
            if (gameMaster.shipSpeedUpgradeValue != maxShipSpeedUpgradeValue)
            {
                priceTextShipSpeed.GetComponent<Text>().text =
                    shipSpeedUpgradePrice[gameMaster.shipSpeedUpgradeValue + 1].ToString();
            }
        }
    }
    public void UpgradeMissile()
    {
        if (Upgrade(gameMaster.missileUpgradeValue, maxMissileUpgradeValue,missileUpgradePrice,
            missileMaxText, missileUpgradeIndicators, priceTextMissile, priceTextParentMissile) == "Y")
        {
            gameMaster.missileUpgradeValue++;
            //saving progress
            GameManager.SaveProgress(gameMaster);

            //showing the price
            if (gameMaster.missileUpgradeValue != maxMissileUpgradeValue)
            {
                priceTextMissile.GetComponent<Text>().text =
                    missileUpgradePrice[gameMaster.missileUpgradeValue + 1].ToString();
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
    private void CheckMaxUpgrade(int upgradeValue, int maxUpgradeValue,
        GameObject maxText, GameObject priceParent)
    {
        //checking if we now have max upgrade
        if (upgradeValue >= maxUpgradeValue)
        {
            maxText.SetActive(true);
            priceParent.SetActive(false);
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
    private void PriceTextUpdate(GameObject priceText, GameObject priceTextParent,
        int upgradeValue, int maxUpgradeValue, int[] priceArray)
    {
        if (upgradeValue < maxUpgradeValue)
        {
            priceText.GetComponent<Text>().text = priceArray[upgradeValue + 1].ToString();

        }
    }
    public void ShowShopButtons()
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            shopButtons[i].SetActive(true);
        }
    }
}
