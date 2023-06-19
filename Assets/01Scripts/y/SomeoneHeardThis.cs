using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeoneHeardThis : MonoBehaviour
{
    Gun gun;

    void Start()
    {
        gun = GetComponent<Gun>();
    }

    public void SomeoneHeardFireSound()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, gun.weaponPrefab.WeaponSO.FireSoundAmount, transform.forward, 0);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.CompareTag("Enemy"))
            {
                hit[i].transform.GetComponent<EnemyFSM>().SetCuriousPoint(transform.position);
                print("setcurioutpoinnt»£√‚");
            }
        }

    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, gun.weaponPrefab.WeaponSO.FireSoundAmount);
    }
}
