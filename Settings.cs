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
    public Button toggleMusicButton;
    public Button toggleSoundsButton;

    //colors
    private Color32 white = new Color32(255, 255, 255, 255);
    private Color32 gray = new Color32(255, 255, 255, 150);

    void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        isMusicEnabled = gameMaster.isMusicEnabled;
        areSoundsEnabled = gameMaster.areSoundsEnabled;

        //setting the colors as they are supposed to be
        if (isMusicEnabled)
        {
            toggleMusicButton.GetComponent<Image>().color = white;
        }
        else
        {
            toggleMusicButton.GetComponent<Image>().color = gray;
        }

        if (areSoundsEnabled)
        {
            toggleSoundsButton.GetComponent<Image>().color = white;
        }
        else
        {
            toggleSoundsButton.GetComponent<Image>().color = gray;
        }
    }

    public void ToggleMusic()
    {
        if (isMusicEnabled)
        {
            //fading out the sound for smoother effect
            audioManager.FadeOutCaller("MainTheme", 0.03f, audioManager.sounds);

            isMusicEnabled = false;
            //audioManager.StopAudio("MainTheme");

            toggleMusicButton.GetComponent<Image>().color = gray;//changing the color
        }
        else if (!isMusicEnabled)
        {
            //fading in the sound for smoother effect
            audioManager.FadeInCaller("MainTheme", 0.03f, 0.5f, audioManager.sounds);

            isMusicEnabled = true;
            //audioManager.PlayAudio("MainTheme");
            toggleMusicButton.GetComponent<Image>().color = white;//changing the color
        }
        gameMaster.isMusicEnabled = isMusicEnabled;
    }
    public void ToggleSounds()
    {
        if (areSoundsEnabled)
        {
            areSoundsEnabled = false;
            toggleSoundsButton.GetComponent<Image>().color = gray;//changing the color
        }
        else if (!areSoundsEnabled)
        {
            areSoundsEnabled = true;
            toggleSoundsButton.GetComponent<Image>().color = white;//changing the color
        }
        gameMaster.areSoundsEnabled = areSoundsEnabled;
    }
    public void BackButton()
    {
        sceneLoader.LoadSceneByName("MainMenu", "Start");
        GameManager.SaveProgress(gameMaster);
    }
}
