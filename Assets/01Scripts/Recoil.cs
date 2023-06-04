using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{


    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;

    [SerializeField] float returnTime;

    Quaternion dir;
    Quaternion origin;
    Transform camrot;

    private void Start()
    {
        camrot = GetComponent<Camera>().transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("¹ß");
            
        }
    }
}
