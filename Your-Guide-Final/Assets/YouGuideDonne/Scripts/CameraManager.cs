using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameraList= new List<CinemachineVirtualCamera>();

    public int activeCameraIndex;

    [SerializeField] int camPriorityOff = 0;
    [SerializeField] int camPriorityOn = 1;

    private void Awake()
    {
        ChangeActifCamera(activeCameraIndex);
    }

    public void ChangeActifCamera(int indexCam)
    {
        CinemachineVirtualCamera currentCamera = cameraList[activeCameraIndex];
        CinemachineVirtualCamera newCamera = cameraList[indexCam];

        currentCamera.Priority = camPriorityOff;
        newCamera.Priority = camPriorityOn;
        activeCameraIndex = indexCam;
    }
}
