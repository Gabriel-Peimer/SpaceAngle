using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    string googlePlay_ID = "3816795";
    bool testMode = true;
    string myPlacementId = "rewardedVideo";

    //for saving data
    private GameMaster gameMaster;
    //for reward
    public ScoreDisplayAtCollision scoreDisplayAtCollision;
    private int rewardForVideoAd;
    
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(googlePlay_ID, testMode);

        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
    public void DisplayInterstitialAd()
    {
        Advertisement.Show();
    }
    public void DisplayVideoAd()
    {
        Advertisement.Show(myPlacementId);

        rewardForVideoAd = scoreDisplayAtCollision.videoAdReward;
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)//ad has finished
        {
            gameMaster.coinCount += rewardForVideoAd;
            GameManager.SaveProgress(gameMaster);
        }
        else if (showResult == ShowResult.Skipped)//ad was skipped
        {
            Debug.Log("Ad skipped");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
