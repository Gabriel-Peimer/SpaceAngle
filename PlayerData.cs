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

    public PlayerData(GameMaster gameMaster)
    {
        missileUpgradeValue = gameMaster.missileUpgradeValue;
        slowMotionUpgradeValue = gameMaster.slowMotionUpgradeValue;

        coinCount = gameMaster.coinCount;

        playerHighScore = gameMaster.playerHighScore;
    }
}
