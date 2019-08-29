using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using DefaultNamespace.GameManagerDefinitions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class ShapeView : SerializedMonoBehaviour
{
    // The shape associated
    private Shape _shape;
    
    // The size of the shape
    [OdinSerialize]
    private float _shapeSize = 2;
    
    // The random manager
    public RandomManager RandomManager;
    
    // The board manager
    public BoardManager BoardManager;

    // The list of cage assets
    [OdinSerialize] 
    [PreviewField(alignment: ObjectFieldAlignment.Left)] 
    [AssetsOnly]
    private List<GameObject> _cageAssets = new List<GameObject>(4);

    [NonSerialized]
    private List<GameObject> _cages;
    
    // Initialize the shape
    public void Initialize(Shape shape, Transform spawnPointTransform)
    {
        _shape = shape;
        _cages = new List<GameObject>();
        
        // Subscribe to the board manager
        BoardManager.ShapeMoved += MoveShape;
        //BoardManager.ShapeDropped += MoveShape;
        
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
                    
                    var newShape = Instantiate(_cageAssets[RandomManager.Random.Next(0, _cageAssets.Count)], 
                        new Vector3(_shapeSize / 2, -_shapeSize / 2),
                        Quaternion.identity);

                    newShape.transform.localScale = new Vector3(_shapeSize / 10, _shapeSize / 10);
                    newShape.transform.position += position;
                    
                    _cages.Add(newShape);
                }
            }
        }
    }
    
    // Move the shape
    private void MoveShape(int newPosition)
    {
        _cages.ForEach(cage =>
        {
            cage.transform.position += new Vector3(_shapeSize * (newPosition - BoardManager.ColumnToInsertAt), 0);
        });
    }
}
