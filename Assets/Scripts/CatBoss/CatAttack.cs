using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CatAttack : CatBaseState
{
    private Vector3 distance;
    private Vector3 ant;
    private float time;
    private float speed;
    private float count;
    private float stuckCount;
    private bool stuck;
    private Vector3 playerPosition;
    public void EnterState(CatStateMachine stateMachine, float cooldown, float attackSpeed, GameObject ballOfFur, GameObject ballOfWool, GameObject player)
    {
        distance = player.transform.position - stateMachine.transform.position;
        distance += stateMachine.transform.position;
        time = cooldown;
        speed = attackSpeed;
        stuck = false;
        ant = stateMachine.transform.position;
        count = 0;
        stuckCount = 0;
        playerPosition = player.transform.position;
        stateMachine.SetAtacando(true);
        stateMachine.GetAnimator().SetBool("Morte", false);
        stateMachine.GetAnimator().SetBool("Parar", false);
        stateMachine.GetAnimator().SetBool("Arranhando", true);
        stateMachine.GetAnimator().SetBool("BolaLa", false);
        stateMachine.GetAnimator().SetBool("BolaPelo", false);
        stateMachine.GetAnimator().SetFloat("AnimMoveX", FindWay(stateMachine.transform.position).x);
        stateMachine.GetAnimator().SetFloat("AnimMoveY", FindWay(stateMachine.transform.position).y);
    }
    public void UpdateState(CatStateMachine stateMachine)
    {
        stateMachine.transform.position = Vector2.MoveTowards(stateMachine.transform.position, distance, speed * Time.deltaTime);
        if ((Vector3.Distance(stateMachine.transform.position, distance) < 0.01f)||(stuck))
        {
            stateMachine.GetAnimator().SetBool("Morte", false);
            stateMachine.GetAnimator().SetBool("Parar", true);
            stateMachine.GetAnimator().SetBool("Arranhando", false);
            stateMachine.GetAnimator().SetBool("BolaLa", false);
            stateMachine.GetAnimator().SetBool("BolaPelo", false);
            count += Time.deltaTime;
            stateMachine.SetAtacando(false);
            if (count > time)
            {
                stateMachine.SwitchState(stateMachine.RandomState(stateMachine.Randomize()));
            }
        }
        else
        {
            stuckCount += Time.deltaTime;
            if (stuckCount > time)
            {
                if (Vector3.Distance(stateMachine.transform.position, ant) < 0.2f)
                {
                    count = stuckCount;
                    stuck = true;
                }
                else
                {
                    ant = stateMachine.transform.position;
                    stuckCount = 0f;
                }
            }
        }
    }
    public Vector3 FindWay(Vector3 catLocation)
    {
        Vector3 distancia = playerPosition - catLocation;
        if (Mathf.Abs(distancia.x) < Mathf.Abs(distancia.y))
        {
            if (distancia.y > 0)
            {
                return new Vector3(0, 1, 0);
            }
            else
            {
                return new Vector3(0, -1, 0);
            }
        }
        else
        {
            if (distancia.x > 0)
            {
                return new Vector3(1, 0, 0);
            }
            else
            {
                return new Vector3(-1, 0, 0);
            }
        }
    }

}
