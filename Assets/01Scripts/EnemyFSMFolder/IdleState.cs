using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(EnemyAI enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("idleEnter");
    }

    public override void Exit()
    {
        Debug.Log("idleExxit");
    }

    public override void Update()
    {
        Debug.Log("idleUpdate");
    }


}
