using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setupignores : MonoBehaviour
{
    public Collider2D[] collidersToIgnore;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Collider2D collider2D in collidersToIgnore)
        {
            foreach (var secondCollider in collidersToIgnore)
            {
                Physics2D.IgnoreCollision(collider2D, secondCollider, true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
