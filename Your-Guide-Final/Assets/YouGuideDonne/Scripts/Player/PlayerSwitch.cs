using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSwitch : MonoBehaviour
{
    private PlayerControler pControler;
    private ReceptacleControler rControler;

    [Header("Switch Value")]
    [SerializeField] private float maxDistanceToSwtich;
    [SerializeField] private float minDistanceToSwtich;

    [Header("Timing Switch")]
    [SerializeField] private float timeToSwitch;
    [SerializeField] private float timeAnimeAttSwitch;
    [SerializeField] private float timePlayerStun;

    [Header("Sign/Feedback")]
    //[SerializeField] private GameObject signCanSwitch;
    

    private bool OnSwitch;
    private Vector3 newPosPlayer;
    private Vector3 newPosReceptacle;
    // Start is called before the first frame update
    private void Awake()
    {
        rControler = FindObjectOfType<ReceptacleControler>();
        pControler = transform.GetComponent<PlayerControler>();
        OnSwitch = false;
        
    }

    public bool IsInRange()
    {
        if (rControler != null)
        {
            float distanceReceptacle = Vector3.Distance(transform.position, rControler.transform.position);
            bool inRange = distanceReceptacle <= maxDistanceToSwtich && distanceReceptacle >= minDistanceToSwtich;
            return inRange;

        }
        return false;
    }

    public void StartSwitch()
    {
        if (!OnSwitch)
        {
            newPosPlayer = rControler.transform.position;
            newPosReceptacle = transform.position;
            Debug.Log(newPosPlayer);
            Debug.Log(newPosReceptacle);

            StartCoroutine(Switch());
        }
    }


    public IEnumerator Switch()
    {
        OnSwitch = true;
        pControler.pAdrenaline.adrenalineValue = 0;
        pControler.pCharacterController.enabled = false;

        /*Vector3 newPosPlayer = rControler.transform.position;
        Vector3 newPosReceptacle = transform.position;*/

        yield return new WaitForSeconds(timeToSwitch);

        //pControler.pAnimator.TriggerAttparameter();

        rControler.transform.position = newPosReceptacle;
        transform.position = newPosPlayer;

        rControler.rFX.PlaySwitchParticule();

        rControler.rAnimator.receptacleAnimator.SetBool(rControler.rAnimator.switchParametername, true);
        rControler.rAnimator.receptacleAnimator.SetTrigger(rControler.rAnimator.switchTriggerParametername);
        pControler.pAnimator.TriggerSwitchParameter();

        yield return new WaitForSeconds(timeAnimeAttSwitch);

       // pControler.pAnimator.TriggerAttparameter();

        
        yield return new WaitForSeconds(timePlayerStun);

        pControler.pAnimator.TriggerAttparameter();
        rControler.rAnimator.receptacleAnimator.SetBool(rControler.rAnimator.switchParametername, false);

        pControler.pCharacterController.enabled = true;
        OnSwitch = false;
    }

    /*public void ChangeSignEnable(bool etat)
    {
        
        signCanSwitch.SetActive(etat);
    }*/


}
