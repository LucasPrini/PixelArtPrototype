using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralCollider : MonoBehaviour
{
    [SerializeField] Collider2D _feetCollider;

    PlayerConstraints _playerContraints;

    [SerializeField] bool _isPassingThroughTerrain;

    private void Awake()
    {
        _playerContraints = GameObject.Find("Player").GetComponent<PlayerConstraints>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _feetCollider.enabled = false;
        }
        if (_playerContraints.IsGrounded() && _isPassingThroughTerrain == false)
        {
            _feetCollider.enabled = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PassThroughTerrain")
        {
            _isPassingThroughTerrain = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PassThroughTerrain")
        {
            _isPassingThroughTerrain = false;
        }
    }


}
