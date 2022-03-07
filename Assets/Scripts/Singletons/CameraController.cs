using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera m_VirtualCamera;

    private void Awake()
    {
        m_VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetTarget(Transform target)
    {
        m_VirtualCamera.m_Lens.NearClipPlane = -20;
        m_VirtualCamera.Follow = target;
    }
}
