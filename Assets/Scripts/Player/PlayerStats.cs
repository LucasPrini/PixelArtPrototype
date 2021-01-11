using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : GenericStats
{
    public float dashForce;

    private void Start()
    {
        currentHealth = maxHealth;
    }
}
