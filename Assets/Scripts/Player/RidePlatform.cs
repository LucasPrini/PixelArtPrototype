using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidePlatform : MonoBehaviour
{
    [SerializeField] GameObject _feetCollider;
    FeetCollider _feetColliderScript;
    public bool isRidingPlatform;

    private void Awake()
    {
        _feetColliderScript = _feetCollider.GetComponent<FeetCollider>();
    }

    private void Update()
    {
        if (_feetColliderScript.isPlayerRidingPlatform == true)
        {
            isRidingPlatform = true;
        }
        else
        {
            isRidingPlatform = false;
        }
    }


}
