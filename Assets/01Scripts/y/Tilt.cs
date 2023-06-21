using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum TiltState
{
    Idle,
    Left,
    Right,
}

public class Tilt : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float duration;
    [SerializeField] float tiltAngleAmount;
    [SerializeField] float tiltPositionAmount;

    float curAngle;

    TiltState curTiltState;

    PlayerMovement player;

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (player.isRunning) curTiltState = TiltState.Idle;

        TiltInput();

            target.GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch = curAngle;
        if(curTiltState == TiltState.Idle)
        {
            curAngle = Mathf.Lerp(curAngle, 0, duration);
        }
        if(curTiltState == TiltState.Left)
        {
            curAngle = Mathf.Lerp(curAngle, tiltAngleAmount, duration);

        }
        if (curTiltState == TiltState.Right)
        {
            curAngle = Mathf.Lerp(curAngle, -tiltAngleAmount, duration);

        }
    }

    void TiltInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(curTiltState == TiltState.Idle || curTiltState == TiltState.Right)
                curTiltState = TiltState.Left;
            else
                curTiltState = TiltState.Idle;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (curTiltState == TiltState.Idle || curTiltState == TiltState.Left)
                curTiltState = TiltState.Right;
            else
                curTiltState = TiltState.Idle;
        }
    }
}
