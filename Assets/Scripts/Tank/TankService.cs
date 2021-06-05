
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public TankScriptable[] tankList;
    public TankScriptable tankScriptable { get; private set; }

    public Joystick joystick;

    private void Start()
    {
        GetTankType();
        CreateTank();
    }

    private void GetTankType()
    {
        int rand = Random.Range(0, tankList.Length);
        tankScriptable = tankList[rand];
    }

    public void CreateTank()
    {
        GameObject tank = Instantiate(tankScriptable.tankPref, Vector3.zero, Quaternion.identity);
        tank.GetComponent<TankController>().SetValues(tankScriptable);
        CameraController.Instance.SetTarget(tank.transform);
    }
}
