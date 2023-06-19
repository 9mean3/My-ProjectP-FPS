using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Curious,
        FindYou,
        Attack,
        Damaged,
        Die
    }

    public EnemyState cEnemyState;

    public UnityEvent OnDie;

    [Space]

    public float defMoveSpeed;
    public float curiousMoveSpeed;
    float curMoveSpeed;
    [SerializeField] int damage;
    [SerializeField] int maxHP;
    public int curHP;

    public float findDistance;
    public float attackDistance;

    public float attackDelay;
    float curTime;

    Transform player;
    FindingPlayerAI ai;
    CharacterController cc;
    NavMeshAgent navAgent;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        cEnemyState = EnemyState.Idle;
        cc = GetComponent<CharacterController>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (cEnemyState == EnemyState.Die) return;
        navAgent.speed = curMoveSpeed;
        if (IsTargetInSight())
        {
            print("any->Findyou");
            cEnemyState = EnemyState.FindYou;
        }
        //DetectPlayer();
        switch (cEnemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Curious:
                Curious();
                break;
            case EnemyState.FindYou:
                FindYou();
                break;

            case EnemyState.Damaged:
                break;
            case EnemyState.Die:
                break;
            default:
                break;
        }
    }

    private void FindYou()
    {
        
    }

/*    public float detectionAngle = 30f;  // 감지할 수 있는 시야 각도
    public float detectionRange = 10f;  // 감지할 수 있는 최대 거리


    private void DetectPlayer()
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
            }
        }
    }*/

    [SerializeField] float SightAngle = 60f; //시야각 범위

    bool IsTargetInSight()
    {

        //타겟의 방향 
        Vector3 targetDir = (player.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, targetDir);

        //내적을 이용한 각 계산하기
        // thetha = cos^-1( a dot b / |a||b|)
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        //Debug.Log("타겟과 AI의 각도 : " + theta);
        if (theta <= SightAngle) return true;
        else return false;

        return false;

    }

    void Idle()
    {
        /*if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            cEnemyState = EnemyState.Move;
            Debug.Log("Idle -> Move");
            navAgent.SetDestination(transform.position);
        }*/
        /*if (ai.)
        {
            cEnemyState = EnemyState.Curious;
            Debug.Log("Idle -> Move");
            navAgent.SetDestination(transform.position);
        }*/
    }

    Vector3 curiousPoint;
    void Curious()
    {
        curMoveSpeed = curiousMoveSpeed;
        navAgent.SetDestination(curiousPoint);

        if (Vector3.Distance(transform.position, curiousPoint) < 1)
        {
            WaitForIt(3, () =>
            {
                cEnemyState = EnemyState.Idle;
            });
        }
    }
    public void SetCuriousPoint(Vector3 point)
    {
        WaitForIt(3, () =>
        {
            curiousPoint = point;
            cEnemyState = EnemyState.Curious;
            print("Curious");
        });
    }

    IEnumerator WaitForIt(float waitingTime, Action action)
    {
        yield return new WaitForSeconds(waitingTime);
        action.Invoke();
    }
        
    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            curTime += Time.deltaTime;
            if (curTime >= attackDelay)
            {
                print("attack");
                curTime = 0;
            }
        }
        else
        {
            cEnemyState = EnemyState.FindYou;
            Debug.Log("Move -> Attack");
            curTime = 0;
        }
    }

    public void hitEnemy(int damage)
    {
        curHP -= damage;

        if (curHP > 0)
        {
            cEnemyState = EnemyState.Damaged;
            print("Any->Damaged");
            Damaged();
        }
        else
        {
            cEnemyState = EnemyState.Die;
            print("Any->Die");
            Die();
        }
    }

    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }


    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);

        cEnemyState = EnemyState.FindYou;
        print("Damaged->FindYou");
    }

    void Die()
    {
        OnDie.Invoke();
    }
}
