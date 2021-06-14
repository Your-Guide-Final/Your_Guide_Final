using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera")]
    public List<CinemachineVirtualCamera> cameraList= new List<CinemachineVirtualCamera>();

    public int activeCameraIndex;

    [SerializeField] int camPriorityOff = 0;
    [SerializeField] int camPriorityOn = 1;

    public CinemachineTargetGroup targetGroupCombat;

    [Header("Camera Shake")]
    //[SerializeField] AnimationCurve amplitudeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    /*[SerializeField] float durationMin = 0.15f;
    [SerializeField] float durationMax = 0.5f;
    [SerializeField] float amplitudeMin = 1f;
    [SerializeField] float amplitudeMax = 10;
    [SerializeField] float frequency = 5f;*/
    [SerializeField] CinemachineBrain camBrain;

    private void Awake()
    {
        ChangeActifCamera(activeCameraIndex);
    }

    public void ChangeActifCamera(int indexCam)
    {
        CinemachineVirtualCamera currentCamera = cameraList[activeCameraIndex];
        CinemachineVirtualCamera newCamera = cameraList[indexCam];

        currentCamera.Priority = camPriorityOff;
        newCamera.Priority = camPriorityOn;
        activeCameraIndex = indexCam;
    }

    public void StartCameraShake( float timeDelay, float duration, float amplitude, float frequency, AnimationCurve AmplitudeCurve)
    {
        StopCoroutine(Shake(timeDelay, duration, amplitude, frequency, AmplitudeCurve));
        StartCoroutine(Shake(timeDelay, duration, amplitude, frequency, AmplitudeCurve));
    }

    public IEnumerator Shake( float timeDelay,float duration, float amplitude, float frequency,AnimationCurve amplitudeCurve)
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
