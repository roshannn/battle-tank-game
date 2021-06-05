using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObject/Tank/NewTankScriptableObject")]
public class TankScriptable : ScriptableObject
{
    public TankType tankType;

    public GameObject tankPref;

    public float health;

    public float movementSpeed;

    public float turnSpeed;

    public float fireRate;

    public Material material;
}
