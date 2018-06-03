using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public Text lifeText;
    public Text scoreText;
    [SerializeField] Text distanceText;

    [SerializeField] GameObject _endGameUiPanel;
    [SerializeField] GameObject _InGameUiPanel;
    [SerializeField] GameObject _useLifeUiPanel;

    void Start() {
        ShowInGamePanel();
    }


    public void UpdateLifeText(int life)
    {
        lifeText.text = life + "";
    }

    public void UpdateDistanceText(int distance)
    {
        distanceText.text = distance + "m";
    }

    public void  UpdateScoreText(int score)
    {
        scoreText.text = score + "";
    }

    public void ShowInGamePanel()
    {
        ShowHidePanel(_InGameUiPanel);
    }

    public void ShowUseLifePanel()
    {
        ShowHidePanel(_useLifeUiPanel);
    }

    public void ShowEndGamePanel()
    {
        ShowHidePanel(_endGameUiPanel);
    }

    void ShowHidePanel(GameObject showedObj)
    {
        _InGameUiPanel.SetActive(false);
        _endGameUiPanel.SetActive(false);
        _useLifeUiPanel.SetActive(false);

        showedObj.SetActive(true);
        _InGameUiPanel.SetActive(true);
    }
}
