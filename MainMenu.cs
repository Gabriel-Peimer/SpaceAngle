using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameMaster gameMaster;
    public SceneLoader sceneLoader;
    private void Awake()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
    public void PlayButton()
    {
        sceneLoader.LoadSceneByName("Gameplay", "Start");
        GameManager.LoadProgress(gameMaster);
    }
    public void ShopButton()
    {
        sceneLoader.LoadSceneByName("Shop", "Start");
        GameManager.LoadProgress(gameMaster);
    }
}
