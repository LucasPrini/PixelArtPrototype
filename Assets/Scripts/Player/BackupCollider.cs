using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupCollider : MonoBehaviour
{
    Collider2D _backupCollider;

    private void Awake()
    {
        _backupCollider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Collider2D enemyCollider = other.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(_backupCollider, enemyCollider, true);
        }

    }
}
