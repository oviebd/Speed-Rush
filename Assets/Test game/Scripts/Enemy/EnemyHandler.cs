using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    public ObjectPoolHandler enemyPool { private get; set; }
    [SerializeField] private float _lifeTime = 10f;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ResetEnemy()
    {
        //Debug.Log("reset enemy");
        Invoke("BacktoPool", _lifeTime);
    }

    void BacktoPool()
    {
        //  Debug.Log("bak to pool");
        enemyPool.ReturnGameObjectToPool(gameObject);
    }
}
