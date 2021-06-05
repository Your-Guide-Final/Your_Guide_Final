using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    PlayerControler pControler;
    private ReceptacleControler rControler;

    [SerializeField] float maxDistanceToHeal;
    [SerializeField] float coutAdrenaline;
    [SerializeField] int healValue;
    [SerializeField] float healCooldown;

    float timer;

    private void Awake()
    {
        pControler = transform.GetComponent<PlayerControler>();
        rControler = FindObjectOfType<ReceptacleControler>();
        timer = healCooldown;
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
        bool timerOk = timer >= healCooldown;
        bool enoughAdrenaline = pControler.pAdrenaline.adrenalineValue >= coutAdrenaline;

        bool canHeal = distance && timerOk && enoughAdrenaline;
        return canHeal;
    }

    public void Heal()
    {
        if (timer >= healCooldown)
        {
            pControler.pAdrenaline.AddAdrenalineValue(-coutAdrenaline);
            rControler.rLife.TakeDamage(-healValue);
            rControler.rAnimator.receptacleAnimator.SetTrigger(rControler.rAnimator.healParameterName);
            timer = 0;
        }

    }

    public void CoolDown()
    {
        if (timer < healCooldown)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, healCooldown);

        }
    }

}
