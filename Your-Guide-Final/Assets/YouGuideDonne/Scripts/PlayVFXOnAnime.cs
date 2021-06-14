using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class PlayVFXOnAnime : MonoBehaviour
{
    [SerializeField] VisualEffect vfx;
    //[SerializeField] string eventName;


    public void playFx(string eventName)
    {
        if (vfx != null)
        {
            vfx.SendEvent(eventName);
        }
    }
}
