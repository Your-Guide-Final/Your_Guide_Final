using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AiMovementWithVelocity : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool debug;
    [SerializeField] private Color gizmoColor;
    [SerializeField] private float gizmoSize;

    protected NavMeshAgent agent;
    protected NavMeshPath path;
    protected Rigidbody rigid;

    //pos private variable
    private int index;
    private Vector3 currentTargetPos;
    protected bool shouldMove;



    //movement variable
    [Header("Movement")]
    [SerializeField] private float pathMinDistance = 0.5f;
    
    



    // Start is called before the first frame update
    private void Awake()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        rigid = transform.GetComponent<Rigidbody>();
        path = new NavMeshPath();
    }

    

    public void Calculatepath(Transform target)
    {
        agent.enabled = true;
        if (agent.CalculatePath(target.position, path))
        {
            index = 0;
            currentTargetPos = path.corners[0];
            shouldMove = true;
        }
        agent.enabled = false;
    }


    public void CheckTargetReach(Transform target)
    {
        float distanceToTarget = Vector3.Distance(currentTargetPos, transform.position);
        if(distanceToTarget<= pathMinDistance)
        {
            if(index<path.corners.Length - 1)
            {
                index++;
                rigid.velocity = Vector3.zero;
                currentTargetPos = path.corners[index];
            }
            else
            {
                rigid.velocity = Vector3.zero;
                shouldMove = false;
                
            }
        }
    }


    public void MoveToCible(float vitesse)
    {
        Vector3 toTargetPos = (currentTargetPos - transform.position).normalized;
        Vector3 posToLook = transform.position + toTargetPos;
        if (shouldMove)
        {
            rigid.velocity = toTargetPos * vitesse;
            transform.LookAt(posToLook);
        }
    }

    public void StopMovement()
    {
        shouldMove = false;
        rigid.velocity = Vector3.zero;
    }

    public virtual void OnDrawGizmos()
    {
        if (debug)
        {
            //Debug.Log("Gizmo On");
            Gizmos.color = gizmoColor;
            if (path != null)
            {
                //Debug.Log("Gizmo Draw");
                for (int i = 0; i < path.corners.Length; i++)
                {
                    Gizmos.DrawSphere(path.corners[i], gizmoSize);
                }
            }
        }
    }

}
