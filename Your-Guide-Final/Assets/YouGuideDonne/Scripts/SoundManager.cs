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

    private void Awake()
    {
        eIntance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        FMOD.Studio.EventDescription battleEventDescription;
        eIntance.getDescription(out battleEventDescription);

        FMOD.Studio.PARAMETER_DESCRIPTION battleParameterDescription;
        battleEventDescription.getParameterDescriptionByName(soundBattleParameter, out battleParameterDescription);

        battleParameterId = battleParameterDescription.id;

        eIntance.start();
    }

    public void StartBattleMusic()
    {
        eIntance.setParameterByID(battleParameterId, onBattleSoundParameterValue);
    }

    public void StopBattleMusic()
    {
        eIntance.setParameterByID(battleParameterId, offBattleSoundParameterValue);
    }

}
