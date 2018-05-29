using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public Text lifeText;
    public Text scoreText;
    void Start() {

    }


    public void UpdateLifeText(int life)
    {
        lifeText.text = life + "";
    }
    
    public void  UpdateScoreText(int score)
    {
        scoreText.text = score + "";
    }
}
