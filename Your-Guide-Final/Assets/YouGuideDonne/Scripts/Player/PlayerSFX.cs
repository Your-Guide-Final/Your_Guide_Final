using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class PlayerSFX : MonoBehaviour
{
    PlayerControler pControler;

    public AudioSource source;

    /*public AudioClip swordSlash;
    public AudioClip switchSlash;
    public AudioClip switchPossible;
    public AudioClip Stun;*/
    

    private void Awake()
    {
        pControler = transform.GetComponent<PlayerControler>();
        if (source == null)
        {
            source = GetComponent<AudioSource>();
        }
    }

    public IEnumerator startSound(float timing, bool loop, AudioClip clip)
    {
        yield return new WaitForSeconds(timing);
        source.Stop();
        if (loop)
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
        }
        else
        {
            source.loop = false;
            source.PlayOneShot(clip);
        }
    }

    public void startCoroutineSound(float timing, bool loop, AudioClip clip)
    {
        StartCoroutine(startSound(timing, loop, clip));
    }
}
