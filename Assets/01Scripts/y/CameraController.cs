using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera camera;

    [SerializeField] float t = 1;

    void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetViewAngle(int angle = 60)
    {
        camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, angle, t * Time.deltaTime);
    }

    public void ShakeCamera()
    {
       // camera.
    }
}
