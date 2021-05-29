using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController
{
    public TankModel tankModel { get; private set; }
    public  TankView tankView { get; private set; }
    private Rigidbody rigidBody;

    public TankController(TankModel _tankModel,TankView _tankView)
    {
        tankModel = _tankModel;
        tankView = GameObject.Instantiate<TankView>(_tankView);
        CameraController.Instance.SetTarget(tankView.transform);
        rigidBody = tankView.GetComponent<Rigidbody>();
        tankView.SetTankController(this);
        tankModel.SetTankController(this);
        tankView.ChangeColor(tankModel.material);

    }
    public void Move(float movement, float movementSpeed)
    {
        Vector3 move = tankView.transform.transform.position += tankView.transform.forward * movement * movementSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition(move);
    }

    public void Rotate(float rotation, float rotateSpeed)
    {
        Vector3 vector = new Vector3(0f, rotation * rotateSpeed, 0f);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.fixedDeltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }
}
