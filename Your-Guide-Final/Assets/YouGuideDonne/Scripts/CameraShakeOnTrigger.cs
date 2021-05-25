using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeOnTrigger : MonoBehaviour
{
    [Header("Trigger Parameter")]
    [SerializeField] bool onlyOnce;


    [SerializeField] float cameraShakeTimeDelay;

    bool alreadyTrigger;

    [Header("Camera Shake")]
    [SerializeField] AnimationCurve amplitudeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    [SerializeField] float duration = 0.5f;

    [SerializeField] float amplitude = 10;
    [SerializeField] float frequency = 5f;
    [SerializeField] CinemachineBrain camBrain;

    private void Awake()
    {
        alreadyTrigger = false;
        if (!camBrain)
        {
            camBrain = FindObjectOfType<CinemachineBrain>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyTrigger)
        {
            PlayerControler pControler = other.GetComponent<PlayerControler>();
            if (pControler)
            {
                StartCoroutine(Shake(cameraShakeTimeDelay));
                if (onlyOnce)
                {
                    alreadyTrigger = true;
                }
            }
        }
    }

    public IEnumerator Shake( float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        float timer = 0f;


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
