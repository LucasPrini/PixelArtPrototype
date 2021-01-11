using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    PlayerController _playerController;
    Rigidbody2D _rigidbody2D;
    CircleCollider2D _circleCollider2D;

    [SerializeField] int _grenadeDamage;
    [SerializeField] float _rollingSpeed;
    float _playerDirection;

    [SerializeField] LayerMask _enemyLayerMask;

    bool _willExplode = false;

    Vector3 currentPos;

    AudioSource _audioSource;
    [SerializeField] AudioClip _grenadeBeep;
    [SerializeField] AudioClip _grenadeExplosion;

    [SerializeField] AudioClip _hitSound;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _audioSource = GetComponent<AudioSource>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        _rigidbody2D.AddForce(new Vector2(200f * _playerController._lastHorizontalInput, 200f));
        _playerDirection = _playerController._lastHorizontalInput;
    }

    // Update is called once per frame
    void Update()
    {

        RotateGrenade();


        if (_willExplode)
        {
            transform.position = currentPos;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    /* private void OnTriggerEnter2D(Collider2D other)
     {
         Debug.Log("fora Enemy");
         if (other.tag == "Enemy")
         {
             Debug.Log("No Enemy");
             other.GetComponent<WizardDamager>().TakeDamage(_grenadeDamage, _hitSound, false);
         }
     }*/

    void DestroyGrenade()
    {
        Destroy(this.gameObject);
    }

    void PlayBeepSound()
    {
        _audioSource.PlayOneShot(_grenadeBeep);
    }

    void PlayExplosionSound()
    {
        _audioSource.PlayOneShot(_grenadeExplosion);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _rigidbody2D.velocity = new Vector3(_rollingSpeed * _playerDirection, 1);

        }
    }

    float tLerp;
    void RotateGrenade()
    {
        tLerp += 0.5f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 360, tLerp));
    }

    void StopAndExplode()
    {
        currentPos = transform.position;
        _willExplode = true;

    }

    void DamageEnemyInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f, _enemyLayerMask);
        foreach (Collider2D hit in colliders)
        {
            if (hit.tag == "Enemy")
            {
                hit.GetComponent<EnemyDamager>().TakeDamage(_grenadeDamage, _hitSound, false);
            }

        }






    }




}
