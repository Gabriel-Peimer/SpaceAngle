using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //upgrades to be stored
    public int missileUpgradeValue;
    public int slowMotionUpgradeValue;
    public int scoreSpeedUpgradeValue;
    //coin system
    public float coinCount;
    //high-score
    public int playerHighScore;
    //settings
    public bool isMusicEnabled;
    public bool areSoundsEnabled;
    //ads
    public int gameCount;

    public PlayerData(GameMaster gameMaster)
    {
        missileUpgradeValue = gameMaster.missileUpgradeValue;
        slowMotionUpgradeValue = gameMaster.slowMotionUpgradeValue;
        scoreSpeedUpgradeValue = gameMaster.scoreSpeedUpgradeValue;

        coinCount = gameMaster.coinCount;

        playerHighScore = gameMaster.playerHighScore;

        isMusicEnabled = gameMaster.isMusicEnabled;
        areSoundsEnabled = gameMaster.areSoundsEnabled;

        gameCount = gameMaster.gameCount;
    }
}
