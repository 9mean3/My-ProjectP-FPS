using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public float detectionAngle = 30f;
    public float detectionRange = 10f;

    private EnemyState currentState;

    private void Start()
    {
        currentState = new IdleState(this);  // 초기 상태를 Idle로 설정
        currentState.Enter();
    }

    private void Update()
    {
        // 플레이어를 감지하면 상태 전환
        if (DetectPlayer())
        {
            ChangeState(new ChaseState(this));
        }

        // 현재 상태 업데이트
        currentState.Update();
    }

    private bool DetectPlayer()
    {
        // 플레이어를 감지하는 로직 구현
        // 플레이어를 발견하면 true 반환, 그렇지 않으면 false 반환
        return true;
    }

    public void ChangeState(EnemyState newState)
    {
        // 현재 상태 종료
        currentState.Exit();

        // 상태 전환
        currentState = newState;
        currentState.Enter();
    }
}