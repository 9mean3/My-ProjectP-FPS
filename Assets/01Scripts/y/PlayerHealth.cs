using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent Damaged;
    public UnityEvent OnDieEvent;

    [SerializeField] int maxHP;
    public int curHP;

    [SerializeField] Image bloodImage;

    bool isDie;

    private void Start()
    {
        curHP = maxHP;
    }

    void Update()
    {

    }

    public void BloodSplatter()
    {
        StartCoroutine(BloodSplatterC());
    }

    IEnumerator BloodSplatterC()
    {
        Color blc = bloodImage.color;
        blc.a = 1;
        blc.a = Mathf.Lerp(blc.a, 0, 10 * Time.deltaTime);
        yield return null;
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
