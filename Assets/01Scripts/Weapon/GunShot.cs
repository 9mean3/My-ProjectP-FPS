using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    public float range = 100f;

    public GameObject shotOrigin;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(shotOrigin.transform.position, shotOrigin.transform.forward, out hit, range))
        {
            Debug.DrawRay(shotOrigin.transform.position, shotOrigin.transform.forward);
        }
    }
}
