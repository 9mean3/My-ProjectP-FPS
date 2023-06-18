using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    PlayerMovement player;
    Gun gun;

    public UnityEvent ReloadingEnd;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        gun = GetComponent<Gun>();
        animator = gun.weaponPrefab.transform.GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isRunning", player.isRunning);

        animator.SetBool("isReloading", gun.isReloading);
    }
}
