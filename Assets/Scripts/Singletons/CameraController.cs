using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoSingletonGeneric<CameraController>
{
    private CinemachineVirtualCamera m_VirtualCamera;

    protected override void Awake()
    {
        base.Awake();
        m_VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetTarget(Transform target)
    {
        m_VirtualCamera.Follow = target;
    }
}
