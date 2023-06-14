using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public WeaponSO weaponType;

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
        if (weaponType.isAuto && Input.GetMouseButton(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(FireCrt());
        }
        if (!weaponType.isAuto && Input.GetMouseButtonDown(0) && !isShooting)
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
        if (Physics.Raycast(gun.transform.Find(weaponType.Name.ToString() + "/FirePos").position, gun.transform.Find(weaponType.Name.ToString() + "/FirePos").forward, out hit))
        {
            GameObject prefab = Instantiate(effPrefab, hit.point, Quaternion.identity);
            Destroy(prefab, 0.08f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(gun.transform.Find(weaponType.Name.ToString() + "/FirePos").position, gun.transform.Find(weaponType.Name.ToString() + "/FirePos").forward);
    }

    IEnumerator FireCrt()
    {
        Fire.Invoke();
        yield return new WaitForSeconds(weaponType.ReturnTime);
        isShooting = false;
    }
}
