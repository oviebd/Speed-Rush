using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissonController : MonoBehaviour {

    public List<MissonScriptable> missonsList;

	void Start () {
		
	}

    MissonScriptable GetCurrentMisson()
    {
        int currentIndex = Random.Range(0, missonsList.Count);
        MissonScriptable  currentMisson = missonsList[currentIndex];

        return currentMisson;
    }

   public MissonScriptable SetMissonData(int levelNo)
    {
       MissonScriptable currentMisson = GetCurrentMisson();

        int target = Random.Range(currentMisson.minVal, currentMisson.maxVal);
      //  Debug.Log("Misson Controller Misson Name :" + currentMisson.name + "  target : " + target);

        currentMisson.targetedValue = target;

        return currentMisson;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
