using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    //shop and endscreen objects
    public GameObject shop;
    private GameObject audioManager;
    private GameObject gameMasterObject;
    private GameMaster gameMaster;

    //in order to see how many coins we have
    private int coinCount;

    //missile
    public int missileUpgradeValue;
    public int maxMissileUpgradeValue = 4;
    private int[] missileUpgradePrice = { 0, 10, 250, 400, 800 };
    //slow-motion
    public int slowMotionUpgradeValue;
    private int maxSlowMotionUpgradeValue;
    private int[] slowMotionUpgradePrice = { 0,  10, 60, 180, 360 };

    private void Awake()
    {
        gameMasterObject = GameObject.Find("GameMaster");
        gameMaster = gameMasterObject.GetComponent<GameMaster>();

        audioManager = GameObject.Find("AudioManager");

        GameManager.gameHasEnded = false;//otherwise the game cannot end once a player goes to shop
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(audioManager);
    }
    public void UpgradeSlowMotion()
    {
        //checking that the player has the appropriate number of coins
        if (coinCount >= slowMotionUpgradePrice[gameMaster.slowMotionUpgradeValue]) 
        {
            if (gameMaster.slowMotionUpgradeValue < maxSlowMotionUpgradeValue)
            {
                //taking away the coins for the purchase
                coinCount -= slowMotionUpgradePrice[gameMaster.slowMotionUpgradeValue];
                gameMaster.coinCount = coinCount;

                gameMaster.slowMotionUpgradeValue++;
                GameManager.SaveProgress(gameMaster);
            }
        }
        else
        {
            Debug.Log("Not enough funds");
        }
    }
    public void UpgradeMissile()
    {
        //checking that the player has the appropriate number of coins
        if (coinCount >= missileUpgradePrice[gameMaster.missileUpgradeValue])
        {
            if (gameMaster.missileUpgradeValue < maxMissileUpgradeValue)
            {
                //taking away the coins for the purchase
                //adding 1 because that at the moment the upgrade value is 1 less than the 
                //one the player bought
                gameMaster.coinCount -= missileUpgradePrice[gameMaster.missileUpgradeValue + 1];

                gameMaster.missileUpgradeValue++;
                GameManager.SaveProgress(gameMaster);
                Debug.Log(gameMaster.coinCount);
            }
        }
        else
        {
            Debug.Log("Not enough funds");
        }
    }
}
