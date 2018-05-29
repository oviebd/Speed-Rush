using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

    public ObjectPoolHandler floorPool { private get; set; }        
  
    [SerializeField] private float _lifeTime = 10f;

    void Start()
    {

    }


    void Update()
    {

    }

    public void ResetFloor()
    {
       // Debug.Log("reset Floor");
       
        Invoke("BacktoPool", _lifeTime);
    }

    void BacktoPool()
    {
     //   Debug.Log("bak to pool");
        floorPool.ReturnGameObjectToPool(gameObject);
    }
}
