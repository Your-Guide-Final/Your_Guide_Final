using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamExplo : MonoBehaviour
{
    [SerializeField] CameraManager camManager;
    [SerializeField] int cameraIndex = 1;
    //[SerializeField] string playerTag;

    private void Awake()
    {
        if (camManager == null)
        {
            camManager = FindObjectOfType<CameraManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler pControler = other.GetComponent<PlayerControler>();
        bool isPlayer = pControler != null;
        if (isPlayer)
        {
            camManager.ChangeActifCamera(1);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerControler pControler = other.GetComponent<PlayerControler>();
        bool isPlayer = pControler != null;
        if (isPlayer)
        {
            camManager.ChangeActifCamera(0);

        }
    }
}
