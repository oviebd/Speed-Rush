
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public int score = 0;

    void Start () {
		
	}
	
	void Update () {
        score++;
        scoreText.text = score + "";
	}
}
