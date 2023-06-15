using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] float tiltAngleAmount;
    [SerializeField] float tiltPositionAmount;

    TiltState curTiltState;

    private void Start()
    {
        
    }

    private void Update()
    {
        TiltInput();

        if(curTiltState == TiltState.Idle)
        {
            target.localPosition = Vector3.zero;
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
