using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class FootStepSfx : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField] string eventSfxName;

    private void OnTriggerEnter(Collider other)
    {
        //UnityEngine.Debug.Log("trigger");
        FMODUnity.RuntimeManager.PlayOneShot(eventSfxName, transform.position);
    }

}
