using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatStateMachine : Enemy1Controller
{
    [SerializeField] private float attackSpeed = 8f;
    [SerializeField] private float cooldown = 4;
    [SerializeField] private GameObject ballOfFur;
    [SerializeField] private GameObject ballOfWool;
    private Animator animator;
    private bool startFight;
    private bool atacando = false;
    public CatBaseState CurrentState { get; private set; }

    private void Awake()
    {
        startFight = true;
        atacando = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (notInRoom == false)
        {
            if (died == true)
            {
                time += Time.deltaTime;
                if (time > 0.9f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (GameControl.multiplayer == true)
                {
                    if (IsPlayerAlive())
                    {
                        GetNearestPlayer();
                    }
                }
                if (startFight)
                {
                    SwitchState(new CatAttack());
                    startFight = false;
                }
                else
                {
                    CurrentState.UpdateState(this);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (atacando)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerLife>().PlayerDamage();
            }
        }
    }
    public int Randomize()
    {
        System.Random aleatorio = new System.Random();
        int valor = aleatorio.Next();
        return (valor % 10);
    }
    
    public void SwitchState(CatBaseState catState)
    {
        CurrentState = catState;
        catState.EnterState(this, cooldown, attackSpeed, ballOfFur, ballOfWool, player);
    }

    public CatBaseState RandomState (int select)
    {
        if(select > 7)
        {
            return new CatThrowBallOfWoll();
        }
        if (select > 5)
        {
            return new CatThrowBallOfFur();
        }
        return new CatAttack();
    }
    public void SetAtacando (bool ataque)
    {
        atacando = ataque;
    }
    public Animator GetAnimator()
    {
        return animator;
    }
}
