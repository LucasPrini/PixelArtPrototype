using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    Collider2D _collider;
    PlayerConstraints _playerContraints;
    private void Awake()
    {
        _playerContraints = GameObject.Find("Player").GetComponent<PlayerConstraints>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_playerContraints.IsGrounded())
        {
            _collider.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PassThroughTerrain")
        {
            _collider.enabled = true;
        }
    }
}
