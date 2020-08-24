using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameMaster gameMaster;
    private void Awake()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Gameplay");
        GameManager.LoadProgress(gameMaster);
    }
    public void ShopButton()
    {
        SceneManager.LoadScene("Shop");
        GameManager.LoadProgress(gameMaster);
    }
}
