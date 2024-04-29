using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerRootFinder : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
        Transform playerRoot = GameObject.FindGameObjectWithTag("CinemachineTarget").transform;

        virtualCamera.Follow = playerRoot;
        virtualCamera.LookAt = playerRoot;


    }

}
