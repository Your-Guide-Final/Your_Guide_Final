using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    [FMODUnity.EventRef]
    [SerializeField] string musicEvent;
    //[FMODUnity.ParamRef]
    [SerializeField] string soundBattleParameter;
    [SerializeField] float onBattleSoundParameterValue;
    [SerializeField] float offBattleSoundParameterValue;

    [SerializeField] string soundLifeParameter;

    FMOD.Studio.EventInstance eIntance;
    FMOD.Studio.PARAMETER_ID battleParameterId;
    FMOD.Studio.PARAMETER_ID lifeParameterId;
    
    private void Awake()
    {
        eIntance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        FMOD.Studio.EventDescription battleEventDescription;
        eIntance.getDescription(out battleEventDescription);

        FMOD.Studio.PARAMETER_DESCRIPTION battleParameterDescription;
        battleEventDescription.getParameterDescriptionByName(soundBattleParameter, out battleParameterDescription);
        
        battleParameterId = battleParameterDescription.id;
        
        FMOD.Studio.PARAMETER_DESCRIPTION lifeParameterDescription;
        battleEventDescription.getParameterDescriptionByName(soundLifeParameter, out lifeParameterDescription);

        lifeParameterId = lifeParameterDescription.id;


        eIntance.start();

    }

    public void StartBattleMusic()
    {
        eIntance.setParameterByID(battleParameterId, onBattleSoundParameterValue);
       
        //eIntance.getParameterByID(battleParameterId, out cancer);
        //Debug.Log("Start Battle Music " + cancer);
        //Debug.Log(battleParameterId.data1);
    }

    public void StopBattleMusic()
    {
        eIntance.setParameterByID(battleParameterId, offBattleSoundParameterValue);
        //Debug.Log("Stop Battle Music " + cancer);
    }

    public void SetLifeMusicValue(float value)
    {
        float cancer;
        eIntance.setParameterByID(lifeParameterId, value);
        eIntance.getParameterByID(battleParameterId, out cancer);
        Debug.Log("Life Value Music : " + cancer);
    }

}
