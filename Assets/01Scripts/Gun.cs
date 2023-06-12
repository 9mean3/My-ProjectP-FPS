using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public UnityEvent Fire;

    public bool isZoom;
    public GameObject gun;
    [SerializeField] float returnTime;
    [Space]
    [SerializeField] Vector3 zoomPos;
    [SerializeField] float zoomTime;
    [Space]
    [SerializeField] GameObject effPrefab;

    Vector3 gunOriginPos;

    bool isShooting = false;
    void Start()
    {
        gunOriginPos = gun.transform.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(FireCrt());
        }

        if (Input.GetMouseButton(1))
        {
            isZoom = true;
        }
        else
        {
            isZoom = false;
        }

        Zooming();
    }

    void Zooming()
    {
        if (isZoom)
        {
            gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition, zoomPos, zoomTime);
        }
        else
        {
            gun.transform.localPosition = Vector3.Slerp(gun.transform.localPosition, gunOriginPos, zoomTime);
        }
    }

    public void raycasttst()
    {
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.Find("ak47/FirePos").position, gun.transform.Find("ak47/FirePos").forward, out hit))
        {
            GameObject prefab = Instantiate(effPrefab, hit.point, Quaternion.identity);
            Destroy(prefab, 0.08f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(gun.transform.Find("ak47/FirePos").position, gun.transform.Find("ak47/FirePos").forward);
    }

    IEnumerator FireCrt()
    {
        Fire.Invoke();
        yield return new WaitForSeconds(returnTime);
        isShooting = false;
    }
}
