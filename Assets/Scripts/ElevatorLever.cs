using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLever : MonoBehaviour
{
    [SerializeField] bool _playerInRange;

    [SerializeField] Sprite _deactivatedLeverSprite;
    [SerializeField] Sprite _activatedLeverSprite;

    SpriteRenderer _leverSpriteRenderer;

    public bool isLeverActivated;

    private void Awake()
    {
        _leverSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerInRange)
        {
            isLeverActivated = true;
        }

        if (isLeverActivated == true)
        {
            _leverSpriteRenderer.sprite = _activatedLeverSprite;
        }
        else
        {
            _leverSpriteRenderer.sprite = _deactivatedLeverSprite;
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
