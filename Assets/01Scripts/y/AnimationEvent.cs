using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    Gun gun;

    private void Start()
    {
        gun = transform.root.GetComponent<Gun>();
    }

    public void ReloadEnd()
    {
        gun.ReloadEnd();
    }
}
