using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MissonScriptable : ScriptableObject
{
    public int id;
    public string name;
    public string tag;
    public int minVal;
    public int maxVal;
    public int targetedValue;
    public bool isItcollisionBased;
}
