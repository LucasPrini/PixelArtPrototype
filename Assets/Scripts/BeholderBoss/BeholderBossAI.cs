using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderBossAI : MonoBehaviour
{
    public enum State
    {
        idle,
        BounceAttack,
        TentacleShotAttack,
        LaserBeamAttack,
    }


    EnemyDamager _bossDamager;
    [SerializeField] GameObject _beholderBloodExplosion;
    [SerializeField] GameObject _deathLightPrefab;


    SpriteRenderer _bossSpriteRenderer;
    Material _bossStartingMaterial;

    Animator _bossAnimator;

    public State bossState;

    public bool _killLaser;
    [SerializeField] GameObject _bossHealthCanvas;

    AudioSource _bossAudioSource;
    [SerializeField] GameObject _target;
    Vector2 _currentTargetPos;
    bool _stopFloating;
    bool _stopFacingTarget;
    [SerializeField] Transform[] _tentacleEyes;
    [SerializeField] GameObject _tentacleShotPrefab;

    [Header("ArenaPositions")]
    [SerializeField] Transform _arenaCenter;
    [SerializeField] Transform _arenaRoofTop;
    [SerializeField] Transform _arenaBottomLeft;
    [SerializeField] Transform _arenaBottomRight;

    float _arenaPosition;

    [Header("BounceAttackVariables")]
    [SerializeField] float _chargeSpeed;
    [SerializeField] GameObject _virtualCam;
    CinemachineCamera _cinemachineCam;
    bool _chargeOnPlayer;
    bool _prepareBounceAttack;
    bool _bounceOnPlayer;
    Vector2 _currentBossPos;

    //floating movement variables 

    Vector3 _bossPosition;
    [Header("FloatingMovement")]
    [SerializeField] float _floatingSpeed;
    [SerializeField] float _floatingHeight;


    [Header("LaserBeam Variables")]
    [SerializeField] Transform _mainEyeTransform;
    [SerializeField] GameObject _laserBeamPrefab;
    [SerializeField] GameObject _laserParticlesPrefab;
    [SerializeField] GameObject[] _particlesHolder;
    LineRenderer _laserBeamLineRenderer;
    [SerializeField] float _arenaSide;

    [Header("Sounds")]
    [SerializeField] AudioClip _laserCharge;
    [SerializeField] AudioClip _laserRelease;
    [SerializeField] AudioClip _tentacleShotSound;

    [SerializeField] AudioClip _boundSound;

    private void Awake()
    {
        //_laserBeamLineRenderer = _laserBeamPrefab.GetComponent<LineRenderer>();
        _bossAudioSource = GetComponent<AudioSource>();
        _bossDamager = GetComponent<EnemyDamager>();
        _bossAnimator = GetComponent<Animator>();
        _bossSpriteRenderer = GetComponent<SpriteRenderer>();
        _bossStartingMaterial = _bossSpriteRenderer.material;
        _cinemachineCam = _virtualCam.GetComponent<CinemachineCamera>();

    }

    // Start is called before the first frame update
    void Start()
    {
        bossState = State.idle;


        /*if (isTentacleShotRoutineON == false)
        {
            StartCoroutine(TentacleShotRoutine());
        }*/


    }

    // Update is called once per frame
    void Update()
    {
        if (_killLaser == true)
        {
            _laserBeamPrefab.SetActive(false);
            _bossHealthCanvas.SetActive(false);
        }

        if (_bossDamager.enemyCurrentHealth <= 0)
        {
            KillBoss();
        }

        switch (bossState)
        {
            case State.idle:
                if (!_stopFloating)
                {
                    FloatingMovement();
                }
                break;

            case State.BounceAttack:
                _bossHealthCanvas.SetActive(true);
                if (isBounceRoutineON == false)
                {
                    StartCoroutine(BounceOnPlayerRoutine());
                }
                isTentacleShotRoutineON = false;
                break;

            case State.TentacleShotAttack:
                TentacleShoot();
                isLaserBeamRoutineOn = false;
                break;

            case State.LaserBeamAttack:
                CastLaserBeam();
                isBounceRoutineON = false;
                break;
        }
        _bossPosition = transform.position;
        if (!_stopFloating)
        {
            FloatingMovement();
        }

        if (!_stopFacingTarget)
        {
            FaceTarget();
        }
        BounceOnPlayer();



    }

    private void FixedUpdate()
    {




    }

    void ShootTentacleShot(Transform tentacle)
    {
        Instantiate(_tentacleShotPrefab, tentacle.position, Quaternion.identity);
    }

    void BounceOnPlayer()
    {
        if (_chargeOnPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _arenaCenter.position, _chargeSpeed * Time.deltaTime);
        }
        if (_prepareBounceAttack)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(_currentBossPos.x, _arenaRoofTop.position.y), (_chargeSpeed + 3) * Time.deltaTime);
        }
        if (_bounceOnPlayer)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _currentTargetPos, _chargeSpeed * Time.deltaTime);
        }
    }

    void TentacleShoot()
    {
        if (isTentacleShotRoutineON == false)
        {
            StartCoroutine(TentacleShotRoutine());
        }
    }

    void CastLaserBeam()
    {
        if (isLaserBeamRoutineOn == false)
        {
            StartCoroutine(LaserBeamRoutine());
        }
    }

    void RandomArenaSide()
    {
        _arenaSide = Random.Range(1, 3);
        if (_arenaSide == 1)
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, _arenaBottomRight.position, _chargeSpeed * Time.deltaTime);
            transform.position = _arenaBottomRight.position;
        }
        else
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, _arenaBottomLeft.position, _chargeSpeed * Time.deltaTime);
            transform.position = _arenaBottomLeft.position;
        }
    }

    void RandomArenaPosition()
    {
        _arenaPosition = Random.Range(1, 5);
        if (_arenaPosition == 1)
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, _arenaBottomRight.position, _chargeSpeed * Time.deltaTime);
            transform.position = _arenaBottomRight.position;
        }
        else if (_arenaPosition == 2)
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, _arenaBottomLeft.position, _chargeSpeed * Time.deltaTime);
            transform.position = _arenaBottomLeft.position;
        }
        else if (_arenaPosition == 3)
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, _arenaBottomLeft.position, _chargeSpeed * Time.deltaTime);
            transform.position = _arenaRoofTop.position;
        }
        else if (_arenaPosition == 4)
        {
            //transform.position = Vector3.MoveTowards(this.transform.position, _arenaBottomLeft.position, _chargeSpeed * Time.deltaTime);
            transform.position = _arenaCenter.position;
        }
    }

    [SerializeField] bool isLaserBeamRoutineOn;
    IEnumerator LaserBeamRoutine()
    {
        isLaserBeamRoutineOn = true;
        RandomArenaSide();
        yield return new WaitForSeconds(2);
        _stopFloating = true;

        _bossAudioSource.PlayOneShot(_laserCharge);
        for (int i = 0; i < 5; i++)
        {
            _particlesHolder[i] = Instantiate(_laserParticlesPrefab, _mainEyeTransform.position, Quaternion.identity);
            _particlesHolder[i].transform.SetParent(_mainEyeTransform);
            _particlesHolder[i].transform.localPosition = Vector2.zero;
            yield return new WaitForSeconds(0.5f);

        }
        _stopFacingTarget = true;
        yield return new WaitForSeconds(0.8f);
        _bossAudioSource.PlayOneShot(_laserRelease);
        _laserBeamPrefab.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _laserBeamPrefab.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            Destroy(_particlesHolder[i]);

        }
        yield return new WaitForSeconds(0.8f);
        _stopFacingTarget = false;

        bossState = State.BounceAttack;




    }


    void FloatingMovement()
    {

        float newY = Mathf.Sin(Time.time * _floatingSpeed) * _floatingHeight;
        transform.position = new Vector3(transform.position.x, newY + transform.position.y, transform.position.z);
    }

    public void FaceTarget()
    {
        if (this.transform.position.x > _target.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (this.transform.position.x < _target.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void KillBoss()
    {
        Destroy(_bossDamager);
        if (isKillBeholderRoutine == false)
        {
            StartCoroutine(KillBeholderRoutine());
        }
    }

    bool isKillBeholderRoutine;
    IEnumerator KillBeholderRoutine()
    {

        isKillBeholderRoutine = true;
        _stopFloating = true;
        _bossSpriteRenderer.material = _bossStartingMaterial;
        Instantiate(_deathLightPrefab, transform.position, Quaternion.identity);
        _bossAnimator.SetTrigger("Die");

        yield return new WaitForSeconds(5.1f);

        Instantiate(_beholderBloodExplosion, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

    [SerializeField] bool isBounceRoutineON;
    IEnumerator BounceOnPlayerRoutine()
    {
        isBounceRoutineON = true;
        yield return new WaitForSeconds(2);
        _chargeOnPlayer = true;
        yield return new WaitForSeconds(1.5f);
        _chargeOnPlayer = false;
        _currentBossPos = this.transform.position;
        _prepareBounceAttack = true;
        yield return new WaitForSeconds(0.9f);
        _prepareBounceAttack = false;
        _currentTargetPos = _target.transform.position;
        _bounceOnPlayer = true;
        _bossAudioSource.PlayOneShot(_boundSound);
        _cinemachineCam.ShakeScreen(3, 1);
        yield return new WaitForSeconds(1f);
        _bounceOnPlayer = false;
        _currentBossPos = this.transform.position;
        _prepareBounceAttack = true;
        yield return new WaitForSeconds(0.9f);
        _prepareBounceAttack = false;
        _currentTargetPos = _target.transform.position;
        _bounceOnPlayer = true;
        _bossAudioSource.PlayOneShot(_boundSound);
        _cinemachineCam.ShakeScreen(3, 1);
        yield return new WaitForSeconds(1f);
        _bounceOnPlayer = false;
        _currentBossPos = this.transform.position;
        _prepareBounceAttack = true;
        yield return new WaitForSeconds(0.9f);
        _prepareBounceAttack = false;
        _currentTargetPos = _target.transform.position;
        _bounceOnPlayer = true;
        _bossAudioSource.PlayOneShot(_boundSound);
        _cinemachineCam.ShakeScreen(3, 1);
        yield return new WaitForSeconds(1f);
        _bounceOnPlayer = false;

        bossState = State.TentacleShotAttack;

    }

    [SerializeField] bool isTentacleShotRoutineON;
    IEnumerator TentacleShotRoutine()
    {
        isTentacleShotRoutineON = true;
        yield return new WaitForSeconds(1);
        RandomArenaPosition();

        yield return new WaitForSeconds(1);
        ShootTentacleShot(_tentacleEyes[0]);
        _bossAudioSource.PlayOneShot(_tentacleShotSound);
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[1]);
        _bossAudioSource.PlayOneShot(_tentacleShotSound);
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[2]);
        _bossAudioSource.PlayOneShot(_tentacleShotSound);
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[3]);
        _bossAudioSource.PlayOneShot(_tentacleShotSound);
        yield return new WaitForSeconds(0.6f);
        bossState = State.LaserBeamAttack;


    }
}
