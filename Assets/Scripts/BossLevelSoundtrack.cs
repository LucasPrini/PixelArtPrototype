using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelSoundtrack : MonoBehaviour
{
    GameObject _beholderBoss;
    AudioSource _levelAudioSource;
    [SerializeField] AudioClip _introSound;
    [SerializeField] AudioClip _bossFightSound;
    private void Awake()
    {
        _levelAudioSource = GetComponent<AudioSource>();
        _beholderBoss = GameObject.Find("BeholderBoss");
    }

    private void Update()
    {
        if (_beholderBoss == null)
        {
            _levelAudioSource.Stop();
        }
    }

    public void PlayIntroSound()
    {

        _levelAudioSource.PlayOneShot(_introSound);
    }

    public void PlayBossSound()
    {
        //_levelAudioSource.PlayOneShot(_bossFightSound);
        _levelAudioSource.clip = _bossFightSound;
        _levelAudioSource.Play();
        _levelAudioSource.loop = true;
    }
}
