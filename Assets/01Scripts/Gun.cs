using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public UnityEvent Fire;

    [SerializeField] float returnTime;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject effPrefab;

    bool isShooting = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(FireCrt());
        }
    }

    public void raycasttst()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, gun.transform.forward, out hit))
        {
            GameObject prefab = Instantiate(effPrefab, hit.point, Quaternion.identity);
            Destroy(prefab, 0.08f);
        }
    }

    IEnumerator FireCrt()
    {
        Fire.Invoke();
        yield return new WaitForSeconds(returnTime);
        isShooting=false;
    }
}
