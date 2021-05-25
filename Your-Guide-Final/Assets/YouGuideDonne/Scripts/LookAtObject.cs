using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    Transform objectToLook;

    private void Awake()
    {
        objectToLook = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 directionLook = transform.position - objectToLook.position;
        transform.forward = directionLook;
    }
}
