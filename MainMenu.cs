using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneLoader sceneLoader;

    //gameMaster stuff
    private GameMaster gameMaster;
    private GameObject[] gameMasterArray;

    //AudioManager stuff
    private GameObject[] audioManagerArray;

    //for intro
    public Intro introScript;

    private void Awake()
    {
        //making sure that there aren't more than 1 gameMaster at all times...
        gameMasterArray = GameObject.FindGameObjectsWithTag("GameMaster");

        if (gameMasterArray.Length > 1)
        {
            Destroy(gameMasterArray[1].gameObject);
        }
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        GameManager.LoadProgress(gameMaster);

        //making sure that there aren't more than 1 audioManager at all times...
        audioManagerArray = GameObject.FindGameObjectsWithTag("AudioManager");

        if (audioManagerArray.Length > 1)
        {
            Destroy(audioManagerArray[1].gameObject);
        }

        gameMaster.gameCount = 0;//resetting game count for ads, so that we don't get an ad on game open
    }
    public void PlayButton()
    {
        sceneLoader.LoadSceneByName("Gameplay", "Start");
        gameMaster.isIntroScene = false;
        GameManager.gameHasEnded = false;
    }
    public void ShopButton()
    {
        sceneLoader.LoadSceneByName("Shop", "Start");
    }
    public void SettingsButton()
    {
        sceneLoader.LoadSceneByName("Settings", "Start");
    }
    public void IntroButton()
    {
        sceneLoader.LoadSceneByName("Gameplay", "Start");
        gameMaster.isIntroScene = true;
    }
}
