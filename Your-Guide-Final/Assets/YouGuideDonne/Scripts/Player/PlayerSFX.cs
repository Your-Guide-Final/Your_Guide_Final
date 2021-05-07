using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class PlayerSFX : MonoBehaviour
{
    PlayerControler pControler;

    

    //public AudioSource source;

    /*public AudioClip swordSlash;
    public AudioClip switchSlash;
    public AudioClip switchPossible;
    public AudioClip Stun;*/


    private void Awake()
    {
        pControler = transform.GetComponent<PlayerControler>();
        
    }

    public IEnumerator OneShotSound(float timing, string eventSfxName)
    {
        yield return new WaitForSeconds(timing);
        FMODUnity.RuntimeManager.PlayOneShot(eventSfxName, transform.position);
        
    }

    public void startCoroutineSound(float timing,string eventSfxName)
    {
        StartCoroutine(OneShotSound(timing, eventSfxName));
    }
}
