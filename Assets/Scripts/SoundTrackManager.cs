using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackManager : MonoBehaviour
{
    [SerializeField] AudioClip _soundTrackOne;
    [SerializeField] AudioClip _soundTrackTwo;

    float _soundTrackLenght;

    AudioSource _soundTrackAudioSource;

    private void Awake()
    {
        _soundTrackAudioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (isSoundRountineON == false)
        {
            StartCoroutine(SoundTrackRoutine());
        }
    }
    bool isSoundRountineON;
    IEnumerator SoundTrackRoutine()
    {
        isSoundRountineON = true;
        yield return new WaitForSeconds(2);
        _soundTrackAudioSource.clip = _soundTrackOne;
        _soundTrackLenght = _soundTrackOne.length;
        _soundTrackAudioSource.Play();
        yield return new WaitForSeconds(_soundTrackLenght - 2);
        _soundTrackAudioSource.clip = _soundTrackTwo;
        _soundTrackLenght = _soundTrackTwo.length;
        _soundTrackAudioSource.Play();
        yield return new WaitForSeconds(_soundTrackLenght - 2);
        _soundTrackAudioSource.Stop();
        isSoundRountineON = false;
    }
}
