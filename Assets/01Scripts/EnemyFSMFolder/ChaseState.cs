using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyAI enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("ChaseEnter");
    }

    public override void Exit()
    {
        Debug.Log("ChaseExxit");
    }

    public override void Update()
    {
        Debug.Log("ChaseUpdate");
    }
}
