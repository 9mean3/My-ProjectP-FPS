using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] Transform target;

    [Header("Position")]
    public float amount = 0.02f;
    public float maxAmonut = 0.06f;
    public float smoothAmount = 6f;

    [Header("Rotation")]
    public float rotationAmount = 4f;
    public float maxRotationAmount = 5f;
    public float smoothRotation = 12f;

    [Space]
    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private float InputX;
    private float InputY;
    void Start()
    {
        initialPosition = target.localPosition;
        initialRotation = target.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSway();

        /*if(Mathf.Abs(transform.parent.parent.GetComponent<PlayerMovement>().Velocity.magnitude) > 0.1f)
        {
            transform.localPosition += new Vector3(Mathf.Sin(Time.deltaTime), Mathf.Sin(Time.deltaTime));
            Debug.Log("move");
        }*/
        
        MoveSway();
        TiltSway();
    }

    private void CalculateSway()
    {
        InputX = -Input.GetAxis("Mouse X");
        InputY = -Input.GetAxis("Mouse Y");
    }

    private void MoveSway()
    {
        float moveX = Mathf.Clamp(InputX * amount, -maxAmonut, maxAmonut);
        float moveY = Mathf.Clamp(InputY * amount, -maxAmonut, maxAmonut);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);

        target.localPosition = Vector3.Lerp(target.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }

    private void TiltSway()
    {
        float tiltY = Mathf.Clamp(InputX * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltX = Mathf.Clamp(InputY * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3(rotationX ? -tiltX : 0f, rotationY ? tiltY : 0f, rotationZ ? tiltY : 0f));

        target.localRotation = Quaternion.Slerp(target.localRotation, finalRotation * initialRotation, Time.deltaTime * smoothRotation);
    }
}
