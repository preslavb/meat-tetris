using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            // Get the top most object and delete it
            Transform topmostObject = other.transform;

            while (topmostObject.parent != null && topmostObject.parent.name != "Pusher")
            {
                topmostObject = topmostObject.parent;
            }
            
            Destroy(topmostObject.gameObject);
        }
    }
}
