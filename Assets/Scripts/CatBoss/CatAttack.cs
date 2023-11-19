using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CatAttack : CatBaseState
{
    private Vector3 player;
    private Vector3 distance;
    private float time;
    private float speed;
    private float count = 0;
    public void EnterState(CatStateMachine stateMachine, float cooldown, float attackSpeed, GameObject ballOfFur, GameObject ballOfWool)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        distance = player - stateMachine.transform.position;
        distance += stateMachine.transform.position;
        time = cooldown;
        speed = attackSpeed;
        stateMachine.SetAtacando(true);
    }
    public void UpdateState(CatStateMachine stateMachine)
    {
        stateMachine.transform.position = Vector2.MoveTowards(stateMachine.transform.position, distance, speed * Time.deltaTime);
        if (Vector3.Distance(stateMachine.transform.position, distance) < 0.01f)
        {
            count += Time.deltaTime;
            stateMachine.SetAtacando(false);
            if (count > time)
            {
                stateMachine.SwitchState(stateMachine.RandomState(stateMachine.Randomize()));
            }
        }
    }

}
