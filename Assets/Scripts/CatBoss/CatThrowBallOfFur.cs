using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatThrowBallOfFur : CatBaseState
{
    private float time;
    private float count = 0;
    private Vector3 playerPosition;
    private GameObject ball;
    private GameObject fur;


    public void EnterState(CatStateMachine stateMachine, float cooldown, float attackSpeed, GameObject ballOfFur, GameObject ballOfWool, GameObject player)
    {
        time = cooldown;
        fur = ballOfFur;
        playerPosition = player.transform.position;
    }
    public void UpdateState(CatStateMachine stateMachine)
    {
        count += Time.deltaTime;
        if((time - 0.9f) > count)
        {
            stateMachine.GetAnimator().SetBool("Morte", false);
            stateMachine.GetAnimator().SetBool("Parar", false);
            stateMachine.GetAnimator().SetBool("Arranhando", false);
            stateMachine.GetAnimator().SetBool("BolaLa", false);
            stateMachine.GetAnimator().SetBool("BolaPelo", true);
            stateMachine.GetAnimator().SetFloat("AnimMoveX", FindWay(stateMachine.transform.position).x);
            stateMachine.GetAnimator().SetFloat("AnimMoveY", FindWay(stateMachine.transform.position).y);
        }
        if (count > time)
        {
            stateMachine.GetAnimator().SetBool("Morte", false);
            stateMachine.GetAnimator().SetBool("Parar", true);
            stateMachine.GetAnimator().SetBool("Arranhando", false);
            stateMachine.GetAnimator().SetBool("BolaLa", false);
            stateMachine.GetAnimator().SetBool("BolaPelo", false);
            ball = MonoBehaviour.Instantiate(fur, stateMachine.transform.position, Quaternion.identity);
            ball.GetComponent<BallOfFur>().GetPlayer(playerPosition);
            stateMachine.SwitchState(stateMachine.RandomState(stateMachine.Randomize()));
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
