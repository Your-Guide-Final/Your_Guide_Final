using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptacleSFX : MonoBehaviour
{
    

    public IEnumerator OneShotSound(float timing, string eventSfxName)
    {
        yield return new WaitForSeconds(timing);
        FMODUnity.RuntimeManager.PlayOneShot(eventSfxName, transform.position);

    }

    public void startCoroutineSound(float timing, string eventSfxName)
    {
        StartCoroutine(OneShotSound(timing, eventSfxName));
    }
}
