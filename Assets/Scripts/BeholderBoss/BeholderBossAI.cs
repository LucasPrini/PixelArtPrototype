using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderBossAI : MonoBehaviour
{
    [SerializeField] GameObject _target;
    Vector2 _currentTargetPos;
    [SerializeField] Transform[] _tentacleEyes;
    [SerializeField] GameObject _tentacleShotPrefab;

    [SerializeField] Transform _arenaCenter;
    [SerializeField] float _chargeSpeed;
    bool _chargeOnPlayer;
    bool _prepareBounceAttack;
    bool _bounceOnPlayer;
    Vector2 _currentBossPos;

    // Start is called before the first frame update
    void Start()
    {
        /*if (isTentacleShotRoutineON == false)
        {
            StartCoroutine(TentacleShotRoutine());
        }*/

        if (isBounceRoutineON == false)
        {
            StartCoroutine(BounceOnPlayerRoutine());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (_chargeOnPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _arenaCenter.position, _chargeSpeed * Time.deltaTime);
        }
        if (_prepareBounceAttack)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(_currentBossPos.x, _currentBossPos.y + 3), _chargeSpeed * Time.deltaTime);
        }
        if (_bounceOnPlayer)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _currentTargetPos, _chargeSpeed * Time.deltaTime);
        }

    }

    void ShootTentacleShot(Transform tentacle)
    {
        Instantiate(_tentacleShotPrefab, tentacle.position, Quaternion.identity);
    }

    void BounceOnPlayer()
    {

    }

    bool isBounceRoutineON;
    IEnumerator BounceOnPlayerRoutine()
    {
        isBounceRoutineON = true;
        yield return new WaitForSeconds(2);
        _chargeOnPlayer = true;
        yield return new WaitForSeconds(3);
        _chargeOnPlayer = false;
        _currentBossPos = this.transform.position;
        _prepareBounceAttack = true;
        yield return new WaitForSeconds(1.1f);
        _prepareBounceAttack = false;
        _currentTargetPos = _target.transform.position;
        _bounceOnPlayer = true;
        yield return new WaitForSeconds(1f);
        _bounceOnPlayer = false;
        _currentBossPos = this.transform.position;
        _prepareBounceAttack = true;
        yield return new WaitForSeconds(1.1f);
        _prepareBounceAttack = false;
        _currentTargetPos = _target.transform.position;
        _bounceOnPlayer = true;
        yield return new WaitForSeconds(1f);
        _bounceOnPlayer = false;
        _currentBossPos = this.transform.position;
        _prepareBounceAttack = true;
        yield return new WaitForSeconds(1.1f);
        _prepareBounceAttack = false;
        _currentTargetPos = _target.transform.position;
        _bounceOnPlayer = true;
        yield return new WaitForSeconds(1f);
        _bounceOnPlayer = false;

        isBounceRoutineON = false;
    }

    bool isTentacleShotRoutineON;
    IEnumerator TentacleShotRoutine()
    {
        isTentacleShotRoutineON = true;
        yield return new WaitForSeconds(1);
        ShootTentacleShot(_tentacleEyes[0]);
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[1]);
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[2]);
        yield return new WaitForSeconds(0.6f);
        ShootTentacleShot(_tentacleEyes[3]);
        yield return new WaitForSeconds(0.6f);
        isTentacleShotRoutineON = false;
    }
}
