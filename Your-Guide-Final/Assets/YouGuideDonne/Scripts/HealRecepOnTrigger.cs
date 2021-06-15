using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HealRecepOnTrigger : MonoBehaviour
{
    bool alreadyTrigger;

    [FMODUnity.EventRef]
    [SerializeField] private string eventHealSfxName;
    [SerializeField] VisualEffect healVfx;
    [SerializeField] string eventVfx;
    [SerializeField] int healValue = 10;
    [SerializeField] float timeBetweenHeal = 0.3f;

    private void Awake()
    {
        alreadyTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        ReceptacleControler rControler = other.GetComponent<ReceptacleControler>();

        if (rControler != null)
        {
            if (!alreadyTrigger)
            {
                alreadyTrigger = true;                
                FMODUnity.RuntimeManager.PlayOneShot(eventHealSfxName, rControler.transform.position);
                StartCoroutine(FullHealRecep(rControler));
                
            }
        }
    }


    public IEnumerator FullHealRecep(ReceptacleControler rControler)
    {
        float timer = timeBetweenHeal;
        while (!rControler.rLife.IsLifeMax())
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenHeal)
            {
                rControler.rLife.TakeDamage(-healValue);
                //FMODUnity.RuntimeManager.PlayOneShot(eventHealSfxName, rControler.transform.position);
                healVfx.SendEvent(eventVfx);
                timer = 0;

            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
