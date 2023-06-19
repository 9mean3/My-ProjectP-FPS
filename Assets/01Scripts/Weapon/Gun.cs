using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public WeaponPrefab weaponPrefab;

    public UnityEvent Fire;
    public UnityEvent aim;
    public UnityEvent aimRelease;

    public GameObject gunHolder;
    [SerializeField] GameObject aimObj;

    public bool isZoom;
    [SerializeField] Vector3 zoomPos;
    [SerializeField] float zoomTime;
    [Space]
    [SerializeField] GameObject hitEffPrefab;
    [SerializeField] GameObject bloodEffPrefab;

    public int curBulletCountInMagazine;
    public int curTotalBulletCount;
    public bool isReloading = false;

    public LayerMask whoisEnemy;

    Transform firePos;
    Light muzzleLight;

    Vector3 gunOriginPos;

    PlayerMovement player;

    public bool isShooting = false;
    void Start()
    {
        curTotalBulletCount = weaponPrefab.WeaponSO.StartTotalBulletCount;
        curBulletCountInMagazine = weaponPrefab.WeaponSO.TotalBulletCountInMagazine;
        firePos = gunHolder.transform.Find(weaponPrefab.WeaponSO.Name.ToString() + "/FirePos");
        muzzleLight = firePos.transform.GetComponent<Light>();
        muzzleLight.enabled = false;
        gunOriginPos = gunHolder.transform.localPosition;
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isReloading) return; /////////////////////////////////////////////////////////////////////////////////////////////////////////wait for it

        if (weaponPrefab.WeaponSO.isAuto && Input.GetMouseButton(0) && !isShooting && curBulletCountInMagazine > 0)
        {
            isShooting = true;
            StartCoroutine(FireCrt());
        }
        if (!weaponPrefab.WeaponSO.isAuto && Input.GetMouseButtonDown(0) && !isShooting && curBulletCountInMagazine > 0)
        {
            isShooting = true;
            StartCoroutine(FireCrt());
        }

        if (Input.GetKeyDown(KeyCode.R) && curTotalBulletCount > 0)
        {
            Reloading();
        }

        Zooming();
    }

    void Reloading()
    {
        isZoom = false;
        isReloading = true;
    }

    void Zooming()
    {
        /*if (Input.GetMouseButton(1))
        {
            isZoom = true;
        }
        else
        {
            isZoom = false;
        }*/

        //if (player.isRunning) isZoom = false;
        if (Input.GetMouseButton(1) && !player.isRunning)
        {
            isZoom = true;
            player.isRunning = false;
        }
        else
        {
            isZoom = false;
        }

        if (isZoom && !player.isRunning)
        {
            aimObj.transform.localPosition = Vector3.Lerp(aimObj.transform.localPosition, zoomPos, Time.deltaTime * zoomTime);
            aim.Invoke();
        }
        else
        {
            aimObj.transform.localPosition = Vector3.Lerp(aimObj.transform.localPosition, gunOriginPos, Time.deltaTime * zoomTime);
            aimRelease.Invoke();
        }
    }

    public void raycasttst()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePos.position, firePos.forward, out hit))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyFSM enemyFSM = hit.transform.GetComponent<EnemyFSM>();
                enemyFSM.hitEnemy(weaponPrefab.WeaponSO.Damage);
                GameObject prefab = Instantiate(bloodEffPrefab);
                prefab.transform.position = hit.point;
                prefab.transform.forward = hit.normal;
                Destroy(prefab, 0.5f);
            }
            else
            {
                GameObject prefab = Instantiate(hitEffPrefab);
                prefab.transform.position = hit.point;
                prefab.transform.forward = hit.normal;
                Destroy(prefab, 0.5f);
            }
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawRay(firePos.position, firePos.forward);
    }

    public void ReloadEnd()
    {
        isReloading = false;
        int wdp = weaponPrefab.WeaponSO.TotalBulletCountInMagazine - curBulletCountInMagazine;
        //print(wdp);
        if (curTotalBulletCount < wdp) wdp = curTotalBulletCount + curBulletCountInMagazine;
        //print(wdp);
        curTotalBulletCount -= wdp;
        curBulletCountInMagazine += wdp;
    }

    IEnumerator FireCrt()
    {
        Fire.Invoke();
        curBulletCountInMagazine--;
        yield return new WaitForSeconds(weaponPrefab.WeaponSO.ReturnTime);
        isShooting = false;
    }
}
