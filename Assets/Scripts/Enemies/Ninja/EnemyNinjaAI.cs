using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNinjaAI : GenericEnemyAI
{
    NinjaStats _ninjaStats;
    [SerializeField] GameObject _smokePrefab;
    [SerializeField] AudioClip _smokeSound;

    [SerializeField] LayerMask _layerMask;
    bool swapAttackSide;
    private void Awake()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        _ninjaStats = GetComponent<NinjaStats>();
        enemyCollider2D = GetComponent<Collider2D>();
        awarenessRangeScript = awarenessArea.GetComponent<AwarenessRange>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        canEnemyMove = true;
    }


    private void Update()
    {


        CheckPlayerDirection();
        if ((playerDirection == 1 && this.transform.rotation.y == 180) || (playerDirection == -1 && this.transform.rotation.y == 0))
        {
            swapAttackSide = true;
        }
        else
        {
            swapAttackSide = false;
        }
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

            case State.attackingTarget:
                AttackPlayer();
                break;
        }
    }

    float playerDirection;
    void CheckPlayerDirection()
    {
        if (target.transform.rotation.y == 0)
        {
            playerDirection = 1;
        }
        else
        {
            playerDirection = -1;
        }
    }

    void AttackPlayer()
    {
        FaceTarget(target);
        isAttacking = true;
        canEnemyMove = false;
        if (isRoutineOn == false)
        {
            StartCoroutine(AttackRoutine());
        }


    }

    bool isRoutineOn = false;
    IEnumerator AttackRoutine()
    {
        isRoutineOn = true;
        animator.SetBool("isPrepingAttack", true);

        yield return new WaitForSeconds(1f);
        Instantiate(_smokePrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(_smokeSound);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, transform.right, target.transform.position.x - this.transform.position.x + 1.2f, _layerMask);
        if (hit)
        {
            Debug.Log("HITWALL");
        }
        if (hit && swapAttackSide)
        {
            transform.position = target.transform.position - new Vector3(-(0.7f * playerDirection), -0.3f, 0);
        }
        else
        {
            transform.position = target.transform.position - new Vector3((0.7f * playerDirection), -0.3f, 0);
        }

        FaceTarget(target);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isPrepingAttack", false);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        isRoutineOn = false;
    }

}
