using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConstraints : MonoBehaviour
{
    Collider2D _playerMainCollider;
    [SerializeField] Collider2D _backUpCollider;

    [SerializeField] bool _grounded;

    [SerializeField] LayerMask _groundLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        _playerMainCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();

        if (IsGrounded())
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
    }


    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _backUpCollider.bounds.extents.y + 0.2f, _groundLayerMask);
        Debug.DrawRay(transform.position, Vector2.down * (_backUpCollider.bounds.extents.y + 0.2f), Color.green);
        return hit.collider != null;
    }




}
