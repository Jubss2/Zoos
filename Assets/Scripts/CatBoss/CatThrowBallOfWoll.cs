using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CatThrowBallOfWoll : CatBaseState
{
    private float time;
    private float count = 0;
    private Vector3 player;
    private GameObject ball;
    public void EnterState(CatStateMachine stateMachine, float cooldown, float attackSpeed, GameObject ballOfFur, GameObject ballOfWool)
    {
        time = cooldown;
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        ball = MonoBehaviour.Instantiate(ballOfWool, stateMachine.transform.position, Quaternion.identity);
        ball.GetComponent<BallOfWool>().SetMovimento(FindWay(stateMachine.transform.position));
    }
    public void UpdateState(CatStateMachine stateMachine)
    {
        count += Time.deltaTime;
        if (count > time)
        {
            stateMachine.SwitchState(stateMachine.RandomState(stateMachine.Randomize()));
        }
    }
    public Vector3 FindWay(Vector3 catLocation)
    {
        Vector3 distancia = player - catLocation;
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
