using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNinjaAI : GenericEnemyAI
{
    NinjaStats _ninjaStats;
    private void Awake()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        _ninjaStats = GetComponent<NinjaStats>();
        enemyCollider2D = GetComponent<Collider2D>();
        awarenessRangeScript = awarenessArea.GetComponent<AwarenessRange>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        canEnemyMove = true;
    }


    private void Update()
    {
        if (canEnemyMove == false)
        {
            enemyRigidbody2D.velocity = new Vector2(0f, enemyRigidbody2D.velocity.y);
        }
        StateController(_ninjaStats._awarenessRange, _ninjaStats.attackRange);
        targetDistance = Vector3.Distance(target.transform.position, this.transform.position);
        switch (state)
        {
            case State.roaming:

                if (canEnemyMove)
                {
                    animator.SetBool("isMoving", true);
                    MoveNPC(enemyRigidbody2D, _ninjaStats.movementSpeed);

                }

                TurnOnObstacle(enemyCollider2D, groundLayerMask);

                break;

            case State.chasingTarget:

                if (canEnemyMove)
                {
                    animator.SetBool("isMoving", true);
                    ChaseTarget(target, _ninjaStats.movementSpeed);

                }


                FaceTarget(target);

                break;
        }
    }

}
