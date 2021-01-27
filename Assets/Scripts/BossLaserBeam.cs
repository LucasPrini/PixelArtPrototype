using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserBeam : MonoBehaviour
{

    LineRenderer _laserBeamRenderer;
    [SerializeField] Transform _startingTransform;
    [SerializeField] Transform _shootingPosTransform;

    Vector2 _heading;
    float _distance;
    Vector2 _direction;

    RaycastHit2D hit;

    [SerializeField] LayerMask _layerMask;

    [SerializeField] GameObject _particlesCollisionPrefab;
    GameObject _instantiatedObject;

    private void Awake()
    {
        _laserBeamRenderer = GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDisable()
    {
        Destroy(_instantiatedObject);
    }

    // Update is called once per frame
    void Update()
    {
        _heading = _shootingPosTransform.position - _startingTransform.position;
        _distance = _heading.magnitude;
        _direction = _heading / _distance;

        _laserBeamRenderer.SetPosition(0, _startingTransform.position);
        _laserBeamRenderer.SetPosition(1, _shootingPosTransform.position);
        hit = Physics2D.Raycast(_startingTransform.position, _direction, _distance, _layerMask);

        if (hit)
        {
            _laserBeamRenderer.SetPosition(1, hit.point);
            hit.transform.GetComponent<PlayerDamager>().TakeDamage(1, this.transform);
            if (isParticleRoutineON == false && alreadyInstantiated == false)
            {
                StartCoroutine(InstantiateParticles());
            }

            if (_instantiatedObject != null)
            {
                _instantiatedObject.transform.position = hit.point;
            }

        }
        if (!hit)
        {
            isParticleRoutineON = false;
            alreadyInstantiated = false;
            Destroy(_instantiatedObject);
        }
    }

    bool isParticleRoutineON;
    bool alreadyInstantiated;
    IEnumerator InstantiateParticles()
    {
        isParticleRoutineON = true;
        alreadyInstantiated = true;

        _instantiatedObject = Instantiate(_particlesCollisionPrefab, hit.point, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
    }
}
