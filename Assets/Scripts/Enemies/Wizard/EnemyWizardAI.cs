using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizardAI : GenericEnemyAI
{
    [SerializeField] Transform _greenProjSpawnPos;
    public GameObject _greenProjPrefab;
    WizardGreenProj _wizardGreenProj;

    [SerializeField] GameObject _lightObject;

    [SerializeField] AudioClip _castSpellClip;
    WizardStats _wizardStats;


    private void Awake()
    {
        _wizardStats = GetComponent<WizardStats>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        _wizardGreenProj = _greenProjPrefab.GetComponent<WizardGreenProj>();
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
        if (canEnemyMove == false)
        {
            enemyRigidbody2D.velocity = new Vector2(0f, enemyRigidbody2D.velocity.y);
        }
        StateController(_wizardStats._awarenessRange, _wizardStats.attackRange);
        targetDistance = Vector3.Distance(target.transform.position, this.transform.position);
        switch (state)
        {
            case State.roaming:
                if (canEnemyMove)
                {
                    animator.SetBool("isMoving", true);
                    MoveNPC(enemyRigidbody2D, _wizardStats.movementSpeed);

                }

                TurnOnObstacle(enemyCollider2D, groundLayerMask);

                break;

            case State.chasingTarget:

                if (canEnemyMove)
                {
                    animator.SetBool("isMoving", true);
                    ChaseTarget(target, _wizardStats.movementSpeed);

                }


                FaceTarget(target);

                break;

            case State.attackingTarget:
                canEnemyMove = false;
                if (!isAttacking)
                {
                    FaceTarget(target);
                }

                animator.SetBool("isMoving", false);
                AttackPlayer();
                break;
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
            isAttacking = true;
            canEnemyMove = false;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1);
            canEnemyMove = true;
            isAttacking = false;
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
        var proj = Instantiate(_greenProjPrefab, _greenProjSpawnPos.position, this.transform.rotation);
        proj.GetComponent<WizardGreenProj>().wizardRotation = this.transform.rotation.y;
        proj.GetComponent<WizardGreenProj>().damage = _wizardStats.basicAttackDamage;


    }
    void PlayCastSound()
    {
        audioSource.PlayOneShot(_castSpellClip);
        Debug.Log("played");
    }
}
