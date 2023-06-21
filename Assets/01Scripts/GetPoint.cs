using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GetPoint : MonoBehaviour
{

    public static GetPoint Instance;

    public float Range;

    private void Awake()
    {
        Instance = this;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;

        return false;
    }

    public Vector3 GetRandomPoint(Transform point = null, float radius = 0)
    {
        Vector3 randomPos = Random.insideUnitSphere * radius + point.position;
        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas);

        return hit.position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

#endif

}