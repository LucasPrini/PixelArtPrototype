using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WizardAI : MonoBehaviour
{
    public enum State
    {
        roaming,
        chasingTarget,
        attackingTarget,
    }
    public State state;

    WizardStats _wizardStats;

    public bool canMove;
    Rigidbody2D _rigidbody2D;
    CapsuleCollider2D _capsuleCollider2D;
    Animator _animator;

    [SerializeField] GameObject _target;
    [SerializeField] float _targetDistance;

    [SerializeField] GameObject _lightObject;

    [SerializeField] Transform _greenProjSpawnPos;
    [SerializeField] GameObject _greenProjPrefab;



    [SerializeField] LayerMask _groundLayerMask;


    AudioSource _audioSource;
    [SerializeField] AudioClip _castSpellClip;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _wizardStats = GetComponent<WizardStats>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;


    }

    // Update is called once per frame
    void Update()
    {


        _targetDistance = Vector3.Distance(_target.transform.position, this.transform.position);
        switch (state)
        {
            case State.roaming:
                if (canMove)
                {
                    MoveNPC();
                }

                AvoidWall();
                _animator.SetBool("isMoving", true);
                break;

            case State.chasingTarget:
                if (canMove)
                {
                    ChaseTarget();
                }


                FacePlayer();
                _animator.SetBool("isMoving", true);
                break;

            case State.attackingTarget:
                FacePlayer();
                _animator.SetBool("isMoving", false);
                AttackPlayer();
                break;
        }


        StateController();
        CheckRightWall();
        CheckLeftWall();


        if (state != State.attackingTarget)
        {
            isCoroutineOn = false;
        }



    }

    private void FixedUpdate()
    {



    }


    void StateController()
    {
        if (_targetDistance > _wizardStats._awarenessRange)
        {
            state = State.roaming;
        }
        else if (_targetDistance <= _wizardStats._awarenessRange && _targetDistance >= _wizardStats.attackRange)
        {
            state = State.chasingTarget;
        }
        else if (_targetDistance <= _wizardStats.attackRange)
        {
            state = State.attackingTarget;
        }
    }





    public void MoveNPC()
    {
        if (this.transform.rotation.y == 0)
        {
            _rigidbody2D.velocity = new Vector2(_wizardStats.movementSpeed, _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(-_wizardStats.movementSpeed, _rigidbody2D.velocity.y);
        }

    }

    void AvoidWall()
    {
        if (CheckRightWall())
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (CheckLeftWall())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }


    bool CheckRightWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.right, _capsuleCollider2D.bounds.extents.x + 1f, _groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.right * (_capsuleCollider2D.bounds.extents.x + 0.5f), Color.green);
        return hit.collider != null;
    }

    bool CheckLeftWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.left, 0.5f, _groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.left * 0.5f, Color.green);
        return hit.collider != null;
    }


    void ChaseTarget()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, _target.transform.position, _wizardStats.movementSpeed * Time.deltaTime);
    }

    void FacePlayer()
    {
        if (this.transform.position.x < _target.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (this.transform.position.x > _target.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    [SerializeField] bool isCoroutineOn = false;
    void AttackPlayer()
    {


        if (state == State.attackingTarget && isCoroutineOn == false)
        {
            StartCoroutine(AttackCooldown());


        }
        IEnumerator AttackCooldown()
        {
            isCoroutineOn = true;
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            _animator.ResetTrigger("Attack");
            yield return new WaitForSeconds(0.4f);
            isCoroutineOn = false;

        }


    }

    void litGreenLight()
    {
        _lightObject.GetComponent<GreenLight>().canLit = true;
    }

    void unLitGreenLight()
    {
        _lightObject.GetComponent<GreenLight>().canLit = false;
    }

    void CastProjectile()
    {
        Instantiate(_greenProjPrefab, _greenProjSpawnPos.position, Quaternion.identity);

    }
    void PlayCastSound()
    {
        _audioSource.PlayOneShot(_castSpellClip);
        Debug.Log("played");
    }

    void DisableMovement()
    {
        canMove = false;
    }
    void EnableMovement()
    {
        canMove = true;
    }


}
