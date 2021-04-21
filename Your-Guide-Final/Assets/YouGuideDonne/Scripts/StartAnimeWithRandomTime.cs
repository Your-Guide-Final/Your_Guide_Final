using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimeWithRandomTime : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName;
    [SerializeField] private float maxTimeToStart;



    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(StartAnime());
    }

    public IEnumerator StartAnime()
    {
        float Time = Random.Range(0, maxTimeToStart);
        yield return new WaitForSeconds(Time);
        animator.SetTrigger(triggerName);
    }
}
