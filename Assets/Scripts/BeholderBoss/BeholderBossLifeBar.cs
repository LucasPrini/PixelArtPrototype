using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class BeholderBossLifeBar : MonoBehaviour
{
    Slider _bossLifeSlider;

    [SerializeField] GameObject _beholderBoss;
    EnemyDamager _bossDamager;

    [SerializeField] GameObject _lifeTextOB;
    TextMeshProUGUI _lifeTextMesh;


    private void Awake()
    {
        _bossLifeSlider = GetComponent<Slider>();
        _bossDamager = _beholderBoss.GetComponent<EnemyDamager>();
        _lifeTextMesh = _lifeTextOB.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _bossLifeSlider.maxValue = _bossDamager.enemyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_beholderBoss == null)
        {
            this.gameObject.SetActive(false);
            _lifeTextOB.SetActive(false);
        }

        _bossLifeSlider.value = _bossDamager.enemyCurrentHealth;
        _lifeTextMesh.SetText(_bossDamager.enemyCurrentHealth + "/" + _bossDamager.enemyMaxHealth);
    }
}
