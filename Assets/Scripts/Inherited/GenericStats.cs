using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float movementSpeed;

    public float basicAttackDamage;
    public float attackRange;

    public float attackSpeed;

    private void Start()
    {
        currentHealth = maxHealth;
    }


}
