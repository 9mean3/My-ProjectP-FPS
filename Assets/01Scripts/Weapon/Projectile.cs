using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject concHitEff;
    [SerializeField] GameObject hitEff;

    bool isPlayer;
    int damage;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    public void SetProjectile(bool b, int damage)
    {
        isPlayer = b;
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPlayer)
            if (collision.transform.CompareTag("Enemy"))
            {
                Instantiate(hitEff, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
                collision.transform.GetComponent<EnemyFSM>().hitEnemy(damage);
            }
            else
            {
                Instantiate(concHitEff, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
            }
        else if (!isPlayer)
            if (collision.transform.CompareTag("Player"))
            {
                GameObject p = Instantiate(hitEff, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
                collision.transform.GetComponent<PlayerHealth>().GetDamage(damage);
                collision.transform.GetComponent<PlayerHealth>().Damaged.Invoke();
            }
            else
            {
                Instantiate(concHitEff, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
            }
        Destroy(gameObject);
    }

}