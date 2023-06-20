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