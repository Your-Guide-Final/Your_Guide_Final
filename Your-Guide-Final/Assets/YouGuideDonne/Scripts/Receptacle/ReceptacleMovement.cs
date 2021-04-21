using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReceptacleMovement : AiMovementWithVelocity
{
    public float vitesseFollow;
    public float vitesseSprint;

    [Header("Movement Distance")]
    [SerializeField] private float minDistanceToFollow;
    [SerializeField] private float minDistanceToSprint;
    [SerializeField] private float minDistanceToStop;
    [SerializeField] private float minDistanceToBeScared;

    [Header("Timer")]
    [SerializeField] private float refreshFrequency = 0.25f;

    [Header("FeedBack")]
    [SerializeField] private ParticleSystem walkParticule;
    public float emissionRateWalk;
    public float emissionRateRun;


    private PlayerControler player;
    private ReceptacleControler rControler;


    float timer;

    private void Awake()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        rigid = transform.GetComponent<Rigidbody>();
        path = new NavMeshPath();
        timer = 0;
        player = FindObjectOfType<PlayerControler>();
        Calculatepath(player.transform);
        rControler = GetComponent<ReceptacleControler>();
    }


    public bool IsInRangeToFollow()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        bool InRange = distance >= minDistanceToFollow;
        return InRange;
    }

    public bool IsInRangeToSprint()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        bool InRange = distance >= minDistanceToSprint;
        return InRange;
    }

    public bool IsInRangeToStop()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        bool InRange = distance <= minDistanceToStop;
        return InRange;
    }

    public bool IsInrangeToBeScared()
    {
        EnemiControler[] enemiList = FindObjectsOfType<EnemiControler>();
        if (enemiList.Length != 0)
        {
            foreach (EnemiControler eControler in enemiList)
            {
                Vector3 posE = eControler.transform.position;
                float distance = Vector3.Distance(posE, transform.position);
                bool InRange = distance <= minDistanceToBeScared;
                if (InRange)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }


    public void InizialisePath()
    {
        Calculatepath(player.transform);
    }

    public void Follow(float speed, float emissionRate)
    {
        
        CheckTargetReach(player.transform);
        timer += Time.deltaTime;

        if(!rControler.rStatue.isStun && !rControler.rStatue.isScared)
        {
            MoveToCible(speed);
            var emission = walkParticule.emission;
            emission.rateOverTime = emissionRate;
        }

        if(!shouldMove && IsInRangeToFollow())
        {
            InizialisePath();
        }

        if (timer >= refreshFrequency)
        {
            InizialisePath();
            timer = 0;
        }
    }

    public void Sprint()
    {
        CheckTargetReach(player.transform);
        MoveToCible(vitesseSprint);
        if (!shouldMove && IsInRangeToSprint())
        {
            InizialisePath();
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public void SetEmissionParticuleToNull()
    {
        var emission = walkParticule.emission;
        emission.rateOverTime = 0;
    }
}
