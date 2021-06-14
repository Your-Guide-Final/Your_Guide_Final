using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureControler : MonoBehaviour
{
    Animator animator;

    [Header("Animator Parameter")]
    [SerializeField] string speedParameter;
    [SerializeField] string drinkParameter;
    [SerializeField] string ChokParameter;
    [SerializeField] string IddleParameter;

    [Header("Timing")]
    
    [SerializeField] float maxTimeToSecondIdle;
    [SerializeField] bool canDrink;
    [SerializeField] float maxTimeToDrink;
    [SerializeField] float TimeDrink;

    [Header("Position")]
    [SerializeField] Transform posIdle;
    [SerializeField] Transform posToHide;

    [Header("Movement")]
    [SerializeField] float speedRun;
    [SerializeField] float speedWalk;


    bool scared;
    bool onDrink;

    float idleTimer;
    float drinkTimer;


    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        idleTimer = 0;
        drinkTimer = 0;
        scared = false;
        onDrink = false;
    }

    private void Update()
    {
        
    }





}
