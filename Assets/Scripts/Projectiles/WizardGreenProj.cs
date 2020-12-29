using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardGreenProj : MonoBehaviour
{
    [SerializeField] float _projSpeed;
    public float damage = 0;

    Transform _wizardTransform;
    public float wizardRotation;

    private void Awake()
    {
        //_wizardTransform = GameObject.Find("Wizard").transform;
    }
    // Start is called before the first frame update
    void Start()
    {

        //_wizardRotation = _wizardTransform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * _projSpeed * Time.deltaTime);









    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hit");
            other.GetComponent<PlayerDamager>().TakeDamage(damage, this.transform);
            Destroy(this.gameObject);
        }
    }



}
