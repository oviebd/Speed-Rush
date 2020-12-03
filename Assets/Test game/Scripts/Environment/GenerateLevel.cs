using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    //Generate Floor

    private GameObject _floorParent;
    private ObjectPoolHandler _floorObjectPool;

    [SerializeField] private GameObject _player;
    private GameObject _currentFloorInstance;

    private float _floorMaxZScale;
    private float _currentFloorZpos = 0f;
    private int _floorZsize = 27;

    private int floorNum=0;
    private bool isItFirstFloor;

    //Generate Enemy Blocks
    [SerializeField] private float _maxXpos;
    [SerializeField] private float _minXpos;

    private ObjectPoolHandler _enemyObjectPool;

    private int _min_enemy_per_block = 2; //2
    private int _max_enemy_per_block = 5;//5

    private float _distanceBetweenTwoEnemy;
    private float _last_enemyZpos = 0f;

    //Spawn reWard
    int _numberOfblocksAfterRewardSpawn = 2;

    private ObjectPoolHandler _rewardObjectPool;
    private float _distanceBetweenTwoReward=15f;
    private float _last_RewardZpos = 0f;
    private float _min_distance_for_reward =100f;

    void Start()
    {
        _enemyObjectPool = GameObject.FindWithTag("EnemyPool").GetComponent<ObjectPoolHandler>();
        _floorObjectPool = GameObject.FindWithTag("FloorPool").GetComponent<ObjectPoolHandler>();
        _rewardObjectPool =  GameObject.FindWithTag("RewardPool").GetComponent<ObjectPoolHandler>();

        _floorParent = this.gameObject;
        _player.SetActive(false);
        isItFirstFloor = true;
        MakeCalculation();
        GenerateNewFloor();

        ManageEnemySpawn();
      
        Invoke("ActivePlayer", .5f);
    }

    void ActivePlayer()
    {
        _player.SetActive(true);
        Camera.main.gameObject.GetComponent<CameraFollower>().SetCameraTarget();
    }

    void MakeCalculation()
    {
        GameObject _floorPrefab = _floorObjectPool.GetGameObjectFromPool();
        _floorPrefab.SetActive(false);
        _floorMaxZScale = _floorPrefab.transform.localScale.z;
        _currentFloorZpos = _floorMaxZScale * _floorMaxZScale * _floorMaxZScale;
        _floorPrefab.GetComponent<FloorController>().floorPool = _floorObjectPool;
        _floorPrefab.GetComponent<FloorController>().ResetFloor();

        //Reward
        _min_distance_for_reward = (_floorMaxZScale * _floorMaxZScale * _floorMaxZScale) * _numberOfblocksAfterRewardSpawn;
    }


   public void SetData(LevelInfoClass level)
    {
        _min_enemy_per_block = level.min_Enemy;
        _max_enemy_per_block = level.max_enemy;
    }


    public void GenerateNewFloor()
    {
        floorNum++;
        GameObject _floorPrefab = _floorObjectPool.GetGameObjectFromPool();

        _floorPrefab.GetComponent<FloorController>().floorPool = _floorObjectPool;
        _floorPrefab.GetComponent<FloorController>().ResetFloor();

        Vector3 instantiatePos = new Vector3(_floorPrefab.transform.localPosition.x, _floorPrefab.transform.localPosition.y, _currentFloorZpos);

        _floorPrefab.transform.localPosition = instantiatePos;
        _floorPrefab.transform.SetParent(_floorParent.transform, true);
        _floorPrefab.transform.localRotation = _floorPrefab.transform.rotation;

        _currentFloorZpos = _currentFloorZpos + (_floorMaxZScale * _floorMaxZScale * _floorMaxZScale);

        if (isItFirstFloor == false)
        {
            ManageEnemySpawn();

            if ((floorNum% _numberOfblocksAfterRewardSpawn) ==0)
            {
                GenerateReward();
            }
           
        }
        else
        {
            isItFirstFloor = false;
        }
    }

    void GenerateReward()
    {
        _last_RewardZpos = _last_RewardZpos + _min_distance_for_reward;
    
        GameObject _rewardPrefab = _rewardObjectPool.GetGameObjectFromPool();
        float xpos = Random.Range(_minXpos, _maxXpos);
        Vector3 instantiatePos = new Vector3(xpos, _rewardPrefab.transform.localPosition.y, _last_RewardZpos);
       
        _rewardPrefab.GetComponent<RewardController>().rewardPool = _rewardObjectPool;
        _rewardPrefab.GetComponent<RewardController>().ResetReward();

        _rewardPrefab.transform.localPosition = instantiatePos;
        _rewardPrefab.transform.SetParent(_floorParent.transform, true);
        _rewardPrefab.transform.localRotation = _floorParent.transform.rotation;
    }

    private void ManageEnemySpawn()
    {
        int maxEnemyOnAPlane = Random.Range(_min_enemy_per_block, _max_enemy_per_block);

        int factor = (_floorZsize / maxEnemyOnAPlane);
        if (factor <= 5)
            factor = 5;

        _distanceBetweenTwoEnemy = factor;

      //  Debug.Log("Distance btwene two enemy :" + _distanceBetweenTwoEnemy);

        for (int i = 0; i < maxEnemyOnAPlane; i++)
        {
            GenerateEnemy();
        }
    }

    private void GenerateEnemy()
    {

        float xpos = Random.Range(_minXpos, _maxXpos);
        _last_enemyZpos = _last_enemyZpos + _distanceBetweenTwoEnemy;

        if (_last_enemyZpos > _currentFloorZpos && floorNum >1)
        {
            _last_enemyZpos = _currentFloorZpos;
            return;
        }

        GameObject _enemyPrefab = _enemyObjectPool.GetGameObjectFromPool();

        Vector3 instantiatePos = new Vector3(xpos, _enemyPrefab.transform.localPosition.y, _last_enemyZpos);

      //  _enemyPrefab.GetComponent<EnemyHandler>().enemyPool = _enemyObjectPool;
       // _enemyPrefab.GetComponent<EnemyHandler>().ResetEnemy();

        _enemyPrefab.transform.localPosition = instantiatePos;
        _enemyPrefab.transform.SetParent(_floorParent.transform, true);
        _enemyPrefab.transform.localRotation = _floorParent.transform.rotation;

    }
}
