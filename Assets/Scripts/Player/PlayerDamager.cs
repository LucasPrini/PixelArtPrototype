using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    PlayerController _playerController;
    ComboPointsManager _comboPointsManager;

    PlayerActions _playerActions;

    AudioSource _audioSource;
    Animator _animator;
    [SerializeField] AudioClip _hurtSound;

    public Collider2D damageCollider;
    PlayerStats _playerStats;
    [SerializeField] CinemachineVirtualCamera _cinemachineVirtualCamera;
    CinemachineCamera _cinemaMachineCameraScript;

    SpriteRenderer _spriteRenderer;
    [SerializeField] float _hurtKnockback;
    public bool isPlayerShielded = false;

    float transparency;


    private void Awake()
    {
        _comboPointsManager = GameObject.Find("GameManager").GetComponent<ComboPointsManager>();
        _playerActions = GetComponent<PlayerActions>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _cinemaMachineCameraScript = _cinemachineVirtualCamera.GetComponent<CinemachineCamera>();
        _audioSource = GetComponent<AudioSource>();
        _playerStats = GetComponent<PlayerStats>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {

    }


    //TakeDamageMethods
    public void TakeDamage(float damageTaken, Transform damageSourceTransform)
    {
        if (_playerActions.isDashing == false)
        {
            Vector2 knockBackDirection = (damageSourceTransform.position - this.transform.position);
            StartCoroutine(DisablePlayerControl());
            _playerController.playerRigidbody2D.velocity = -(knockBackDirection.normalized) * _hurtKnockback;
            _playerStats.currentHealth = _playerStats.currentHealth - damageTaken;
            _audioSource.PlayOneShot(_hurtSound);
            _cinemaMachineCameraScript.ShakeScreen(5f, 0.2f);
            _comboPointsManager.playerComboPoints = 0;
            _playerActions.comboCounter = 0;
            _playerActions.comboPointsCounter = 0;
            StartCoroutine(BlinkWhenHurt());
        }





    }
    IEnumerator BlinkWhenHurt()
    {
        _spriteRenderer.color = new Color(1, 0.5f, 0.5f, 0);
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = new Color(1, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = new Color(1, 0.5f, 0.5f, 0);
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = new Color(1, 1f, 1f, 1);

    }
    IEnumerator DisablePlayerControl()
    {
        _playerController.playerControlEnabled = false;
        yield return new WaitForSeconds(0.4f);
        _playerController.playerControlEnabled = true;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && isPlayerShielded == false)
        {
            TakeDamage(1, other.gameObject.transform);
        }
    }
}
