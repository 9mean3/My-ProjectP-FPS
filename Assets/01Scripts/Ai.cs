using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    public NavMeshAgent agent;

    public float radius;

    Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }
    float t = 0;
    [SerializeField] float moveCD = 3;
    bool attacking = false;
    private void Update()
    {
/*        t += Time.deltaTime;
        if (t > moveCD && attacking)
        {
            agent.SetDestination(GetPoint.Instance.GetRandomPoint(player, radius));
            t = 0;
        }
        else if(t > moveCD && !attacking)
        {
            agent.SetDestination(GetPoint.Instance.GetRandomPoint(player, radius));
            t = 0;
        }*/
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif
}