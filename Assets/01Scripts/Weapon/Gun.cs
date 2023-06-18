using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public WeaponPrefab weaponPrefab;
    public WeaponSO weaponSO;

    public UnityEvent Fire;
    public UnityEvent aim;
    public UnityEvent aimRelease;

    public bool isZoom;
    public GameObject gunHolder;
    [SerializeField] GameObject aimObj;

    [SerializeField] Vector3 zoomPos;
    [SerializeField] float zoomTime;
    [Space]
    [SerializeField] GameObject hitEffPrefab;
    [SerializeField] GameObject muzzleFlashEffPrefab;

    ParticleSystem particleSystem;

    Transform firePos;
    Light muzzleLight;

    Vector3 gunOriginPos;

    PlayerMovement player;

    public bool isShooting = false;
    void Start()
    {
        particleSystem = hitEffPrefab.GetComponent<ParticleSystem>();
        firePos = gunHolder.transform.Find(weaponPrefab.WeaponSO.Name.ToString() + "/FirePos");
        muzzleLight = firePos.transform.GetComponent<Light>();
        muzzleLight.enabled = false;
        gunOriginPos = gunHolder.transform.localPosition;
        player = GetComponent<PlayerMovement>();
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
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyFSM enemyFSM = hit.transform.GetComponent<EnemyFSM>();
                enemyFSM.hitEnemy(weaponSO.Damage);
            }
            else
            {
            hitEffPrefab.transform.position = hit.point;
            hitEffPrefab.transform.forward = hit.normal;
            particleSystem.Play();
            }
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawRay(firePos.position, firePos.forward);
    }

    IEnumerator FireCrt()
    {
        Fire.Invoke();

        yield return new WaitForSeconds(weaponPrefab.WeaponSO.ReturnTime);
        isShooting = false;
    }
}
