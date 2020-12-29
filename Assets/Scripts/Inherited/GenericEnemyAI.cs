using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyAI : MonoBehaviour
{
    public enum State
    {
        roaming,
        chasingTarget,
        attackingTarget,
    }
    public State state;

    public Rigidbody2D enemyRigidbody2D;

    public GameObject target;
    public AudioSource audioSource;
    public Animator animator;
    public LayerMask groundLayerMask;
    public bool canEnemyMove;
    public bool isAttacking = false;

    public Collider2D enemyCollider2D;
    public float targetDistance;

    [SerializeField] Transform _edgeChecker;



    public GameObject awarenessArea;
    public AwarenessRange awarenessRangeScript;










    public void StateController(float awarenessRange, float attackRange)
    {
        if (awarenessRangeScript.playerInRange == true)
        {
            if (targetDistance > awarenessRange && !isAttacking)
            {
                state = State.roaming;
            }
            else if (targetDistance <= awarenessRange && targetDistance >= attackRange && !isAttacking)
            {
                state = State.chasingTarget;
            }
            else if (targetDistance <= attackRange)
            {
                state = State.attackingTarget;
            }
        }
        else
        {
            state = State.roaming;
        }



    }

    public void MoveNPC(Rigidbody2D _rigidbody2D, float movementSpeed)
    {
        if (this.transform.rotation.y == 0)
        {
            _rigidbody2D.velocity = new Vector2(movementSpeed, _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(-movementSpeed, _rigidbody2D.velocity.y);
        }

    }



    public void TurnOnObstacle(Collider2D collider2D, LayerMask layerMask)
    {

        bool CheckRightWall()
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.right, collider2D.bounds.extents.x + 1f, layerMask);
            Debug.DrawRay(transform.position, Vector3.right * (collider2D.bounds.extents.x + 0.5f), Color.green);
            return hit.collider != null;
        }

        bool CheckLeftWall()
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.left, 0.5f, layerMask);
            Debug.DrawRay(transform.position, Vector3.left * 0.5f, Color.green);
            return hit.collider != null;
        }

        bool CheckEdge()
        {
            RaycastHit2D hit = Physics2D.Raycast(_edgeChecker.position, Vector3.down, 0.3f, groundLayerMask);
            Debug.DrawRay(_edgeChecker.position, Vector3.down * 0.3f, Color.red);
            return hit.collider == null;


        }

        if (CheckEdge())
        {
            if (this.transform.rotation.y == 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }



        if (CheckRightWall())
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (CheckLeftWall())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public void TurnAndGoBack()
    {
        /*if (transform.position.x >= _roamingMaxXPos)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (transform.position.x <= _roamingMinXpos)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }*/
    }

    public void FaceTarget(GameObject target)
    {
        if (this.transform.position.x < target.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (this.transform.position.x > target.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void ChaseTarget(GameObject target, float movementSpeed)
    {
        if (canEnemyMove)
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            if (targetDistance > 1)
            {
                if (this.transform.rotation.y == 0)
                {
                    enemyRigidbody2D.velocity = new Vector2(movementSpeed, enemyRigidbody2D.velocity.y);
                }
                else
                {
                    enemyRigidbody2D.velocity = new Vector2(-movementSpeed, enemyRigidbody2D.velocity.y);
                }
            }


        }
    }

    public void DisableEnemyMovement()
    {
        canEnemyMove = false;
    }

    public void EnableEnemyMovement()
    {
        canEnemyMove = true;
    }



}
