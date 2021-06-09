using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    PlayerControler pControler;
    private ReceptacleControler rControler;

    [SerializeField] int healValue;
    [SerializeField] float timeBetweenHeal;

    [SerializeField] float maxDistanceToHeal;
    [SerializeField] float coutAdrenalineFor1HealValue;
    [SerializeField] float minAdrenalineValueToHeal;

    


    

    float timer;

    private void Awake()
    {
        pControler = transform.GetComponent<PlayerControler>();
        rControler = FindObjectOfType<ReceptacleControler>();
        timer = timeBetweenHeal;
    }

    public bool IsInRange()
    {
        if (rControler != null)
        {
            float distance = Vector3.Distance(this.transform.position, rControler.transform.position);
            bool canHeal = distance <= maxDistanceToHeal;

            return canHeal;
        }
        else
        {
            return false;
        }
    }

    public bool CanHeal()
    {
        bool distance = IsInRange();
        //bool timerOk = timer >= healCooldown;
        bool enoughAdrenaline = pControler.pAdrenaline.adrenalineValue > 0;

        bool canHeal = distance /*&& timerOk*/ && enoughAdrenaline && !rControler.rLife.IsLifeMax();
        return canHeal;
    }

    public bool EnoughAdrenalineToStartHeal()
    {
        bool enoughAdrenaline = pControler.pAdrenaline.adrenalineValue >= minAdrenalineValueToHeal;
        return enoughAdrenaline;
    }

    public void Heal()
    {
        CoolDown();

        if (timer >= timeBetweenHeal)
        {
            pControler.pAdrenaline.AddAdrenalineValue(-coutAdrenalineFor1HealValue);
            rControler.rLife.TakeDamage(-healValue);
            timer = 0;

        }
        /*if (timer >= healCooldown)
        {
            pControler.pAdrenaline.AddAdrenalineValue(-coutAdrenaline);
            rControler.rLife.TakeDamage(-healValue);
            rControler.rAnimator.receptacleAnimator.SetTrigger(rControler.rAnimator.healParameterName);
            timer = 0;
        }*/

    }

    public void CoolDown()
    {
        if (timer < timeBetweenHeal)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, timeBetweenHeal);

        }
    }

}
