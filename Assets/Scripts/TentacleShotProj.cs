using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleShotProj : MonoBehaviour
{
    GameObject _target;
    [SerializeField] Vector2 _targetCurrentPos;
    [SerializeField] float _projSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        if (_target != null)
        {
            _targetCurrentPos = _target.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, _targetCurrentPos, _projSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerDamager>().TakeDamage(1, this.transform);
            Destroy(this.gameObject);
        }
    }
}
