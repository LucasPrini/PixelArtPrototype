using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardDamager : EnemyDamager
{

    WizardStats _wizardStats;


    EnemyWizardAI _enemyWizardAI;
    float _wizardDamage;




    private void Awake()
    {
        enemyCollider2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        _wizardStats = GetComponent<WizardStats>();
        _enemyWizardAI = GetComponent<EnemyWizardAI>();
        _wizardDamage = _enemyWizardAI._greenProjPrefab.GetComponent<WizardGreenProj>().damage;
        spriteRenderer = GetComponent<SpriteRenderer>();

        enemyCurrentHealth = _wizardStats.currentHealth;
    }
    void Start()
    {
        _wizardDamage = _wizardStats.basicAttackDamage;
        enemyCurrentHealth = _wizardStats.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _wizardStats.currentHealth = enemyCurrentHealth;
    }



    /*public void TakeDamage(int damageTaken, AudioClip _hurtSound, bool isCritical)
    {
        _wizardStats.currentHealth -= damageTaken;
        StartCoroutine(BlinkWhenHurt());
        _audioSource.PlayOneShot(_hurtSound);
        GameObject floatingDamage = Instantiate(floatingDamagePrefab, _collider2D.bounds.max, Quaternion.identity);
        floatingDamage.GetComponent<FloatingDamage>().damageReceived = damageTaken;
        if (isCritical)
        {
            floatingDamage.GetComponent<FloatingDamage>().textMesh.color = Color.red;
            floatingDamage.GetComponent<FloatingDamage>().textMesh.fontSize = 3;
        }
    }*/

    /*IEnumerator BlinkWhenHurt()
    {
        spriteRenderer.material = hurtMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
    }*/


}
