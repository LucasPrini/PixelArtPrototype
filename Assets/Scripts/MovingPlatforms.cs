using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    public bool moveHorizontal;
    public bool moveVertical;

    public float platformSpeed;
    [SerializeField] GameObject _platformArea;
    Collider2D _areaCollider2D;

    [SerializeField] float _platformTimer;






    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _areaCollider2D = _platformArea.GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRoutineOn == false)
        {
            StartCoroutine(PlatformTimer());


        }

        if (moveHorizontal == true)
        {
            moveVertical = false;
        }
        if (moveVertical == true)
        {
            moveHorizontal = false;
        }
    }

    private void FixedUpdate()
    {
        if (moveHorizontal == true)
        {
            _rigidbody2D.velocity = new Vector3(platformSpeed, 0, 0);
        }
        else if (moveVertical == true)
        {
            _rigidbody2D.velocity = new Vector3(0, platformSpeed, 0);
        }



    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        /*if(other.gameObject.tag == "FeetCollider"){
            Debug.Log("feetcollided");
            other.gameObject.transform.parent.parent.parent = this.transform;
        }*/
    }

    /*private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "MovingPlatformArea"){
            platformSpeed = platformSpeed * -1;
        }
        
    }*/

    bool isTimerRoutineOn;
    IEnumerator PlatformTimer()
    {
        isTimerRoutineOn = true;
        yield return new WaitForSeconds(_platformTimer);
        platformSpeed = platformSpeed * -1;
        isTimerRoutineOn = false;
    }




}
