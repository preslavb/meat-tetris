using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using DefaultNamespace.GameManagerDefinitions;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShapeView : MonoBehaviour
{
    // The shape associated
    [ShowInInspector]
    private Shape _shape;
    
    // The size of the shape
    [SerializeField]
    private float _shapeSize = 7.25f;
    
    // The random manager
    public RandomManager RandomManager;
    
    // The board manager
    public BoardManager BoardManager;
    
    // Whether this shape is relevant
    [NonSerialized]
    public bool _isRelevant = true;
    
    // Whether this shape is rotating
    [NonSerialized]
    private bool _isRotating = true;
    
    // The rotation of the shape view
    private int _rotation = 0;

    // The list of cage assets
    [SerializeField]
    [PreviewField(alignment: ObjectFieldAlignment.Left)] 
    [AssetsOnly]
    private List<GameObject> _cageAssets = new List<GameObject>(4);

    [NonSerialized]
    private List<List<GameObject>> _cages;
    
    // Initialize the shape
    public void Initialize(Shape shape, Transform spawnPointTransform)
    {
        _shape = shape;
        _cages = new List<List<GameObject>>(shape.Height);

        for (int row = 0; row < shape.Height; row++)
        {
            _cages.Insert(row, new List<GameObject>());

            for (int col = 0; col < shape.Width; col++)
            {
                _cages[row].Insert(col, null);
            }
        }
        
        // Calculate the position it needs to be at
        transform.position = spawnPointTransform.position + (_shapeSize * new Vector3((float)shape.Width / 2, -(float)shape.Height / 2));
        
        // Subscribe to the board manager
        BoardManager.ShapeMoved += MoveShape;
        BoardManager.ShapeRotated += RotateShape;
        BoardManager.ShapeDropped += DropShape;
        
        RenderShape(spawnPointTransform);
    }

    // Display the shape
    private void RenderShape(Transform spawnPoint)
    {
        for (int xIndex = 0; xIndex < _shape.State[0].Count; xIndex++)
        {
            for (int yIndex = 0; yIndex < _shape.State.Count; yIndex++)
            {
                // If the position is taken, render a cage
                if (_shape.State[yIndex][xIndex])
                {
                    var position = spawnPoint.position + new Vector3(xIndex * _shapeSize, ((_shape.State.Count - 1) - yIndex) * _shapeSize);
                    
                    // Move it down enough
                    position = position - new Vector3(0, (_shape.State.Count - 1) * _shapeSize);

                    var newShape = Instantiate(_cageAssets[RandomManager.Random.Next(0, _cageAssets.Count)], transform);
                    
                    newShape.transform.position = new Vector3(_shapeSize / 2, -_shapeSize / 2);

                    newShape.transform.localScale = new Vector3(_shapeSize / 10, _shapeSize / 10);
                    newShape.transform.position += position;

                    _cages[yIndex][xIndex] = newShape;
                }
            }
        }

        // Go through a second time, disabling the colliders as needed
        for (int xIndex = 0; xIndex < _shape.State[0].Count; xIndex++)
        {
            for (int yIndex = 0; yIndex < _shape.State.Count; yIndex++)
            {
                if (_cages[yIndex][xIndex] == null)
                    continue;
                
                // Check left
                if (xIndex - 1 >= 0 && _cages[yIndex][xIndex - 1] != null)
                {
                    var cage = _cages[yIndex][xIndex];
                    
                    cage.transform.Find("ColliderLeft").gameObject.SetActive(false);
                }

                // Check right
                if (xIndex + 1 < _shape.Width && _cages[yIndex][xIndex + 1] != null)
                {
                    var cage = _cages[yIndex][xIndex];

                    cage.transform.Find("ColliderRight").gameObject.SetActive(false);
                }
                
                // Check up
                if (yIndex - 1 >= 0 && _cages[yIndex - 1][xIndex] != null)
                {
                    var cage = _cages[yIndex][xIndex];
                    
                    cage.transform.Find("ColliderTop").gameObject.SetActive(false);
                }

                // Check down
                if (yIndex + 1 < _shape.Height && _cages[yIndex + 1][xIndex] != null)
                {
                    var cage = _cages[yIndex][xIndex];

                    cage.transform.Find("ColliderBottom").gameObject.SetActive(false);
                }
            }
        }
    }
    
    // Move the shape
    private void MoveShape(int newPosition)
    {
        transform.position += new Vector3(_shapeSize * (newPosition - BoardManager.ColumnToInsertAt), 0);
    }

    // Rotate the shape
    private void RotateShape(int rotateBy)
    {
        //gameObject.transform.Rotate(0, 0, -90);
        
        // Check if there is already a rotation happening and if so, interrupt it and rotate immediately
        if (_isRotating)
        {
            StopCoroutine(RotateShapeCoroutine(0.5f));
            
            gameObject.transform.rotation = Quaternion.Euler(0, 0, _rotation * -90);

            StartCoroutine(RotateShapeCoroutine(0.5f));
        }

        else
        {
            StartCoroutine(RotateShapeCoroutine(0.5f));
        }

        _rotation++;
    }
    
    // Rotate shape coroutine
    private IEnumerator RotateShapeCoroutine(float time)
    {
        // Notify the rotation
        _isRotating = true;
        
        // Cache the rigidbody2d
        var rigidbody2d = GetComponent<Rigidbody2D>();
        
        // Track the time
        var countdown = 0f;

        while (countdown < time)
        {
            var fraction = countdown / time;
            
            rigidbody2d.MoveRotation(Mathf.Lerp(rigidbody2d.rotation, _rotation * -90, fraction));
            
            yield return new WaitForFixedUpdate();

            countdown += Time.fixedDeltaTime;
        }

        _isRotating = false;
    }
    
    // Drop the shape
    private void DropShape()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = gameObject.GetComponent<Rigidbody2D>().constraints | RigidbodyConstraints2D.FreezeRotation;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Box")) && _isRelevant)
        {
            _isRelevant = false;
            Debug.Log($"Collided with {other.gameObject.tag}");

            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            // Unsubscribe from the events
            BoardManager.ShapeMoved -= MoveShape;
            BoardManager.ShapeRotated -= RotateShape;
            BoardManager.ShapeDropped -= DropShape;
            
            // Reenable the Board manager
            BoardManager.Enable();
        }

        else if ((other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Box")) && !_isRelevant)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
