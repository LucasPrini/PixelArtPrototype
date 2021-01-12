using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float shieldDuration;
    public PlayerDamager _playerDamager;

    PlayerActions _playerActions;

    private void Awake()
    {
        _playerDamager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamager>();
        _playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyDamageCollider" || other.tag == "Enemy")
        {
            if (_playerActions.isDashing == false)
            {
                _playerDamager.isPlayerShielded = false;
                Destroy(this.gameObject);
            }


        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(shieldDuration);
        Destroy(this.gameObject);
    }
}
