using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLifePanel : AnimatorPanel
{
    public void OnWatchAdsButtonClicked()
    {
        Hide();
        AdManager.instance.ShowRewardAd();
       // GameManager.instance.RespawnPlayer();
    }

    public void OnCancelButtonClicked()
    {
        Hide();
        GameManager.instance.EndGame();
    }
}
