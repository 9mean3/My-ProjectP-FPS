using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Recoil : MonoBehaviour
{
    Vector3 currentRotation;
    Vector3 targetRotation;

    [SerializeField] GameObject target;
    [SerializeField] float gunRecoil;
    [Space]
    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;
    [Space]
    [SerializeField] float zmrecoilX;
    [SerializeField] float zmrecoilY;
    [SerializeField] float zmrecoilZ;
    [Space]
    [SerializeField] float snappiness;
    [SerializeField] float returnSpeed;

    Gun gun;

    private void Start()
    {
        gun = FindAnyObjectByType<Gun>();
    }

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);

        target.transform.localRotation = Quaternion.Euler(currentRotation * gunRecoil);
    }

    public void RecoilFire()
    {
        if (!gun.isZoom)
            targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        else
            targetRotation += new Vector3(zmrecoilX, Random.Range(-zmrecoilY, zmrecoilY), Random.Range(-zmrecoilZ, zmrecoilZ));

    }
}
