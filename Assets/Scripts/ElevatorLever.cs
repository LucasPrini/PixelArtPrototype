using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLever : MonoBehaviour
{
    [SerializeField] bool _playerInRange;

    AudioSource _leverAudioSource;
    [SerializeField] AudioClip _leverSound;

    [SerializeField] Sprite _deactivatedLeverSprite;
    [SerializeField] Sprite _activatedLeverSprite;
    [SerializeField] GameObject _interactText;

    SpriteRenderer _leverSpriteRenderer;

    public bool isLeverActivated;

    private void Awake()
    {
        _leverSpriteRenderer = GetComponent<SpriteRenderer>();
        _leverAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _interactText.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerInRange)
        {
            isLeverActivated = true;
            _leverAudioSource.PlayOneShot(_leverSound);
        }

        if (isLeverActivated == true)
        {
            _leverSpriteRenderer.sprite = _activatedLeverSprite;
        }
        else
        {
            _leverSpriteRenderer.sprite = _deactivatedLeverSprite;
        }

        if (_playerInRange && isLeverActivated == false)
        {
            _interactText.SetActive(true);
        }
        else
        {
            _interactText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _playerInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _playerInRange = false;
        }

    }
}
