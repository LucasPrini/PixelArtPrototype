using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    [SerializeField] Collider2D _feetCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PassThroughTerrain")
        {
            _feetCollider.enabled = false;
        }
    }
}
