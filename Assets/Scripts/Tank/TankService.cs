using UnityEngine;

public class TankService : MonoBehaviour
{
    public TankScriptable[] tankList;
    public EnemyService enemyService;
    public Transform parent;
    private TankController tankController;
    public TankScriptable tankScriptable { get; private set; }
    public CameraController camControl;
    public Joystick joystick;
    private Transform tankSpawn;

    private void Start()
    {
        parent = GetComponent<Transform>();
    }
    public void StartTank()
    {
        GetTankType();
        GetPlayerTransform();
        CreateTank();
    }

    
    private void GetPlayerTransform()
    {
        tankSpawn = enemyService.enemyTransformScriptable.GetPlayerTransform();
        tankSpawn.position = new Vector3(tankSpawn.position.x, 0, tankSpawn.position.z);
    }


    private void GetTankType()
    {
        int rand = Random.Range(0, tankList.Length);
        tankScriptable = tankList[rand];
    }

    public void CreateTank()
    {
        GameObject tank = Instantiate(tankScriptable.tankPref,parent,true);
        tank.transform.position = tankSpawn.position;
        tank.transform.rotation = Quaternion.identity;
        tankController = tank.GetComponent<TankController>();
        tankController.SetValues(tankScriptable);
        camControl.SetTarget(tankController.tankTransform);
    }

    public void DestroyTank()
    {
        tankController.Destroy();
    }

}
