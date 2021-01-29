using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConstraints : MonoBehaviour
{
    Collider2D _playerMainCollider;
    [SerializeField] Collider2D _backUpCollider;

    public bool isGrounded;

    [SerializeField] LayerMask _groundLayerMask;

    public bool _stillGrounded;
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
            isGrounded = true;
        }
        else
        {
            if (isCoyoteRoutineOn == false)
            {
                StartCoroutine(CoyoteJumpRoutine());
            }

        }
    }


    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _backUpCollider.bounds.extents.y + 0.2f, _groundLayerMask);
        Debug.DrawRay(transform.position, Vector2.down * (_backUpCollider.bounds.extents.y + 0.2f), Color.green);
        isCoyoteRoutineOn = false;
        return hit.collider != null;
    }

    bool isCoyoteRoutineOn;
    IEnumerator CoyoteJumpRoutine()
    {
        isCoyoteRoutineOn = true;
        yield return new WaitForSeconds(0.2f);
        isGrounded = false;
    }




}
