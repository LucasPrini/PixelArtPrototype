using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool playerControlEnabled = true;
    public bool canMove = true;
    public Rigidbody2D playerRigidbody2D;
    [SerializeField] Vector3 _mousePos;

    PlayerConstraints _playerContraints;
    PlayerActions _playerActions;

    RidePlatform _ridePlatform;
    //[SerializeField] GameObject _movingPlatform;
    //MovingPlatforms _movingPlatforms;
    [SerializeField] GameObject _feetCollider;
    FeetCollider _feetColliderScript;

    Animator _animator;

    public float _horizontalInput;
    public float _lastHorizontalInput;
    [SerializeField] float _moveSpeed;

    [SerializeField] KeyCode _jumpKey;
    public float jumpHeight;
    [SerializeField] bool _canJump = true;
    [SerializeField] int _jumpCounter;
    AudioSource _audioSource;
    [SerializeField] AudioClip _jumpSound;
    [SerializeField] int _airJumps;
    public bool jumping;

    float _earlyJumpCountdown;

    [SerializeField] Vector3 _playerVelocity;

    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerContraints = GetComponent<PlayerConstraints>();
        _audioSource = GetComponent<AudioSource>();
        _playerActions = GetComponent<PlayerActions>();
        _ridePlatform = GetComponent<RidePlatform>();
        //_movingPlatforms = _movingPlatform.GetComponent<MovingPlatforms>();
        _feetColliderScript = _feetCollider.GetComponent<FeetCollider>();
    }



    // Update is called once per frame
    void Update()
    {
        _playerVelocity = playerRigidbody2D.velocity;

        JumpTimer();

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        if (canMove == false)
        {
            playerRigidbody2D.velocity = new Vector2(0, 0);
        }
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Jump();
        RotateCharacter();
        RestricAirJump();



        if (_horizontalInput < 0)
        {
            _lastHorizontalInput = -1;
        }
        else if (_horizontalInput > 0)
        {
            _lastHorizontalInput = 1;
        }
    }

    private void FixedUpdate()
    {
        if (playerControlEnabled && canMove)
        {
            MoveCharacter();
        }

    }

    void MoveCharacter()
    {

        if (_horizontalInput != 0 && _playerContraints.IsGrounded() == true)
        {
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

        if (_ridePlatform.isRidingPlatform == true)
        {
            if (_feetColliderScript.platformMovingHorizontal == true)
            {
                playerRigidbody2D.velocity = new Vector2(_feetColliderScript.platformSpeed + _horizontalInput * _moveSpeed, playerRigidbody2D.velocity.y);
            }
            else
            {
                playerRigidbody2D.velocity = new Vector2(_horizontalInput * _moveSpeed, playerRigidbody2D.velocity.y);

            }



        }
        else
        {
            playerRigidbody2D.velocity = new Vector2(_horizontalInput * _moveSpeed, playerRigidbody2D.velocity.y);

        }


    }

    void Jump()
    {


        if (_earlyJumpCountdown > 0 && _canJump)
        {
            jumping = true;
            canMove = true;
            _playerActions.isDashing = false;
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpHeight);
            _animator.SetTrigger("jump");
            _audioSource.PlayOneShot(_jumpSound);
            _jumpCounter += 1;
            jumping = false;
        }
    }

    void JumpTimer()
    {
        if (Input.GetKeyDown(_jumpKey))
        {
            _earlyJumpCountdown = 0.2f;
            Jump();

        }
        if (_earlyJumpCountdown > 0)
        {
            _earlyJumpCountdown -= Time.deltaTime;
        }
    }

    void RestricAirJump()
    {
        if (_playerContraints.isGrounded)
        {
            _jumpCounter = 0;
            _canJump = true;
        }
        if (_jumpCounter > _airJumps)
        {
            _canJump = false;
        }

    }

    void RotateCharacter()
    {


        if (_playerActions.isShootingRoutineOn == false)
        {
            if (_horizontalInput < 0)
            {
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0);
            }
            else if (_horizontalInput > 0)
            {
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0);
            }
        }


    }

    public void FaceMouseDirection()
    {
        _horizontalInput = 0;

        if (_mousePos.x < this.transform.position.x)
        {
            _lastHorizontalInput = -1;
            this.transform.rotation = Quaternion.Euler(0f, 180f, 0);
        }
        else if (_mousePos.x > this.transform.position.x)
        {
            _lastHorizontalInput = 1;
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0);
        }
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













