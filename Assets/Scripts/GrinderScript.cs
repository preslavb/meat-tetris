using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GrinderScript : MonoBehaviour
{

    public GameObject BloodSplatterPrefab;

    public ScoreSaver ScoreSaver;
    
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
            
            // Check if there is a chicken in the box
            if (topmostObject.GetComponentInChildren<EyeSpriteController>() != null)
            {
                Instantiate(BloodSplatterPrefab);
                ScoreSaver.score += 0.5f;
            }

            // Check if there is a chicken in the box
            else if (topmostObject.GetComponentInChildren<GoreFlag>() != null && topmostObject.GetComponentInChildren<GoreFlag>().enabled)
            {
                Instantiate(BloodSplatterPrefab);
                ScoreSaver.score += 0.5f;
            }
            
            Destroy(topmostObject.gameObject);
        }
        
        else if (other.GetComponentInParent<EyeSpriteController>() != null)
        {
            var actualAnimal = other.GetComponentInParent<EyeSpriteController>();

            Instantiate(BloodSplatterPrefab);
            ScoreSaver.score++;
            
            Destroy(actualAnimal.gameObject);
        }
    }
}
