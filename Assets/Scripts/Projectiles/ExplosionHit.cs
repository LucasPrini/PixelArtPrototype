using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHit : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int _grenadeDamage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //other.GetComponent<WizardDamager>().TakeDamage(_grenadeDamage, _hurtSound, false)
        }
    }
}
