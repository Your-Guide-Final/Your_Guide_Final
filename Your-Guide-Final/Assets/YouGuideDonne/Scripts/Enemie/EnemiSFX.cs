using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiSFX : MonoBehaviour
{
    EnemiControler eControler;

    public AudioSource source;

    private void Awake()
    {
        eControler = transform.GetComponent<EnemiControler>();
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
