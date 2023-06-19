using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyAI enemyAI;  // 적 AI 스크립트 참조

    public EnemyState(EnemyAI enemy)
    {
        enemyAI = enemy;
    }

    public abstract void Enter();  // 상태 진입 시 실행되는 메서드
    public abstract void Update();  // 상태 업데이트 시 실행되는 메서드
    public abstract void Exit();  // 상태 종료 시 실행되는 메서드
}

public class IdleState : EnemyState
{
    public IdleState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        // idle 상태에 진입했을 때 수행할 초기화 작업
        Debug.Log("idle 상태에 진입!");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        // idle 상태를 종료할 때 수행할 작업
        Debug.Log("idle 상태 종료!");
    }
}

public class ChaseState : EnemyState
{
    public ChaseState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        // 추적 상태에 진입했을 때 수행할 초기화 작업
        Debug.Log("추적 상태에 진입!");
    }

    public override void Update()
    {
        // 추적 상태의 업데이트 로직
        // 플레이어를 추적하고 이동하는 등의 동작 구현
    }

    public override void Exit()
    {
        // 추적 상태를 종료할 때 수행할 작업
        Debug.Log("추적 상태 종료!");
    }
}