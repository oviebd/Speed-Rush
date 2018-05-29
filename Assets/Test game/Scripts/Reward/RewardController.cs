using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour {


    public ObjectPoolHandler rewardPool { private get; set; }
    [SerializeField] private float _lifeTime = 10f;


    public void ResetReward()
    {
        Invoke("BacktoPool", _lifeTime);
    }

    void BacktoPool()
    {
        rewardPool.ReturnGameObjectToPool(gameObject);
    }
}
