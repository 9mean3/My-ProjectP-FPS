using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : AnimatorController
{
    public void ReloadEnd()
    {
        gun.isReloading = true;
    }
}
