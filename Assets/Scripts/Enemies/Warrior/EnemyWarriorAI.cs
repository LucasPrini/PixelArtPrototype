using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarriorAI : GenericEnemyAI
{

    WarriorStats _warriorStats;


    private void Awake()
    {
        _warriorStats = GetComponent<WarriorStats>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        enemyCollider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        awarenessRangeScript = awarenessArea.GetComponent<AwarenessRange>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canEnemyMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckEdge() && isAttacking)
        {
            enemyRigidbody2D.velocity = Vector3.zero;
        }
        TurnAndGoBack();
        if (canEnemyMove == false)
        {
            enemyRigidbody2D.velocity = new Vector2(0f, enemyRigidbody2D.velocity.y);
        }
        switch (state)
        {
            case State.roaming:
                canEnemyMove = true;
                _chargeOnPlayer = false;
                if (canEnemyMove)
                {
                    animator.SetBool("isMoving", true);

                }

                break;

            case State.chasingTarget:
                canEnemyMove = true;
                _chargeOnPlayer = false;
                if (CheckEdge())
                {
                    canEnemyMove = false;
                }
                if (canEnemyMove)
                {
                    animator.SetBool("isMoving", true);

                }




                FaceTarget(target);

                break;

            case State.attackingTarget:



                animator.SetBool("isMoving", false);

                AttackPlayer();


                if (!isAttacking)
                {
                    FaceTarget(target);
                }
                break;
        }
    }

    private void FixedUpdate()
    {

        StateController(_warriorStats._awarenessRange, _warriorStats.attackRange);
        targetDistance = Vector3.Distance(target.transform.position, this.transform.position);
        switch (state)
        {
            case State.roaming:
                canEnemyMove = true;
                _chargeOnPlayer = false;
                TurnOnObstacle(enemyCollider2D, groundLayerMask);

                if (canEnemyMove)
                {
                    animator.SetBool("isMoving", true);
                    MoveNPC(enemyRigidbody2D, _warriorStats.movementSpeed);

                }



                break;

            case State.chasingTarget:
                canEnemyMove = true;
                _chargeOnPlayer = false;


                if (canEnemyMove)
                {
                    animator.SetBool("isMoving", true);
                    ChaseTarget(target, _warriorStats.movementSpeed);

                }


                FaceTarget(target);

                break;

            case State.attackingTarget:


                animator.SetBool("isMoving", false);
                AttackPlayer();

                break;
        }


        /*if (_chargeOnPlayer)
        {
            ChargeOnPlayer();
        }*/
    }

    bool isAttackRountineOn = false;
    [SerializeField] bool _chargeOnPlayer = false;
    void AttackPlayer()
    {
        if (isAttackRountineOn == false)
        {
            StartCoroutine(AttackRountine());
        }
    }

    IEnumerator AttackRountine()
    {
        isAttackRountineOn = true;

        enemyRigidbody2D.velocity = Vector3.zero;
        canEnemyMove = false;
        FaceTarget(target);
        yield return new WaitForSeconds(0.5f);
        if (targetDistance < _warriorStats.attackRange)
        {
            isAttacking = true;
            //canEnemyMove = true;
            Debug.Log("chargeCalled");

            animator.SetTrigger("Charge");

            //ChargeOnPlayer();
            yield return new WaitForSeconds(1f);
            //canEnemyMove = false;
            enemyRigidbody2D.velocity = Vector3.zero;

            yield return new WaitForSeconds(1f);
        }

        //canEnemyMove = true;
        isAttacking = false;
        isAttackRountineOn = false;
    }

    void ChargeOnPlayer()
    {

        if (this.transform.rotation.y == 0)
        {
            enemyRigidbody2D.AddForce(new Vector2(_warriorStats.chargeSpeed, 0f), ForceMode2D.Impulse);
        }
        else
        {
            enemyRigidbody2D.AddForce(new Vector2(-_warriorStats.chargeSpeed, 0f), ForceMode2D.Impulse);
        }





    }
}
