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
    //coin system
    public float coinCount = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
