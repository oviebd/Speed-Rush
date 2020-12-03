using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance =null;
	private PlayerController _pllayerController;
 
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	public PlayerController GetPlayerController()
	{
		if(_pllayerController == null)
			_pllayerController = FindObjectOfType<PlayerController>();
		return _pllayerController;
	}

  

   


   
  

	
}
