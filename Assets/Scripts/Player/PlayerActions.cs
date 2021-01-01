using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerController _playerController;
    GameObject _mainCanvas;

    ComboPointsManager _comboPointsManager;
    [SerializeField] CinemachineCamera _cinemachineCamera;
    PlayerStats _playerStats;
    [SerializeField] AudioClip _shootClip;
    [SerializeField] AudioClip _criticalShotClip;
    [SerializeField] float _comboTimer;
    [SerializeField] bool _canCombo = true;

    [SerializeField] float _pointsToIncreaseCrit;
    public float comboPointsCounter;

    [SerializeField] GameObject _increasedCritTextPrefab;
    [SerializeField] AudioClip _critIncreaseSound;

    [SerializeField] GameObject _critChanceTextOB;
    TextMeshProUGUI _critChanceText;
    [SerializeField] int _shotsToCrits;
    public bool isCriticalHit;

    [SerializeField] float _criticalChance = 0;
    AudioSource _audioSource;
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject _criticalShotPrefab;
    [SerializeField] Transform _projSpawnPos;
    [SerializeField] float _projectileSpeed;

    [SerializeField] Transform _grenadeSpawnPos;
    [SerializeField] GameObject _granadePrefab;
    Animator _animator;
    Collider2D _mainCollider2D;

    [SerializeField] KeyCode _grenadeKey;
    [SerializeField] KeyCode _dashKey;
    [SerializeField] AudioClip _dashSound;
    [SerializeField] GameObject _dashEffectPrefab;
    [SerializeField] float _dashCooldown;
    [SerializeField] bool _isDashInCD;

    public bool isDashing;

    private void Awake()
    {
        _comboPointsManager = GameObject.FindWithTag("GameManager").GetComponent<ComboPointsManager>();
        _mainCollider2D = GetComponent<Collider2D>();
        _playerStats = GetComponent<PlayerStats>();
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _critChanceText = _critChanceTextOB.GetComponent<TextMeshProUGUI>();
        _mainCanvas = GameObject.FindWithTag("MainCanvas");

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            _playerController.playerRigidbody2D.velocity = new Vector2(_playerController.playerRigidbody2D.velocity.x, 0f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }

        CriticalHitCalculator();
        IncreaseCriticalChance();
        ThrowGrenade();
        Dash();
    }

    [SerializeField] float _comboCounter = 0;

    void Shoot()
    {
        _comboTimer = 4f;
        StopAndShoot();

    }

    public bool isShootingRoutineOn = false;
    void StopAndShoot()
    {
        if (isShootingRoutineOn == false)
        {
            StartCoroutine(ShootingRoutine());
        }

    }

    IEnumerator ShootingRoutine()
    {
        isShootingRoutineOn = true;

        _playerController.canMove = false;
        _playerController.FaceMouseDirection();

        if (isCriticalHit)
        {

            _animator.SetTrigger("CriticalShot");
        }
        else if (!isCriticalHit)
        {

            _animator.SetTrigger("Shoot");


        }

        _comboCounter += 1;
        if (_comboCounter > _shotsToCrits)
        {
            _comboCounter = 0;
        }




        yield return new WaitForSeconds(0.5f);

        _playerController.canMove = true;
        isShootingRoutineOn = false;

    }

    void CriticalHitCalculator()
    {
        if (_comboCounter >= _shotsToCrits)
        {
            isCriticalHit = true;

        }
        else
        {
            if (Random.Range(0, 100) < _criticalChance)
            {
                isCriticalHit = true;

            }
            else
            {
                isCriticalHit = false;

            }
        }



    }

    void IncreaseCriticalChance()
    {

        if (_comboPointsManager.playerComboPoints <= 0)
        {
            _criticalChance = 0;
        }

        if (comboPointsCounter >= _pointsToIncreaseCrit)
        {
            _criticalChance += 10f;
            Vector3 critTextOffSet = new Vector3(0, 200f, 0);
            var critText = Instantiate(_increasedCritTextPrefab, _mainCanvas.transform.position + critTextOffSet, Quaternion.identity, _mainCanvas.transform);
            critText.transform.parent = _mainCanvas.transform;
            _audioSource.PlayOneShot(_critIncreaseSound);
        }

        if (comboPointsCounter >= _pointsToIncreaseCrit)
        {
            comboPointsCounter = 0;
        }



        _critChanceText.SetText("Critical Chance: " + _criticalChance + "%");


    }





    void PlayShootSound()
    {
        _audioSource.PlayOneShot(_shootClip);
    }
    void PlayCriticalSound()
    {
        _audioSource.PlayOneShot(_criticalShotClip);
    }




    void SpawnProjectile()
    {

        Instantiate(_laserPrefab, _projSpawnPos.position, Quaternion.identity);

    }
    void SpawnCritProjectile()
    {

        Instantiate(_criticalShotPrefab, _projSpawnPos.position, Quaternion.identity);

    }

    void ThrowGrenade()
    {
        if (Input.GetKeyDown(_grenadeKey))
        {
            _animator.SetTrigger("throwGrenade");
            Instantiate(_granadePrefab, _grenadeSpawnPos.position, Quaternion.identity, _mainCanvas.transform);

        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(_dashKey) && _isDashInCD == false)
        {
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {
        _isDashInCD = true;
        _mainCollider2D.enabled = false;
        isDashing = true;
        _playerController.canMove = true;

        _playerController.playerControlEnabled = false;
        _animator.SetTrigger("Dash");
        _audioSource.PlayOneShot(_dashSound);
        _cinemachineCamera.ShakeScreen(3f, 0.15f);
        _playerController.playerRigidbody2D.velocity = new Vector2(_playerStats.dashForce * _playerController._horizontalInput, 0f);
        StartCoroutine(DashEffectRoutine());

        yield return new WaitForSeconds(0.15f);
        isDashing = false;
        _playerController.playerControlEnabled = true;
        _playerController.playerRigidbody2D.velocity = new Vector2(0, -3f);
        yield return new WaitForSeconds(1f);


        _mainCollider2D.enabled = true;
        _isDashInCD = false;


    }

    bool isDashEffectRoutineOn = false;
    IEnumerator DashEffectRoutine()
    {

        Instantiate(_dashEffectPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        Instantiate(_dashEffectPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        Instantiate(_dashEffectPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        Instantiate(_dashEffectPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        Instantiate(_dashEffectPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        Instantiate(_dashEffectPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        Instantiate(_dashEffectPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.02f);
        Instantiate(_dashEffectPrefab, transform.position, transform.rotation);

    }
}

