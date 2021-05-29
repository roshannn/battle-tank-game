using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : SingletonGeneric<TankController>
{
    [SerializeField] TankType tankType;
    [SerializeField] Joystick m_MoveJoystick;
    [SerializeField] Joystick m_TurnJoystick;
    [SerializeField] float m_MoveSpeed;
    [SerializeField] float m_TurnSpeed;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] int m_health;
    private Transform transform;
    private float m_Horizontal, m_Vertical;
    private float m_Turn;
    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
    private void Start()
    {

        CameraController.Instance.SetTarget(transform);
    }
    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        TankMove(m_Vertical);
        TankTurn(m_Horizontal);
    }

    private void TankTurn(float horizontal)
    {
        float turn = horizontal * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    private void TankMove(float vertical)
    {
        Vector3 movement = transform.forward * vertical * m_MoveSpeed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void GetInput()
    {
        m_Vertical = m_MoveJoystick.Vertical;
        m_Horizontal = m_TurnJoystick.Horizontal;
    }
}

