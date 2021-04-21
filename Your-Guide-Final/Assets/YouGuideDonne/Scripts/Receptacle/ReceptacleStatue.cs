using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptacleStatue : MonoBehaviour
{
    ReceptacleControler rControler;

    [Header("Movement")]
    public bool isFollow;
    public bool isSprint;
    public bool isStun;

    [Header("Battle")]
    public bool isScared;
    //[SerializeField] private float minDistanceToBeScared;

    private void Awake()
    {
        rControler = transform.GetComponent<ReceptacleControler>();
        isFollow = false;
        isStun = false;
        isScared = false;
    }

    public void StartBump(Vector3 bumpForce, float timeStun)
    {
        if (!isStun)
        {
            StartCoroutine(Bump(bumpForce, timeStun));

        }
    }

    public IEnumerator Bump(Vector3 bumpForce, float timeStun)
    {
        isStun = true;

        rControler.rAnimator.receptacleAnimator.SetTrigger(rControler.rAnimator.stunTriggerParameterName);
        rControler.rAnimator.receptacleAnimator.SetBool(rControler.rAnimator.stunParameterName,true);

        rControler.rigid.AddForce(bumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(timeStun);

        isStun = false;
        rControler.rAnimator.receptacleAnimator.SetBool(rControler.rAnimator.stunParameterName,false);
        
    }

}
