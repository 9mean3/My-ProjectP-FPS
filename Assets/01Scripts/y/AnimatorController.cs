using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    PlayerMovement player;
    Gun gun;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        gun = GetComponent<Gun>();
        animator = gun.weaponPrefab.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isRunning", player.isRunning);
    }
}
