using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderBossAI : MonoBehaviour
{
    enum State
    {
        idle,
        BounceAttack,
        TentacleShotAttack,
        LaserBeamAttack,
    }

    [SerializeField] State bossState;

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

    private void Awake()
    {
        //_laserBeamLineRenderer = _laserBeamPrefab.GetComponent<LineRenderer>();
        _bossAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bossState = State.BounceAttack;


        /*if (isTentacleShotRoutineON == false)
        {
            StartCoroutine(TentacleShotRoutine());
        }*/


    }

    // Update is called once per frame
    void Update()
    {
        switch (bossState)
        {
            case State.idle:

                break;

            case State.BounceAttack:
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
        transform.position = new Vector3(transform.position.x, newY + _bossPosition.y, transform.position.z);
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
        yield return new WaitForSeconds(1f);
        _bounceOnPlayer = false;
        _currentBossPos = this.transform.position;
        _prepareBounceAttack = true;
        yield return new WaitForSeconds(0.9f);
        _prepareBounceAttack = false;
        _currentTargetPos = _target.transform.position;
        _bounceOnPlayer = true;
        yield return new WaitForSeconds(1f);
        _bounceOnPlayer = false;
        _currentBossPos = this.transform.position;
        _prepareBounceAttack = true;
        yield return new WaitForSeconds(0.9f);
        _prepareBounceAttack = false;
        _currentTargetPos = _target.transform.position;
        _bounceOnPlayer = true;
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
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[1]);
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[2]);
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[3]);
        yield return new WaitForSeconds(0.6f);
        bossState = State.LaserBeamAttack;


    }
}
