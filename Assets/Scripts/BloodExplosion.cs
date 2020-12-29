using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodExplosion : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip _bloodSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _audioSource.PlayOneShot(_bloodSound);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
