using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //public static int highScore;
    //public static int missileUpgradeValue;
    //public static int slowMotionUpgradeValue;

    public int playerHighScore = 0;
    public int missileUpgradeValue = 0;
    public int slowMotionUpgradeValue = 0;

    /*private void Update()
    {
        highScoreMaster = highScore;
        missileUpgradeValueMaster = missileUpgradeValue;
        slowMotionUpgradeValueMaster = slowMotionUpgradeValue;
    }*/
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
