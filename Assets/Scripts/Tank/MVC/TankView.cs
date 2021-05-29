using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    public Joystick movementJoystick;
    public Joystick rotationJoystick;
    public float vertical;
    public float horizontal;
    private TankController tankController;

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        vertical = movementJoystick.Vertical;
        horizontal = rotationJoystick.Horizontal;
    }
    private void FixedUpdate()
    {
        tankController.Move(vertical, tankController.tankModel.movementSpeed);
        tankController.Rotate(horizontal, tankController.tankModel.rotationSpeed);
    }
}
