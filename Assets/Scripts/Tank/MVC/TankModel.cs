using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public TankType tankType { get; private set; }

    public float movementSpeed { get; private set; }
    public float rotationSpeed { get; private set; }

    public float health { get; set; }

    public Material material { get; private set; }
    public TankModel(TankScriptable tankScriptable, TankSOList tankList)
    {
        tankType = tankScriptable.tankType;

        movementSpeed = tankScriptable.movementSpeed;
        rotationSpeed = tankScriptable.rotationSpeed;
        health = tankScriptable.health;
        material = tankScriptable.material;`
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

}
