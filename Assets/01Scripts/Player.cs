using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotateSpeed = 5f;

    float mouseX = 0;
    float mouseY = 0;
    private void Start()
    {
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        transform.Translate(dir * movementSpeed * Time.deltaTime);

        mouseX += Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0, mouseX * movementSpeed, 0);
        /*        Vector3 rotY = new Vector3(0, mouseX * rotateSpeed, 0);
                transform.Rotate(rotY);*/

        mouseY = Input.GetAxis("Mouse Y");
        cam.transform.Rotate(new Vector3(-mouseY * movementSpeed, 0, 0));
        /*        Vector3 rotX = new Vector3(-mouseY * rotateSpeed, 0, 0);
                cam.transform.Rotate(rotX);*/
    }
}
