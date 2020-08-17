using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    //shop and endscreen objects
    public GameObject shop;
    private GameObject gameMasterObject;
    private GameMaster gameMaster;

    //missile
    public int missileUpgradeValue;
    public int maxMissileUpgradeValue = 2;
    //slow-motion
    //public TimeManager timeManager;
    public int slowMotionUpgradeValue;
    private int maxSlowMotionUpgradeValue;

    private void Awake()
    {
        gameMasterObject = GameObject.Find("GameMaster");
        gameMaster = gameMasterObject.GetComponent<GameMaster>();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void UpgradeSlowMotion()
    {
        gameMaster.slowMotionUpgradeValue++;
        //GameManager.SaveProgress(this);
    }
    public void UpgradeMissile()
    {
        if (gameMaster.missileUpgradeValue < maxMissileUpgradeValue)
        {
            gameMaster.missileUpgradeValue++;
            GameManager.SaveProgress(gameMaster);
        }
    }
}
