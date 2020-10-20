using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //high-score
    public int playerHighScore = 0;

    //shop upgrades
    public int missileUpgradeValue = 0;
    public int shipSpeedUpgradeValue = 0;
    public int scoreSpeedUpgradeValue = 0;
    
    //coin system
    public float coinCount = 0;

    //settings
    public bool isMusicEnabled = true;
    public bool areSoundsEnabled = true;

    //for ads
    public int gameCount = 0;

    //for intro (not to be saved)
    public bool isIntroScene;

    //for touch controls
    public bool isJoystickActive = true;

    //for rating
    public bool isAskForRatingOff = false;

    //games between request rating (doesn't need to be saved)
    public int gamesBetweenRatingRequest = 8;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
