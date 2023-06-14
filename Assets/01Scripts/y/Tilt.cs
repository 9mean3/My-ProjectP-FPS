using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TiltState
{
    Idle = 0,
    Right,
    Left,
}

public class Tilt : MonoBehaviour
{
    [SerializeField] float tiltAmount;
    [SerializeField] float tiltTime;
    [SerializeField] float gunMoveAmount;

    TiltState curTiltState;

    Gun gun;

    private void Start()
    {
        gun = transform.parent.GetComponent<Gun>();
        curTiltState = TiltState.Idle;
    }

    float lt = 0;
    float lr = 0;
    void Update()
    {
        ChangeTiltState(); Debug.Log(curTiltState.ToString());

        if (curTiltState == TiltState.Idle)
        {
            lt = Mathf.Lerp(lt, 0, tiltTime);
            lr = 0;
        }
        if (curTiltState == TiltState.Right)
        {
            lt = Mathf.Lerp(lt, -tiltAmount, tiltTime);
            lr = 1 * 0.33f;
        }
        if (curTiltState == TiltState.Left)
        {
            lt = Mathf.Lerp(lt, tiltAmount, tiltTime);
            lr = -1;
        }
        transform.localEulerAngles = new Vector3(0, 0, lt);
        gun.gun.transform.localPosition = Vector3.Lerp(gun.gun.transform.localPosition, new Vector3(gunMoveAmount * lr, 0, 0), tiltTime);
    }

    void ChangeTiltState()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (curTiltState == TiltState.Idle || curTiltState == TiltState.Left)
                curTiltState = TiltState.Right;
            else if (curTiltState == TiltState.Right)
                curTiltState = TiltState.Idle;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (curTiltState == TiltState.Idle || curTiltState == TiltState.Right)
                curTiltState = TiltState.Left;
            else if (curTiltState == TiltState.Left)
                curTiltState = TiltState.Idle;
        }
    }
}
