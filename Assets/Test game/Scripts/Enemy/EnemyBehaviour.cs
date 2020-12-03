using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	GameManager _gameManeger;

    void Start()
    {
		_gameManeger = GameManager.instance;
	}

  
    void Update()
    {
        if(_gameManeger.GetPlayerController().GetPlayerCurrentPosition().z > (this.transform.position.z + 10.0f))
		{
			Destroy(this.gameObject);
		}
    }
}
