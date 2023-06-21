using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    public NavMeshAgent agent;

    public float radius;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    float t = 0;
    [SerializeField] float moveCD = 3;
    private void Update()
    {
        t += Time.deltaTime;
        if (t > moveCD)
        {
            agent.SetDestination(GetPoint.Instance.GetRandomPoint(transform, radius));
            t = 0;
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif
}