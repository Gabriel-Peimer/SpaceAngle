using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //high-score
    public int playerHighScore = 0;

    //shop upgrades
    public int missileUpgradeValue = 0;
    public int slowMotionUpgradeValue = 0;
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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
