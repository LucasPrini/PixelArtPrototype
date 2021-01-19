using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{

    public Collider2D _enemyDamageCollider;
    public GameObject floatingDamagePrefab;
    ComboPointsManager _comboPointsManager;
    [SerializeField] GameObject _comboParticleEmmiter;
    PlayerActions _playerActions;
    PlayerResources _playerResources;
    PlayerStats _playerStats;

    public AudioSource audioSource;

    public Collider2D enemyCollider2D;

    public SpriteRenderer spriteRenderer;

    public Material defaultMaterial;

    public Material hurtMaterial;
    [SerializeField] GameObject _bloodExplosionPrefab;

    public float enemyMaxHealth;
    public float enemyCurrentHealth;


    private void Awake()
    {
        _playerActions = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();
        _playerResources = GameObject.FindWithTag("Player").GetComponent<PlayerResources>();
        _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        _comboPointsManager = GameObject.FindWithTag("GameManager").GetComponent<ComboPointsManager>();
        enemyCollider2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();



        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    private void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }


    public void TakeDamage(int damageTaken, AudioClip _hurtSound, bool isCritical)
    {
        /*if (_playerActions.isCriticalHit)
        {

        }*/


        StartCoroutine(BlinkWhenHurt());
        audioSource.PlayOneShot(_hurtSound);
        GameObject floatingDamage = Instantiate(floatingDamagePrefab, enemyCollider2D.bounds.max, Quaternion.identity);

        if (isCritical)
        {
            damageTaken = (int)(damageTaken * _playerStats.critMultiplier);
            floatingDamage.GetComponent<FloatingDamage>().textMesh.color = Color.red;
            floatingDamage.GetComponent<FloatingDamage>().textMesh.fontSize = 3;
            _comboPointsManager.playerComboPoints += 10f;
            _playerActions.comboPointsCounter += 10f;
            if (_playerResources.maxResources - _playerResources.currentResources >= 10f)
            {
                _playerResources.currentResources += 10;
            }
            else if (_playerResources.maxResources - _playerResources.currentResources < 10)
            {
                _playerResources.currentResources += _playerResources.maxResources - _playerResources.currentResources;
            }
            //Instantiate(_comboParticleEmmiter, transform.position, Quaternion.identity);


        }

        enemyCurrentHealth -= damageTaken;
        floatingDamage.GetComponent<FloatingDamage>().damageReceived = damageTaken;
        if (enemyCurrentHealth <= 0)
        {
            KillEnemy();
        }
    }

    IEnumerator BlinkWhenHurt()
    {
        spriteRenderer.material = hurtMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
    }

    void KillEnemy()
    {
        Instantiate(_bloodExplosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }


}
