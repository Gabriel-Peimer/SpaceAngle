using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    //constant scripts
    private GameMaster gameMaster;
    private AudioManager audioManager;
    public SceneLoader sceneLoader;

    //values in settings
    public bool isMusicEnabled = true;//to be accessed by AudioManager script
    public bool areSoundsEnabled = true;//to be accessed by AudioManager script

    //buttons
    public Text musicButtonText;
    public Text soundButtonText;

    //colors
    private Color32 toggledOn = new Color32(255, 255, 255, 255);
    private Color32 toggledOff = new Color32(170, 170, 170, 200);

    void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        isMusicEnabled = gameMaster.isMusicEnabled;
        areSoundsEnabled = gameMaster.areSoundsEnabled;

        //setting the colors as they are supposed to be
        if (isMusicEnabled)
        {
            musicButtonText.color = toggledOn;
        }
        else
        {
            musicButtonText.color = toggledOff;
        }

        if (areSoundsEnabled)
        {
            soundButtonText.color = toggledOn;
        }
        else
        {
            soundButtonText.color = toggledOff;
        }
    }

    public void ToggleMusic()
    {
        if (isMusicEnabled)
        {
            //fading out the sound for smoother effect
            audioManager.FadeOutCaller("MainTheme", 0.03f, audioManager.sounds);

            isMusicEnabled = false;

            musicButtonText.color = toggledOff;//changing the color
        }
        else if (!isMusicEnabled)
        {
            //fading in the sound for smoother effect
            audioManager.FadeInCaller("MainTheme", 0.03f, 0.5f, audioManager.sounds);

            isMusicEnabled = true;
            musicButtonText.color = toggledOn;//changing the color
        }
        gameMaster.isMusicEnabled = isMusicEnabled;
    }
    public void ToggleSounds()
    {
        if (areSoundsEnabled)
        {
            areSoundsEnabled = false;
            soundButtonText.color = toggledOff;//changing the color
        }
        else if (!areSoundsEnabled)
        {
            areSoundsEnabled = true;
            soundButtonText.color = toggledOn;//changing the color
        }
        gameMaster.areSoundsEnabled = areSoundsEnabled;
    }
    public void BackButton()
    {
        sceneLoader.LoadSceneByName("MainMenu", "Start");
        GameManager.SaveProgress(gameMaster);
    }
}
