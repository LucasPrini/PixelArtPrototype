using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwarenessRange : MonoBehaviour
{
    public bool playerInRange;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
