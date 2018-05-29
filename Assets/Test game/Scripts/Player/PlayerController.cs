using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int currentLife = 1;

    private bool _isPlayerDamageable;
    private float _cooldownTime;

    void Start () {
        _isPlayerDamageable = false;

        _cooldownTime = 1.0f;
        Invoke("MakePlayerDamageable", _cooldownTime);
    }
	

    void MakePlayerDamageable()
    {
        _isPlayerDamageable = true;
    }
	
	void Update () {
		if(gameObject.transform.localPosition.y < 0)
        {
            FindObjectOfType<GameManager>().Restart();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Collided "+ collision.gameObject.name);
        if (other.gameObject.tag == "instantiate_collider")
        {
           // Debug.Log("He He");
            FindObjectOfType<GenerateLevel>().GenerateNewFloor();
        }

        if (other.gameObject.tag == "Enemy")
        {
            // Debug.Log("Destroy game");
            if (_isPlayerDamageable)
            {
                FindObjectOfType<GameManager>().Restart();
            }
           
        }

        if (other.gameObject.tag == "Life")
        {
            Debug.Log("Life game");
            currentLife++;
            FindObjectOfType<UiManager>().UpdateLifeText(currentLife);
        }
    }

  
}
