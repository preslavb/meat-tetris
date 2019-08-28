using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class testSnapMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Random _random;
    
    private Vector2 CurrentVelocityChange = Vector2.zero;

    public float movingConstant = 1.5f; 
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _random = new Random();
        _random = new Random(_random.Next());
    }


    private void FixedUpdate()
    {
        CurrentVelocityChange += new Vector2((float)(_random.NextDouble() * movingConstant * 2) - movingConstant, (float)(_random.NextDouble() * movingConstant * 2) - movingConstant);
        
        // Clamp
        CurrentVelocityChange = new Vector2(Mathf.Clamp(CurrentVelocityChange.x, -movingConstant, movingConstant), Mathf.Clamp(CurrentVelocityChange.y, -movingConstant, movingConstant));

        Rigidbody2D.velocity = CurrentVelocityChange;
    }
}
