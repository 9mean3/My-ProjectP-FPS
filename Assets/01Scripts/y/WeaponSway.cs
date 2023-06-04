using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] float intensity;
    [SerializeField] float smooth;

    [SerializeField] float maxAmount;

    Vector3 originPosition;
    Quaternion originRotation;

    private void Start()
    {
        originRotation = transform.localRotation;
    }

    void Update()
    {
        PositionSway();
    }

    private void PositionSway()
    {
        /*        float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                Quaternion xAbj = Quaternion.AngleAxis(intensity * mouseX, Vector3.up);
                Quaternion yAbj = Quaternion.AngleAxis(-intensity * mouseY, Vector3.forward);
                Quaternion targetRotation = originRotation * xAbj * yAbj;

                transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);*/

        float mouseX = Mathf.Clamp(Input.GetAxis("Mouse X") * intensity, -maxAmount, maxAmount);
        float mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y") * intensity, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(mouseX, mouseY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + originPosition, smooth * Time.deltaTime);
    }
}
