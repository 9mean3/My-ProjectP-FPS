using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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

    public UnityEvent OnAttack;

    public UnityEvent OnDie;

    [Space]

    public float defMoveSpeed;
    public float curiousMoveSpeed;
    float curMoveSpeed;
    bool isFire = false;
    [SerializeField] int damage;
    [SerializeField] int maxHP;
    public int curHP;

    /*    public float findDistance;
        public float attackDistance;*/

    [SerializeField] GameObject gunObject;
    Transform firePos;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float recoilX;
    [SerializeField] float recoilY;

    public float attackDelay;
    float curTime;

    Transform player;
    FindingPlayerAI ai;
    CharacterController cc;
    //NavMeshAgent navAgent;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        firePos = gunObject.transform.Find("FirePos");
        cEnemyState = EnemyState.Idle;
        cc = GetComponent<CharacterController>();
        //navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (cEnemyState == EnemyState.Die) return;
        //navAgent.speed = curMoveSpeed;
        if (IsTargetInSight())
        {
            //print("any->Findyou");
            cEnemyState = EnemyState.FindYou;
        }
        //DetectPlayer();
        switch (cEnemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Curious:
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

    float t = 0;
    float r = 0;
    private void FindYou()
    {
        Vector3 dir = player.transform.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * GetComponent<Ai>().agent.acceleration);

        t += Time.deltaTime;
        //print(t);
        //StartCoroutine(Move());
        if (t > r)
        {
            //navAgent.SetDestination(player.position);
            //StartCoroutine(Move());
            t = 0;
            r = UnityEngine.Random.Range(3, 10);
            print("들옴");
        }

        if (!isFire)
            StartCoroutine(Attack());
    }
    float ct = 3;

    IEnumerator Attack()
    {
        isFire = true;
        //gunObject.transform.localEulerAngles = new Vector3(Random.Range(-recoilX, recoilX), Random.Range(-recoilY, recoilY));
        print("Attack.");
        RaycastHit hit;
        if (Physics.Raycast(firePos.position, firePos.forward, out hit))
        {
            GameObject p = Instantiate(projectilePrefab, firePos.position, firePos.rotation);
            p.GetComponent<Projectile>().SetProjectile(false, damage);
            p.GetComponent<Rigidbody>().velocity = firePos.forward * 400;
        }
        yield return new WaitForSeconds(attackDelay);
        isFire = false;
    }


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

    }

    void Idle()
    {
        
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
