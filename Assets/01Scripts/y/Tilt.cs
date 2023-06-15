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

    private void Start()
    {
        
    }

    private void Update()
    {
        TiltInput();

        if(curTiltState == TiltState.Idle)
        {
            curAngle = Mathf.Lerp(curAngle, 0, duration);
            target.localPosition = Vector3.Lerp(target.localPosition, Vector3.zero, duration);
            target.localEulerAngles = Vector3.Lerp(target.localEulerAngles, Vector3.zero, duration);
        }
        if(curTiltState == TiltState.Left)
        {
            curAngle = Mathf.Lerp(curAngle, tiltAngleAmount, duration);
            target.localPosition = Vector3.Lerp(target.localPosition, new Vector3(-tiltPositionAmount, 0), duration);
            target.localEulerAngles = Vector3.Lerp(target.localEulerAngles, new Vector3(0, 0, curAngle), duration);
        }
        if (curTiltState == TiltState.Right)
        {
            curAngle = Mathf.Lerp(curAngle, tiltAngleAmount, duration);
            target.localPosition = Vector3.Lerp(target.localPosition, new Vector3(tiltPositionAmount, 0), duration);
            target.localEulerAngles = Vector3.Lerp(target.localEulerAngles, new Vector3(0, 0, -curAngle), duration);
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
