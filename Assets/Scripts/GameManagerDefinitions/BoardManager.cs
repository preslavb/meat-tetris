using System;
using Code;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace DefaultNamespace.GameManagerDefinitions
{
    [CreateAssetMenu(menuName = "Managers/Board Manager", fileName = "Board Manager", order = 0)]
    public class BoardManager: GenericManager
    {
        public override ManagerType ManagerType => ManagerType.BoardManager;
        
        // An instance of the random manager
        public RandomManager RandomManager;
        
        // Events
        [NonSerialized] public Action<int> ShapeMoved;
        [NonSerialized] public Action<int> ShapeRotated;
        [NonSerialized] public Action ShapeDropped;
        
        // The board dimensions setup
        [OdinSerialize] private int width; 
        [OdinSerialize] private int height; 
        
        // The board
        public Board Board;
        
        // The column we want to insert the shape in
        [NonSerialized]
        [ShowInInspector]
        public int ColumnToInsertAt;
        
        // The current shape
        public Shape CurrentShape;
        
        // The next shape
        public Shape NextShape;
        
        public override void Setup()
        {
            Board = new Board(width, height);
            
            CurrentShape = Shape.GenerateRandomShape(RandomManager.Random, 2);
            NextShape = Shape.GenerateRandomShape(RandomManager.Random);
        }
        
        // Handle input
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var newValue = Mathf.Clamp(ColumnToInsertAt - 1, 0 - CurrentShape.LeftEdge, width - CurrentShape.Width);
                ShapeMoved?.Invoke(newValue);
                ColumnToInsertAt = newValue;
            }
            
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var newValue = Mathf.Clamp(ColumnToInsertAt + 1, 0 - CurrentShape.LeftEdge, width - CurrentShape.Width);
                ShapeMoved?.Invoke(newValue);
                ColumnToInsertAt = newValue;
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CurrentShape.RotateBy(1);
                ShapeRotated?.Invoke(1);
            }
            
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShapeDropped?.Invoke();
            }
        }
    }
}