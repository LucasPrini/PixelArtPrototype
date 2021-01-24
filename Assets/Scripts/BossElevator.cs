using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElevator : MonoBehaviour
{
    [SerializeField] float _elevatorSpeed;
    Rigidbody2D _elevatorRigidbody2D;
    Animator _elevatorAnimator;
    public bool moveElevator;

    [SerializeField] GameObject _elevatorLever;
    [SerializeField] GameObject _elevatorEntranceCollider;
    ElevatorLever _elevatorLeverScript;

    private void Start()
    {
        _elevatorRigidbody2D = GetComponent<Rigidbody2D>();
        _elevatorLeverScript = _elevatorLever.GetComponent<ElevatorLever>();
        _elevatorAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_elevatorLeverScript.isLeverActivated && isElevatorRoutineOn == false)
        {
            StartCoroutine(ElevatorRoutine());
        }
    }

    private void FixedUpdate()
    {
        if (moveElevator == true)
        {
            _elevatorRigidbody2D.velocity = new Vector3(0, _elevatorSpeed, 0);

        }
    }

    bool isElevatorRoutineOn;
    IEnumerator ElevatorRoutine()
    {
        isElevatorRoutineOn = true;
        _elevatorEntranceCollider.SetActive(true);
        _elevatorAnimator.SetTrigger("CloseGate");
        yield return new WaitForSeconds(0.5f);
        moveElevator = true;
        yield return new WaitForSeconds(2f);
        moveElevator = false;

    }
}
