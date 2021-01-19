using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaDamager : EnemyDamager
{
    NinjaStats _ninjaStats;


    EnemyNinjaAI _enemyNinjaAI;
    float _ninjaDamage;




    private void Awake()
    {
        enemyCollider2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        _ninjaStats = GetComponent<NinjaStats>();
        _enemyNinjaAI = GetComponent<EnemyNinjaAI>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        //enemyCurrentHealth = _ninjaStats.currentHealth;
    }
    void Start()
    {
        //_ninjaStats = _ninjaStats.basicAttackDamage;
        enemyCurrentHealth = _ninjaStats.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _ninjaStats.currentHealth = enemyCurrentHealth;
    }



}
