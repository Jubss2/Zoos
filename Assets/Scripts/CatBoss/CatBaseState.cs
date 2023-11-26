using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CatBaseState
{
    public void EnterState(CatStateMachine stateMachine, float cooldown, float attackSpeed, GameObject ballOfFur, GameObject ballOfWool, GameObject player);
    public void UpdateState(CatStateMachine stateMachine);
}
