using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatThrowBallOfFur : CatBaseState
{
    private float time;
    private float count = 0;
    private Vector3 player;
    private GameObject ball;

    public void EnterState(CatStateMachine stateMachine, float cooldown, float attackSpeed, GameObject ballOfFur, GameObject ballOfWool)
    {
        time = cooldown;
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        ball = MonoBehaviour.Instantiate(ballOfFur, stateMachine.transform.position, Quaternion.identity);
        ball.GetComponent<BallOfFur>().GetPlayer(player);
    }
    public void UpdateState(CatStateMachine stateMachine)
    {
        count += Time.deltaTime;
        if (count > time)
        {
            stateMachine.SwitchState(stateMachine.RandomState(stateMachine.Randomize()));
        }
    }
}
