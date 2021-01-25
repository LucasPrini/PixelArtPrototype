using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElevator : MonoBehaviour
{
    [SerializeField] GameObject _levelSoundTrackOB;
    BossLevelSoundtrack _levelSoundTrackScript;

    [SerializeField] GameObject _wallPullOB;
    WallPullObject _wallPullScript;
    [SerializeField] float _elevatorSpeed;
    Rigidbody2D _elevatorRigidbody2D;
    Animator _elevatorAnimator;
    public bool moveElevator;
    AudioSource _elevatorAudioSource;
    [SerializeField] AudioClip _elevatorGateSound;
    [SerializeField] AudioClip _elevatorReadySound;


    [SerializeField] GameObject _elevatorLever;
    [SerializeField] GameObject _elevatorEntranceCollider;
    ElevatorLever _elevatorLeverScript;

    private void Start()
    {
        _elevatorRigidbody2D = GetComponent<Rigidbody2D>();
        _elevatorLeverScript = _elevatorLever.GetComponent<ElevatorLever>();
        _elevatorAnimator = GetComponent<Animator>();
        _elevatorAudioSource = GetComponent<AudioSource>();
        _levelSoundTrackScript = _levelSoundTrackOB.GetComponent<BossLevelSoundtrack>();
        _wallPullScript = _wallPullOB.GetComponent<WallPullObject>();
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

    public void PlayReadySound()
    {
        _elevatorAudioSource.PlayOneShot(_elevatorReadySound);
    }

    bool isElevatorRoutineOn;
    IEnumerator ElevatorRoutine()
    {
        isElevatorRoutineOn = true;
        _elevatorEntranceCollider.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        _elevatorAnimator.SetTrigger("CloseGate");
        _elevatorAudioSource.PlayOneShot(_elevatorGateSound);
        yield return new WaitForSeconds(2.5f);
        _levelSoundTrackScript.PlayIntroSound();
        moveElevator = true;
        yield return new WaitForSeconds(20f);

        moveElevator = false;
        _elevatorRigidbody2D.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
        _elevatorEntranceCollider.SetActive(false);
        _wallPullScript.PushPlayerOut();
        _levelSoundTrackScript.PlayBossSound();

    }
}
