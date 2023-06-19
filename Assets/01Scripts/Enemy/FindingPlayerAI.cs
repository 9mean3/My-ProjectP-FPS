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

    [SerializeField] float SightAngle = 70f; //�þ߰� ����
    public Transform AttackTargetPlayer;

    bool IsTargetInSight()
    {

        //Ÿ���� ���� 
        Vector3 targetDir = (AttackTargetPlayer.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, targetDir);

        //������ �̿��� �� ����ϱ�
        // thetha = cos^-1( a dot b / |a||b|)
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        //Debug.Log("Ÿ�ٰ� AI�� ���� : " + theta);
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
