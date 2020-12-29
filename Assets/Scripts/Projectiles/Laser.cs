using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    GameObject _player;
    float _currentPlayerYRotation;
    [SerializeField] float _projectileSpeed;
    public int damage;
    [SerializeField] int[] damageRange;

    [SerializeField] bool _isCriticalHit;

    [SerializeField] AudioClip _hitSound;

    Rigidbody2D _projRigidbody2D;

    Collider2D _projCollider;

    bool _shieldHit = false;



    // Start is called before the first frame update
    private void Awake()
    {
        _projRigidbody2D = GetComponent<Rigidbody2D>();
        _projCollider = GetComponent<Collider2D>();
        _player = GameObject.Find("Player");
        //_currentPlayerInput = _player._lastHorizontalInput;
        damage = Random.Range(damageRange[0], damageRange[1]);
        if (_isCriticalHit)
        {
            damage = damage * 2;
        }
    }
    private void Start()
    {
        _currentPlayerYRotation = _player.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPlayerYRotation == 0)
        {
            //transform.Translate(Vector3.right * Time.deltaTime * _projectileSpeed);
            transform.rotation = Quaternion.Euler(0, 0f, 0);
            _projRigidbody2D.velocity = Vector2.right * _projectileSpeed;
        }
        else
        {
            //transform.Translate(Vector3.right * Time.deltaTime * _projectileSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _projRigidbody2D.velocity = Vector2.left * _projectileSpeed;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shield")
        {
            _shieldHit = true;
            other.GetComponent<Shield>().PlayBlockSound();
            other.GetComponent<Shield>().InstantiateBlockPopUp();

            Destroy(this.gameObject);
        }

        if (other.tag == "Enemy" && _shieldHit == false)
        {
            other.GetComponent<EnemyDamager>().TakeDamage(damage, _hitSound, _isCriticalHit);
            Destroy(this.gameObject);
        }

    }
}
