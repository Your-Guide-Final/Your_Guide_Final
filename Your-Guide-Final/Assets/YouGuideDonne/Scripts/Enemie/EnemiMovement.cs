using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiMovement : AiMovementWithVelocity
{
    [SerializeField] private float vitesse = 6;
    /*[SerializeField] private AnimationCurve accelerationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float accelerationDuration = 1;*/

    [Header("Distance")]
    [SerializeField] private float minDistanceToFollowReceptacle = 20;
    [SerializeField] private float minDistanceToFollowPlayer = 6;
    [SerializeField] private float minDistanceToAttack = 3;

    [Header("Timer")]
    [SerializeField] private float refreshFrequency = 0.25f;

    private EnemiControler eControler;
    private ReceptacleControler rControler;
    private PlayerControler pControler;

    float timer;


    [HideInInspector] public Transform currentTarget;

    private void Awake()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        rigid = transform.GetComponent<Rigidbody>();
        path = new NavMeshPath();
        rControler = FindObjectOfType<ReceptacleControler>();
        pControler = FindObjectOfType<PlayerControler>();
        eControler = GetComponent<EnemiControler>();
        timer = 0f;
    }

    public bool IsInRangeToFollowReceptacle()
    {
        if (rControler != null)
        {
            float distance = Vector3.Distance(rControler.transform.position, transform.position);
            bool inRange = distance <= minDistanceToFollowReceptacle;
            return inRange;

        }
        return false;
    }

    public bool IsInRangeToFollowPlayer()
    {
        float distance = Vector3.Distance(pControler.transform.position, transform.position);
        bool inRange = distance <= minDistanceToFollowPlayer;
        return inRange;
    }

    public bool IsInRangeToAttackTarget()
    {
        if (currentTarget != null)
        {
            float distance = Vector3.Distance(currentTarget.position, transform.position);
            bool inRange = distance <= minDistanceToAttack;
            return inRange;

        }

        return false;
    }


    public void ChooseTarget()
    {
        if (IsInRangeToFollowReceptacle())
        {
            currentTarget = rControler.transform;
            return;
        }
        else if (IsInRangeToFollowPlayer())
        {
            currentTarget = pControler.transform;
            return;
        }
        else
        {
            currentTarget = null;
        }
    }

    public void InizialisePath()
    {
        if (currentTarget)
        {
            Calculatepath(currentTarget);

        }
    }

    public void Follow()
    {

        if (currentTarget)
        {
            CheckTargetReach(currentTarget);
            timer += Time.deltaTime;

        }

        if(!eControler.eStatue.stun && !eControler.eStatue.bump)
        {
            MoveToCible(vitesse);
            
        }
        

        if(!shouldMove && currentTarget)
        {
            InizialisePath();
        }

        if (timer >= refreshFrequency)
        {
            InizialisePath();
            timer = 0;
        }
    }

    



}
