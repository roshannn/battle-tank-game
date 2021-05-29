using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : SingletonGeneric<TankService>
{
    public TankSOList tankList;
    private TankModel currentTankModel;
    private TankController tankController;
    public int tankSpawnDelay = 3;

    public TankScriptable tankScriptable { get; private set; }
    private List<TankController> tanks = new List<TankController>();

    private void Start()
    {
        CreateTank();
    }
    public void CreateTank()
    {
        int rand = Random.Range(0, tankList.tanks.Length);
        tankScriptable = tankList.tanks[rand];

        TankModel tankModel = new TankModel(tankScriptable, tankList);
        currentTankModel = tankModel;
        tankController = new TankController(tankModel, tankScriptable.tankView);
        tanks.Add(tankController);
    }

    public TankModel GetCurrentTankModel()
    {
        return currentTankModel;
    }
    public TankController GetTankController(int index = 0) //by default only 1st tankController will be returned 
    {
        return tanks[index];
    }

    
}
