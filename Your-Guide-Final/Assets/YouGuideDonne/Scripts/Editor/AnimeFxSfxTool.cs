using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.VFX;



public class AnimeFxSfxTool : EditorWindow
{
    public Animator animator;
    public AnimatorOverrideController aoc;

    public string debugAnimeStateName;
    public string defaultAnimeStateName;
    public List<AnimationClip> animesClip;

    public bool useVfx;
    public List<VisualEffect> vfx;
    public string vfxEnventName;
    public float vfxTiming;

    public bool useSfx;
    [FMODUnity.EventRef]
    public List<string> sfxEventName;
    public float sfxTiming;

    private AnimationClip currentAnimeCLip;
    private VisualEffect currentVfx;
    private string currentSfxEvent;


    [MenuItem("Tools/AnimeTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AnimeFxSfxTool));
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Animator Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);

        SerializedProperty animatorProperty = so.FindProperty("animator");
        SerializedProperty animatorOverriderProperty = so.FindProperty("aoc");
        SerializedProperty animesClipProperty = so.FindProperty("animesClip");
        SerializedProperty vfxProperty = so.FindProperty("vfx");
        SerializedProperty sfxProperty = so.FindProperty("sfxEventName");

        EditorGUILayout.PropertyField(animatorProperty, true);
        EditorGUILayout.PropertyField(animatorOverriderProperty, true);


        debugAnimeStateName = EditorGUILayout.TextField("Debug State Name", debugAnimeStateName);
        defaultAnimeStateName = EditorGUILayout.TextField("Default State Name", defaultAnimeStateName);

        

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animation Settings", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(animesClipProperty, true);

        EditorGUILayout.Space();

        useVfx = EditorGUILayout.Toggle("Use VFX ?", useVfx);
        if (useVfx)
        {
            EditorGUILayout.PropertyField(vfxProperty, true);
            vfxEnventName = EditorGUILayout.TextField("VFX Event Name", vfxEnventName);
            vfxTiming = EditorGUILayout.FloatField("Time before Play VFX", vfxTiming);
        }

        EditorGUILayout.Space();

        useSfx = EditorGUILayout.Toggle("Use SFX ?", useSfx);

        if (useSfx)
        {
            EditorGUILayout.PropertyField(sfxProperty, true);
            sfxTiming = EditorGUILayout.FloatField("Time before Play SFX", sfxTiming);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (Application.isPlaying)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Start"))
            {
                StartDebug();
            }
            if (GUILayout.Button("Stop"))
            {
                StopDebug();
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Value Animation Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (animesClip.Count > 0)
            {
                EditorGUILayout.LabelField("Animation Clip Settings", EditorStyles.boldLabel);
                GUILayout.BeginVertical("box");

                for (int i = 0; i < animesClip.Count; i++)
                {
                    if (vfx[i] != null)
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(animesClip[i].name, EditorStyles.boldLabel);
                        if (animesClip[i] != currentAnimeCLip)
                        {
                            if (GUILayout.Button("Set Value"))
                            {
                                currentAnimeCLip = animesClip[i];
                            }

                        }
                        else
                        {
                            if (GUILayout.Button("Reset Value"))
                            {
                                currentAnimeCLip = null;
                            }
                        }
                        GUILayout.EndHorizontal();

                    }
                }
                GUILayout.EndVertical();
            }

            EditorGUILayout.Space();

            if (useVfx)
            {
                if (vfx.Count > 0)
                {
                    EditorGUILayout.LabelField("Vfx Settings", EditorStyles.boldLabel);
                    GUILayout.BeginVertical("box");

                    for (int i = 0; i < vfx.Count; i++)
                    {
                        if (vfx[i] != null)
                        {
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(vfx[i].name, EditorStyles.boldLabel);
                            if(vfx[i]!= currentVfx)
                            {
                                if (GUILayout.Button("Set Value"))
                                {
                                    currentVfx = vfx[i];
                                }
                                
                            }
                            else
                            {
                                if (GUILayout.Button("Reset Value"))
                                {
                                    currentVfx = null;
                                }
                            }
                            GUILayout.EndHorizontal();

                        }
                    }
                    GUILayout.EndVertical();
                }

            }

            EditorGUILayout.Space();

            if (useSfx)
            {
                if (sfxEventName.Count > 0)
                {
                    EditorGUILayout.LabelField("Sfx Settings", EditorStyles.boldLabel);
                    GUILayout.BeginVertical("box");

                    for (int i = 0; i < sfxEventName.Count; i++)
                    {
                        if (sfxEventName[i] != null)
                        {
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(sfxEventName[i], EditorStyles.boldLabel);
                            if (sfxEventName[i] != currentSfxEvent)
                            {
                                if (GUILayout.Button("Set Value"))
                                {
                                    currentSfxEvent = sfxEventName[i];
                                }

                            }
                            else
                            {
                                if (GUILayout.Button("Reset Value"))
                                {
                                    currentSfxEvent = null;
                                }
                            }
                            GUILayout.EndHorizontal();

                        }
                    }
                    GUILayout.EndVertical();
                }

            }

        }

        so.ApplyModifiedProperties();
    }


    public void SetDebugAnime(AnimationClip animeClip)
    {
        aoc["Debug"] = animeClip;
        animator.runtimeAnimatorController = aoc;
    }

    public IEnumerator PlayFxWithTime(float time, VisualEffect vfxUse)
    {
        yield return new WaitForSeconds(time);
        vfxUse.SendEvent(vfxEnventName);
    }

    public IEnumerator PlaySfxWithTime(float time, string eventName)
    {
        yield return new WaitForSeconds(time);
        FMODUnity.RuntimeManager.PlayOneShot(eventName, animator.transform.position);
    }

     


    public void StartDebug()
    {
        if (Application.isPlaying)
        {
            if (animator != null)
            {
                if (defaultAnimeStateName == "")
                {
                    UnityEngine.Debug.LogError("Default State Name can not be null");
                }
                else if (debugAnimeStateName == "")
                {
                    UnityEngine.Debug.LogError("Debug State Name can not be null");
                }
                else
                {
                    SetDebugAnime(currentAnimeCLip);

                    animator.Play(debugAnimeStateName,0);
                    

                    if (useVfx)
                    {
                        if (currentVfx != null)
                        {
                            
                            PlayFxWithTime(vfxTiming, currentVfx);

                        }
                        else
                        {
                            Debug.LogWarning("You need to set a Vfx Value first");
                        }
                    }
                    if (useSfx)
                    {
                        if (currentSfxEvent != null)
                        {
                            PlaySfxWithTime(sfxTiming, currentSfxEvent);

                        }
                        else
                        {
                            Debug.LogWarning("You need to set a Sfx Value first");
                        }
                    }
                    
                }

            }
            else
            {
                UnityEngine.Debug.LogError("Animator can not be null");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("You need to be in Play to use this function");
        }
    }

    public void SetValue()
    {

    }


    public void StopDebug()
    {
        if (Application.isPlaying)
        {
            if (defaultAnimeStateName == "")
            {
                UnityEngine.Debug.LogError("Default State Name can not be null");
            }
            else
            {
                animator.Play(defaultAnimeStateName,0);
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("You need to be in Play to use this function");
        }
    }


}
