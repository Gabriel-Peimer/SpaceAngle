using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Shop upgrades to be stored
    public int missileUpgradeValue;
    public int slowMotionUpgradeValue;

    //public int playerHighScore;

    public PlayerData(Shop shop)
    {
        missileUpgradeValue = shop.missileUpgradeValue;
        slowMotionUpgradeValue = shop.slowMotionUpgradeValue;
    }
}
