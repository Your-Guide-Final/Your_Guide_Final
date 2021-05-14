using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ToolCoroutine : MonoBehaviour
{
    
    public IEnumerator PlayFxWithTime(float time, VisualEffect vfxUse, string vfxEventName)
    {
        yield return new WaitForSeconds(time);
        vfxUse.SendEvent(vfxEventName);
    }

    public void PlayCoroutineFxWithTime(float time, VisualEffect vfxUse, string vfxEventName)
    {
        StartCoroutine(PlayFxWithTime(time, vfxUse, vfxEventName));
    }

    public IEnumerator PlaySfxWithTime(float time, string eventName, Animator animator)
    {
        yield return new WaitForSeconds(time);
        FMODUnity.RuntimeManager.PlayOneShot(eventName, animator.transform.position);
    }

    public void PlayCoroutineSfxWithTime(float time, string eventName, Animator animator)
    {
        StartCoroutine(PlaySfxWithTime(time, eventName,animator));
    }
}
