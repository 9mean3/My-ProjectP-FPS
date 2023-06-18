using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die
    }

    EnemyState cEnemyState;

    public UnityEvent OnDie;

    Transform player;

    public float moveSpeed;

    [SerializeField] int maxHP;
    public int curHP;

    [SerializeField] int damage;

    public float findDistance;
    public float attackDistance;

    public float attackDelay;
    float curTime;

    CharacterController cc;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        cEnemyState = EnemyState.Idle;
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (cEnemyState == EnemyState.Die) return;
        switch (cEnemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Damaged:
                break;
            case EnemyState.Die:
                break;
            default:
                break;
        }
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            cEnemyState = EnemyState.Move;
            Debug.Log("Idle -> Move");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            cEnemyState = EnemyState.Attack;
            Debug.Log("Move -> Attack");
            curTime = attackDelay;
        }
        if(Vector3.Distance(transform.position, player.position) > findDistance)
        {
            cEnemyState = EnemyState.Idle;
            print("Move->Idle");
        }
    }

    void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            curTime += Time.deltaTime;
            if(curTime >= attackDelay)
            {
                print("attack");
                curTime = 0;
            }
        }
        else
        {
            cEnemyState = EnemyState.Move;
            Debug.Log("Move -> Attack");
            curTime = 0;
        }
    }

    public void hitEnemy(int damage)
    {
        curHP -= damage;

        if(curHP > 0)
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

        cEnemyState = EnemyState.Move;
        print("Damaged->Move");
    }

    void Die()
    {
        OnDie.Invoke();
    }
}