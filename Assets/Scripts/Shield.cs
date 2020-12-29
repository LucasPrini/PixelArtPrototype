using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] AudioClip _blockSound;
    AudioSource _audioSource;
    [SerializeField] GameObject _blockPopUp;

    Collider2D _shieldCollider2D;


    private void Awake()
    {
        _shieldCollider2D = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayBlockSound()
    {
        _audioSource.PlayOneShot(_blockSound);
    }

    public void InstantiateBlockPopUp()
    {
        Instantiate(_blockPopUp, _shieldCollider2D.bounds.max, Quaternion.identity);
    }
}
