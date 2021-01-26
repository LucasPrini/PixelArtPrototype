using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserStartingPoint : MonoBehaviour
{
    [SerializeField] Transform _target;
    bool _startFollowingTarget;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLaserRoutineON == false)
        {
            StartCoroutine(LaserRoutine());
        }

        if (_startFollowingTarget == false)
        {
            transform.position = new Vector2(_target.position.x, _target.position.y - 5);
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, _target.position.y), 5 * Time.deltaTime);
        }


    }


    bool isLaserRoutineON;
    IEnumerator LaserRoutine()
    {
        isLaserRoutineON = true;
        yield return new WaitForSeconds(1f);
        _startFollowingTarget = true;

    }
}
