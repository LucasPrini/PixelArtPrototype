using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSoundManager : MonoBehaviour
{
    AudioSource _creditsAudioSource;
    [SerializeField] AudioClip _creditsAudio;

    private void Awake()
    {
        _creditsAudioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        _creditsAudioSource.clip = _creditsAudio;
        _creditsAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
