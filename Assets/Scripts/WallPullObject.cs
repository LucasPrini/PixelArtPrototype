using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPullObject : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    bool _push;
    [SerializeField] float _routineStopTime;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void PushPlayerOut()
    {
        if (isWallPushRoutineOn == false)
        {
            StartCoroutine(wallPullRoutine());
        }
    }

    private void FixedUpdate()
    {
        if (_push)
        {
            _rigidbody2D.velocity = new Vector3(-10, 0, 0);
        }

    }

    bool isWallPushRoutineOn;
    IEnumerator wallPullRoutine()
    {
        isWallPushRoutineOn = true;
        _push = true;
        yield return new WaitForSeconds(_routineStopTime);
        _rigidbody2D.velocity = Vector3.zero;
        _push = false;
    }
}
