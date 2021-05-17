﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : SingletonGeneric<TankController>
{
    [SerializeField] Joystick joystick;
    [SerializeField] float m_MoveSpeed;
    [SerializeField] float m_TurnSpeed;
    [SerializeField] Rigidbody m_Rigidbody;
    private float m_Horizontal, m_Vertical;
    private float m_Turn;
    private void Start()
    {
    }
    private void Update()
    {

        GetInput();
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
        m_Vertical = joystick.Vertical;
        m_Horizontal = joystick.Horizontal;
    }
}

