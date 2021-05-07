using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class EnemiSFX : MonoBehaviour
{
    EnemiControler eControler;

    

    private void Awake()
    {
        eControler = transform.GetComponent<EnemiControler>();
    }

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
