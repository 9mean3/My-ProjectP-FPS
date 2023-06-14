using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public WeaponPrefab weaponPrefab;

    public UnityEvent Fire;

    public bool isZoom;
    public GameObject gunHolder;

    [SerializeField] Vector3 zoomPos;
    [SerializeField] float zoomTime;
    [Space]
    [SerializeField] GameObject effPrefab;

    Vector3 gunOriginPos;

    bool isShooting = false;
    void Start()
    {
        gunOriginPos = gunHolder.transform.localPosition;
    }

    void Update()
    {
        if (weaponPrefab.WeaponSO.isAuto && Input.GetMouseButton(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(FireCrt());
        }
        if (!weaponPrefab.WeaponSO.isAuto && Input.GetMouseButtonDown(0) && !isShooting)
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
            gunHolder.transform.localPosition = Vector3.Lerp(gunHolder.transform.localPosition, zoomPos, zoomTime);
        }
        else
        {
            gunHolder.transform.localPosition = Vector3.Slerp(gunHolder.transform.localPosition, gunOriginPos, zoomTime);
        }
    }

    public void raycasttst()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunHolder.transform.Find(weaponPrefab.WeaponSO.Name.ToString() + "/FirePos").position, gunHolder.transform.Find(weaponPrefab.WeaponSO.Name.ToString() + "/FirePos").forward, out hit))
        {
            GameObject prefab = Instantiate(effPrefab, hit.point, Quaternion.identity);
            Destroy(prefab, 0.08f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(gunHolder.transform.Find(weaponPrefab.WeaponSO.Name.ToString() + "/FirePos").position, gunHolder.transform.Find(weaponPrefab.WeaponSO.Name.ToString() + "/FirePos").forward);
    }

    IEnumerator FireCrt()
    {
        Fire.Invoke();
        yield return new WaitForSeconds(weaponPrefab.WeaponSO.ReturnTime);
        isShooting = false;
    }
}
