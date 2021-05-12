using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Animator animator;

    //public UnityEditor.Animations.AnimatorController an;

    //public List<UnityEditor.Animations.AnimatorState> states;

    public AnimatorOverrideController aoc;
    public AnimationClip animeClip;
    public string testanime;

    private void Start()
    {
        animator.runtimeAnimatorController = aoc;
        aoc[testanime] = animeClip;

        /*AnimationClip[] clips = aoc.animationClips;
        int nb = 0;*/

        /*foreach (var clip in clips)
        {
            aoc.g
            if (clip.name == "Debug")
            {
                nb++;
            }
        }

        Debug.Log("nb debug anime =" + nb);*/

        
        //UnityEditor.Animations.AnimatorController ac = transform.GetComponent<Animator>().runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        
        //UnityEditorInternal.State$$anonymous$$achine sm_;
        //UnityEditor.Animations.AnimatorState sm = ac.GetLayerState$$anonymous$$achine(0);


        //UnityEditor.Animations.AnimatorControllerLayer layer = sm.statesRecursive;
    }


}
