using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public int remainingLife { get; set; }
    UiManager uiManager;
    GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
        uiManager = FindObjectOfType<UiManager>();

        remainingLife = 2;
        ShowLifeUi();
    }

    public void AddLife()
    {
        remainingLife++;
        uiManager.UpdateLifeText(remainingLife);
    }

    void ShowLifeUi()
    {
        uiManager.UpdateLifeText(remainingLife);
    }

    public bool IsHealthRemains()
    {
        if (remainingLife > 0)
            return true;
        else
            return false;
    }

    public void ShowUseLifePanel()
    {
        uiManager.ShowUseLifePanel();
        gameManager._isGameRunning = false;
        gameManager.PauseGame();
    }

    public void UseLife()
    {
        remainingLife--;
        FindObjectOfType<PlayerMovementController>().isPlayerMoved = true;
        gameManager._isGameRunning = true;
        uiManager.ShowInGamePanel();
        gameManager.ResumeGame();
      
    }

    public void DoNotUseLife()
    {
        gameManager.ResumeGame();
        Debug.Log("Do not use Life in Health C");
        FindObjectOfType<PlayerController>().Die();
    }
}
