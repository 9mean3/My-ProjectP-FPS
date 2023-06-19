using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingPlayerAI : MonoBehaviour
{
    public float findwithWalkDistance;
    public float findwithFireDistance;
    public float findwithWatchDistance;
    public void Update()
    {
        print(IsTargetInSight());
    }

    [SerializeField] float SightAngle = 70f; //시야각 범위
    public Transform AttackTargetPlayer;

    bool IsTargetInSight()
    {

        //타겟의 방향 
        Vector3 targetDir = (AttackTargetPlayer.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, targetDir);

        //내적을 이용한 각 계산하기
        // thetha = cos^-1( a dot b / |a||b|)
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        //Debug.Log("타겟과 AI의 각도 : " + theta);
        if (theta <= SightAngle) return true;
        else return false;


        return false;

    }

    private void OnDrawGizmos()
    {
/*        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, findwithWalkDistance);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, findwithFireDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findwithWatchDistance);*/
    }
}
