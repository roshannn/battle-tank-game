using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TankSOList", menuName = "ScriptableObject/Tank/TankScriptableObjectList")]
public class TankSOList : ScriptableObject
{
    public TankScriptable[] tanks;
}

