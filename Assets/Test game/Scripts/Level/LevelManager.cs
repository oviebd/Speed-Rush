using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public int levelNo { get; set; }
    List<LevelInfoClass> levelInfosList;

   
    void Start () {
        levelInfosList = new List<LevelInfoClass>();
        levelNo = 1;
        SetLevels();
	}

    /*   public LevelInfoClass GetLevelDificulty(int levelNo)
       {
           LevelInfoClass level = new LevelInfoClass();
           if (levelInfosList.Count <=0 )
           {
               SetLevels();
           }
           if( levelNo <= levelInfosList.Count)
           {
               level = levelInfosList[levelNo];
           }

           return level;
       }*/

    public LevelInfoClass GetLevelDificulty(int levelNo)
    { 
        int min_enemy = 2, max_enemy = 5 ;
        float playerSpeed,playerMaxSpeed ;

        playerSpeed = levelNo* .4f;
        playerMaxSpeed = levelNo * .8f;

        if (playerSpeed > 1.5f)
        {
            playerSpeed = 1.5f;
        }
        if(playerMaxSpeed> 3.5f)
        {
            playerMaxSpeed =3.5f;
        }

        LevelInfoClass level = new LevelInfoClass();
        level.levelNo = levelNo;
        level.min_Enemy = min_enemy;
        level.max_enemy = max_enemy;
        level.playerInitSpeed = playerSpeed;
        level.playerMaxSpeed = playerMaxSpeed;

        return level;
    }

    void SetLevels()
    {
        levelInfosList = new List<LevelInfoClass>();

        int min_enemy=3, max_enemy=6;
        float playerSpeed = 0f;
        for (int i = 1; i <= 5; i++)
        {
            min_enemy = min_enemy +( i-1);
            max_enemy = max_enemy +( (i-1) * 2);
            playerSpeed = playerSpeed + 0.5f;

            if(max_enemy > 18)
            {
                max_enemy = 20;
            }

            if (min_enemy >8)
            {
                min_enemy = 8;
            }

            LevelInfoClass level = new LevelInfoClass();
            level.levelNo = i;
            level.min_Enemy = min_enemy;
            level.max_enemy = max_enemy;
            level.playerInitSpeed = playerSpeed;


            levelInfosList.Add(level);
        }

    }

	
}
