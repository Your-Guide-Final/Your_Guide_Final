using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptacleFX : MonoBehaviour
{
    ReceptacleControler rControler;

    [SerializeField] ParticleSystem SwitchParticule;

    private void Awake()
    {
        rControler = transform.GetComponent<ReceptacleControler>();
    }

    public void PlaySwitchParticule()
    {
        SwitchParticule.Play();
    }



}
