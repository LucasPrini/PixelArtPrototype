using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    [SerializeField] float _platformSpeed;
    [SerializeField] GameObject _platformArea;
    Collider2D _areaCollider2D;


    
    


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
        
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector3(_platformSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "FeetCollider"){
            Debug.Log("feetcollided");
            other.gameObject.transform.parent.parent.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "MovingPlatformArea"){
            _platformSpeed = _platformSpeed * -1;
        }
        
    }



    
}
