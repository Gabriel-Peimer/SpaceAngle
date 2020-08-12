using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void PlayButton()
    {
        SceneManager.LoadScene("Gameplay");
    }
    private void ShopButton()
    {
        SceneManager.LoadScene("Shop");
    }
}
