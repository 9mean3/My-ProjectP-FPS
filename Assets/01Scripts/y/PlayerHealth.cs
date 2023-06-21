using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent Damaged;
    public UnityEvent OnDieEvent;

    [SerializeField] int maxHP;
    public int curHP;

    bool isDie;

    private void Start()
    {
        curHP = maxHP;
    }

    void Update()
    {

    }

    public void GetDamage(int damage)
    {
        if (0 < curHP)
        {
            curHP -= damage;
        }
        else
        {
            if (!isDie)
                OnDieEvent.Invoke();
            isDie = true;
        }
    }
}
