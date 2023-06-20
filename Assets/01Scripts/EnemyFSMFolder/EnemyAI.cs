using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
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
        // 플레이어와의 거리가 감지 범위 이내인지 확인
        Vector3 playerDirection = player.position - transform.position;
        float distanceToPlayer = playerDirection.magnitude;
        if (distanceToPlayer <= detectionRange)
        {
            // 플레이어가 감지 범위 이내에 있을 때, 각도를 계산하여 플레이어를 감지하는지 확인
            float angleToPlayer = Vector3.Angle(transform.forward, playerDirection);
            if (angleToPlayer <= detectionAngle)
            {
                // 플레이어를 감지한 경우
                Debug.Log("플레이어 감지!");

                // 여기에 플레이어를 감지했을 때 수행할 동작을 추가하면 됩니다.
                // 예를 들어, 적이 플레이어를 추적하도록 AI를 추가하거나, 공격하도록 하는 등의 동작을 구현할 수 있습니다.
                return true;
            }
        }
        return false;
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