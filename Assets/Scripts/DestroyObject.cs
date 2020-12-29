using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float _timeToDestroy = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(this.gameObject);
    }
}
