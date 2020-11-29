using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] GameObject explosionEffect;
   // public int currentLife = 1;

    private bool _isPlayerDamageable;
    private float _cooldownTime;
    void Start () {

        explosionEffect.SetActive(false);
        _isPlayerDamageable = false;

        _cooldownTime = 1.0f;
        Invoke("MakePlayerDamageable", _cooldownTime);
    }

    void MakePlayerDamageable()
    {
        _isPlayerDamageable = true;
    }

	public Vector3 GetPlayerCurrentPosition()
	{
		return transform.position;
	}
	
	void Update () {
	/*	if(gameObject.transform.localPosition.y < 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }*/
	}

  /*  private void OnTriggerEnter(Collider other)
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
                other.gameObject.SetActive(false);
               // FindObjectOfType<GameManager>().Restart();
                EnemyHitEffect();
            }
           
        }

        if (other.gameObject.tag == "Heart")
        {
            other.gameObject.SetActive(false);
            Debug.Log("Life game");
            FindObjectOfType<HealthController>().AddLife();
        }
    }*/


  /*  void EnemyHitEffect()
    {
        gameObject.GetComponent<PlayerMovementController>().isPlayerMoved = false;

        if (healthControler.IsHealthRemains())
        {
            healthControler.ShowUseLifePanel();
        }
        else
        {
            Die();
        }

       
    }*/

    public void Die()
    {
        Debug.Log("Die in Player C");
        explosionEffect.SetActive(true);
        Invoke("CallEndGame", 1f);
    }

   /* void CallEndGame()
    {
        gameManager.EndGame();
    }*/

  
}
