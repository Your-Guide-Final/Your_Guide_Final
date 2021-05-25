using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Cinemachine;

public class PlayerFX : MonoBehaviour
{
    PlayerControler pControler;
    public enum typeOfAttack { Attack1,Attack2,Attack3,Impact,Switch}
    public string attackEventName;
    public string degatEventName;

    public string positionDegatParameterName;

    [Header("Attack VFX")]
    [SerializeField] private VisualEffect attack1;
    [SerializeField] private VisualEffect attack2;
    [SerializeField] private VisualEffect attack3;
    [SerializeField] private VisualEffect attack3Impact;
    [SerializeField] private VisualEffect switchFX;

    [Header("Mouvement")]
    [SerializeField] private ParticleSystem walkParticule;
    public float maxParticuleEmissionRate;
    public AnimationCurve particuleEmissionOverAcceleration = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Camera Shake")]
    [SerializeField] AnimationCurve amplitudeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] float durationMin = 0.15f;
    [SerializeField] float durationMax = 0.5f;
    [SerializeField] float amplitudeMin = 1f;
    [SerializeField] float amplitudeMax = 10;
    [SerializeField] float frequency = 5f;
    [SerializeField] CinemachineBrain camBrain;



    private void Awake()
    {
        pControler = transform.GetComponent<PlayerControler>();

    }

    public void startFXAttack(typeOfAttack type)
    {
        switch(type)
        {
            case typeOfAttack.Attack1:

                attack1.SendEvent(attackEventName);
            
                break;

            case typeOfAttack.Attack2:

                attack2.SendEvent(attackEventName);

                break;

            case typeOfAttack.Attack3:

                attack3.SendEvent(attackEventName);
                //attack3Impact.SendEvent(attackEventName);

                break;
            case typeOfAttack.Impact:

                //attack3.SendEvent(attackEventName);
                attack3Impact.SendEvent(attackEventName);

                break;
            case typeOfAttack.Switch:
                switchFX.SendEvent(attackEventName);

                break;


        }
    }

    public IEnumerator StartFXAttackTiming(typeOfAttack type,float effectiveTime)
    {
        yield return new WaitForSeconds(effectiveTime);
        startFXAttack(type);
    }

    public void startCoroutineFX(typeOfAttack type, float effectiveTime)
    {
        StartCoroutine(StartFXAttackTiming(type, effectiveTime));
    }

    public void SetWalkParticuleEmissionRate(float effectiveTimeAcceleration)
    {
        var emission = walkParticule.emission;
        float newEmission = particuleEmissionOverAcceleration.Evaluate(effectiveTimeAcceleration) * maxParticuleEmissionRate;
        emission.rateOverTime = newEmission;
    }

    /* public void startFXDegat(typeOfAttack type, Vector3 position)
     {
         switch (type)
         {
             case typeOfAttack.Attack1:
                 attack1.SetVector3(positionDegatParameterName, position);
                 attack1.SendEvent(degatEventName);

                 break;

             case typeOfAttack.Attack2:
                 attack2.SetVector3(positionDegatParameterName, position);
                 attack2.SendEvent(degatEventName);

                 break;

             case typeOfAttack.Switch:
                 awitch.SetVector3(positionDegatParameterName, position);
                 Switch.SendEvent(degatEventName);
                 break;
         }
     }*/

    public void StartCameraShake(float ratio, float timeDelay)
    {
        StartCoroutine(Shake(ratio,timeDelay));
    }

    public IEnumerator Shake(float ratio, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        float timer = 0f;
        float duration = Mathf.Lerp(durationMin, durationMax, ratio);
        float amplitude = Mathf.Lerp(amplitudeMin, amplitudeMax, ratio);

        CinemachineVirtualCamera vCam = (CinemachineVirtualCamera)camBrain.ActiveVirtualCamera;
        CinemachineBasicMultiChannelPerlin p = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float progress = timer / duration;

            p.m_FrequencyGain = frequency;
            p.m_AmplitudeGain = amplitudeCurve.Evaluate(progress) * amplitude;

            yield return null;
        }

        //Reset shake values
        p.m_AmplitudeGain = 0f;
    }

}
