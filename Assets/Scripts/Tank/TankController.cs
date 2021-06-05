//non mvc

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class TankController: MonoBehaviour
{
    private TankType tankType;

    private float vertical;
    private float horizontal;

    private float movementSpeed;
    private float turnSpeed;
    private Joystick joystick;
    private Rigidbody rigidBody;

    private float health;
    private float fireRate;
    public Transform fireTransform;
    public Transform tankTransform;     
    public MeshRenderer[] tankParts;

    private void Start()
    {
       
        rigidBody = GetComponent<Rigidbody>();
        joystick = TankService.Instance.joystick;
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            BulletService.Instance.Fire(fireTransform);
        }
    }

    private void FixedUpdate()
    {
        Move(vertical);
        Turn(horizontal); 
    }

    private void Turn(float horizontal)
    {
        float turn = horizontal * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }

    private void Move(float vertical)
    {
        Vector3 movement = transform.forward * vertical * movementSpeed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);
    }

    public void SetValues(TankScriptable tankScriptable)
    {
        health = tankScriptable.health;
        tankType = tankScriptable.tankType;
        movementSpeed = tankScriptable.movementSpeed;
        turnSpeed = tankScriptable.turnSpeed;
        for (int i= 0; i < tankParts.Length; i++)
        {
            tankParts[i].material = tankScriptable.material;
        }

    }
}
