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
    [SerializeField] GameObject aimObj;

    [SerializeField] Vector3 zoomPos;
    [SerializeField] float zoomTime;
    [Space]
    [SerializeField] GameObject effPrefab;

    Transform firePos;
    Light muzzleLight;

    Vector3 gunOriginPos;

    bool isShooting = false;
    void Start()
    {
        firePos = gunHolder.transform.Find(weaponPrefab.WeaponSO.Name.ToString() + "/FirePos");
        muzzleLight = firePos.transform.GetComponent<Light>();
        muzzleLight.enabled = false;
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



        Zooming();
    }

    void Zooming()
    {
        if (Input.GetMouseButton(1))
        {
            isZoom = true;
        }
        else
        {
            isZoom = false;
        }

        if (isZoom)
        {
            aimObj.transform.localPosition = Vector3.Lerp(aimObj.transform.localPosition, zoomPos, Time.deltaTime * zoomTime);
        }
        else
        {
            aimObj.transform.localPosition = Vector3.Lerp(aimObj.transform.localPosition, gunOriginPos, Time.deltaTime * zoomTime);
        }
    }

    public void raycasttst()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePos.position, firePos.forward, out hit))
        {
            GameObject prefab = Instantiate(effPrefab, hit.point, Quaternion.identity);
            Destroy(prefab, 0.08f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(firePos.position, firePos.forward);
    }

    IEnumerator FireCrt()
    {
        Fire.Invoke();
        yield return new WaitForSeconds(weaponPrefab.WeaponSO.ReturnTime);
        isShooting = false;
    }
}
