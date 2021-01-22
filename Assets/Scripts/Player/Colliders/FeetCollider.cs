using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    [SerializeField] Collider2D _collider;
    [SerializeField] GameObject _player;
    PlayerConstraints _playerContraints;
    private void Awake()
    {
        _playerContraints = GameObject.Find("Player").GetComponent<PlayerConstraints>();
        _collider = GetComponent<Collider2D>();

    }

    private void Update()
    {
        /*if (_playerContraints.IsGrounded())
        {
            _collider.enabled = true;
        }*/
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.tag == "PassThroughTerrain")
        {
            _collider.enabled = true;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            Debug.Log("FeetCollided");
            //this.transform.parent.parent.SetParent(other.gameObject.transform);
            _player.transform.parent = other.transform;
        }
    }
}
