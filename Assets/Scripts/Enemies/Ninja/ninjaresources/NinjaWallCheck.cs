using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaWallCheck : MonoBehaviour
{
    public bool areaBlocked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            areaBlocked = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            areaBlocked = true;
        }
    }
}
