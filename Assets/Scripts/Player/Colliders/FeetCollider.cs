using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    [SerializeField] Collider2D _collider;
    [SerializeField] GameObject _player;
    PlayerConstraints _playerContraints;

    [SerializeField] GameObject _movingPlatform;
    public float platformSpeed;
    MovingPlatforms _movingPlatformsScript;

    private void Awake()
    {
        _playerContraints = GameObject.Find("Player").GetComponent<PlayerConstraints>();
        _collider = GetComponent<Collider2D>();
        //_movingPlatformsScript = _movingPlatform.GetComponent<MovingPlatforms>();

    }

    private void Update()
    {


    }
    private void OnTriggerExit2D(Collider2D other)
    {

    }
    public bool isPlayerRidingPlatform;
    public bool platformMovingHorizontal;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            if (other.gameObject.GetComponent<MovingPlatforms>().moveHorizontal == true)
            {
                platformMovingHorizontal = true;
            }
            else
            {
                platformMovingHorizontal = false;
            }
            isPlayerRidingPlatform = true;
            //Debug.Log("FeetCollided");
            //this.transform.parent.parent.SetParent(other.gameObject.transform);
            //_player.transform.SetParent(other.transform);
            //_player.GetComponent<Rigidbody2D>().velocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            platformSpeed = other.gameObject.GetComponent<MovingPlatforms>().platformSpeed;

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            isPlayerRidingPlatform = false;
            Debug.Log("FeetCollided");

        }
    }
}
