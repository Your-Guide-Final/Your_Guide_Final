using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControler pControler;

    [Header("Movement value")]
    [SerializeField] float speed = 10;
    [SerializeField] AnimationCurve accelerationCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] float accelerationDuration = 1;
    float timer;
    [Range(0,1)]
    [SerializeField] float minValueToMove = 0.1f;
    [SerializeField] float turnSmoothTime = 0.1f;

    [Header("Movement reference")]
    [SerializeField] Transform axeRota;
    [SerializeField] Transform cam;

    

    float turnSmoothVelocity;

    

    private void Awake()
    {
        pControler = transform.GetComponent<PlayerControler>();
        timer = 0;
    }

    public void Move(Vector3 directionMove,CharacterController charaController /*Rigidbody rigid*/)
    {
        //pControler.pAnimator.SetVitesseParameterValue(rigid.velocity.magnitude);
        pControler.pAnimator.SetVitesseParameterValue(pControler.pInput.GetDirectionInput().magnitude);

        Vector3 moveDirec=Vector3.zero;

        bool canMove = pControler.pStatue.canMove && !pControler.pStatue.stun && !pControler.pStatue.bump;

        if (directionMove.magnitude > minValueToMove && canMove)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, accelerationDuration * directionMove.magnitude);
            /*Vector3 moveDirection = directionMove.normalized;
            moveDirection.y = rigid.velocity.y;
            Transform pTransform = rigid.transform;

            float newXdirec = moveDirection.x * pTransform.forward.normalized.x;
            float newYdirec = moveDirection.y * pTransform.forward.normalized.y;
            float newZdirec = moveDirection.z * pTransform.forward.normalized.z;

            moveDirection = new Vector3(newXdirec, newYdirec, newZdirec);

            rigid.velocity = moveDirection * speed;


            *//*Quaternion rotaCam = cam.rotation;
            rotaCam = new Quaternion(0, rotaCam.y, 0, 0);*//*

            pTransform.rotation = new Quaternion(0, cam.rotation.y, 0,pTransform.rotation.w);

            lastDirection = pTransform.position + moveDirection;

            axeRota.LookAt(lastDirection);*/


            float targetAngle = Mathf.Atan2(directionMove.x, directionMove.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(axeRota.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            axeRota.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirec = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed ;
            float effectiveTime = timer / accelerationDuration;
            float accelerationValue = accelerationCurve.Evaluate(effectiveTime);
            pControler.pFX.SetWalkParticuleEmissionRate(effectiveTime);
            moveDirec *= accelerationValue;
            //Debug.Log(moveDirec);
            
        }
        else if (!pControler.pStatue.onRootMotion && !pControler.pStatue.bump)
        {
            timer = 0;
            moveDirec = Vector3.zero;
            pControler.pFX.SetWalkParticuleEmissionRate(0);
            /*Vector3 velocity = Vector3.zero;
            velocity.y = rigid.velocity.y;
            rigid.velocity = velocity;*/
        }

        if (charaController.enabled && !pControler.pStatue.bump && !pControler.pStatue.stun)
        {
            charaController.SimpleMove(moveDirec);

        }
    }

    public void ChangeDirection(Vector3 directionMove)
    {
        if(directionMove.magnitude > minValueToMove)
        {
            float targetAngle = Mathf.Atan2(directionMove.x, directionMove.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(axeRota.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            axeRota.rotation = Quaternion.Euler(0f, angle, 0f);

        }
    }


    public void StartCoroutineFakeRootMotion(float vitesse, AnimationCurve curve, float duration)
    {
        StartCoroutine(FakeRootMotion(vitesse, curve, duration));
    }

    public IEnumerator FakeRootMotion(float vitesse, AnimationCurve curve, float duration)
    {
        pControler.pStatue.onRootMotion = true;
        //Debug.Log("rootMotionOn");
        //Rigidbody rigid = pControler.rigid;
        CharacterController charaController = pControler.pCharacterController;
        Vector3 direction = axeRota.forward.normalized;
        float rootTimer = 0f;
        while (rootTimer < duration)
        {
            //Debug.Log(timer);
            float effectiveTime = rootTimer / duration;
            Vector3 newVelocity = direction * curve.Evaluate(effectiveTime);

            //rigid.velocity = newVelocity * vitesse;
            charaController.SimpleMove(newVelocity * vitesse);
            rootTimer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        pControler.pStatue.onRootMotion = false;
        //Debug.Log("rootMotionOff");
    }

    public IEnumerator BumpMovement(Vector3 bump, float bumpTime)
    {
        float bumpTimer = 0;
        //pControler.pStatue.bump = true;
        while (bumpTimer < bumpTime)
        {
            pControler.pCharacterController.SimpleMove(bump * Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //pControler.pStatue.bump = false;
    }

    public void StartBumpMovement(Vector3 bump, float bumpTime)
    {
        StartCoroutine(BumpMovement(bump, bumpTime));
    }
}
