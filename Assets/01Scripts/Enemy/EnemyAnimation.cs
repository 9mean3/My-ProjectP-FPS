using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;
    Ai enemyMovement;


    void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = transform.root.GetComponent<Ai>();
    }

    Vector3 _worldDeltaPosition;
    Vector3 _groundDeltaPosition;
    Vector2 _velocity = Vector2.zero;

    void Update()
    {
        _worldDeltaPosition = enemyMovement.agent.nextPosition - enemyMovement.transform.position;
        _groundDeltaPosition.x = Vector3.Dot(enemyMovement.transform.right, _worldDeltaPosition);
        _groundDeltaPosition.y = Vector3.Dot(enemyMovement.transform.forward, _worldDeltaPosition);

        _velocity = (Time.deltaTime > 1e-5f) ? (Vector2)_groundDeltaPosition / Time.deltaTime : _velocity = Vector2.zero;
        bool _shouldMove = _velocity.magnitude > 0.025f && enemyMovement.agent.remainingDistance > enemyMovement.agent.radius;

        //animator.SetBool("isMove", _shouldMove);
        animator.SetFloat("moveX", _velocity.x);
        animator.SetFloat("moveZ", _velocity.y);
        print(_velocity);
    }
}
