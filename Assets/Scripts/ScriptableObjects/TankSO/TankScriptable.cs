using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObject/Tank/NewTankScriptableObject")]
public class TankScriptable : ScriptableObject
{
    public TankType tankType;

    public TankView tankView;

    public float health;

    public float movementSpeed;

    public float rotationSpeed;

    public float fireRate;

    public Material material;
}
