using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatStateMachine : MonoBehaviour
{
    [SerializeField] private int life = 50;
    [SerializeField] private float attackSpeed = 8f;
    [SerializeField] private float cooldown = 4;
    [SerializeField] private GameObject ballOfFur;
    [SerializeField] private GameObject ballOfWool;
    public bool notInRoom = false;
    private bool startFight = true;
    public CatBaseState CurrentState { get; private set; }

    private void Update()
    {
        if (!notInRoom)
        {
            if (startFight)
            {
                SwitchState(new CatAttack());
                startFight = false;
            }
            else
            {
                CurrentState.UpdateState(this);
                //SwitchState(RandomState(Randomize()));
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerLife>().PlayerDamage();
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
        catState.EnterState(this, cooldown, attackSpeed, ballOfFur, ballOfWool);
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
}
