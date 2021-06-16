using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuOnInput : MonoBehaviour
{
    [SerializeField] string input = "Fire3";
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    [SerializeField] int newCameraIndex = 0;
    [SerializeField] bool changeCam = true;

    CameraManager cam;
    private void Awake()
    {
        cam = FindObjectOfType<CameraManager>();
    }


    private void Update()
    {
        if (Input.GetButtonDown(input))
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
            if (changeCam)
            {
                cam.ChangeActifCamera(0);

            }
        }
    }

}
