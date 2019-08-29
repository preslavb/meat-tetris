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
        
        // Disable input
        [NonSerialized]
        private bool _disabledInput = false;
        
        // Events
        [NonSerialized] public Action<int> ShapeMoved;
        [NonSerialized] public Action<int> ShapeRotated;
        [NonSerialized] public Action ShapeDropped;
        [NonSerialized] public Action ShapeDoneDropping;
        
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
            
            CurrentShape = Shape.GenerateRandomShape(RandomManager.Random);
            NextShape = Shape.GenerateRandomShape(RandomManager.Random);
        }
        
        // Handle input
        public override void Update()
        {
            if (_disabledInput)
                return;
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var newValue = Mathf.Clamp(ColumnToInsertAt - 1, 0 - CurrentShape.LeftEdge, width - CurrentShape.RightEdge);
                ShapeMoved?.Invoke(newValue);
                ColumnToInsertAt = newValue;
            }
            
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var newValue = Mathf.Clamp(ColumnToInsertAt + 1, 0 - CurrentShape.LeftEdge, width - CurrentShape.RightEdge);
                ShapeMoved?.Invoke(newValue);
                ColumnToInsertAt = newValue;
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CurrentShape.RotateBy(1);
                var newValue = Mathf.Clamp(ColumnToInsertAt, 0 - CurrentShape.LeftEdge, width - CurrentShape.RightEdge);
                ShapeMoved?.Invoke(newValue);
                ColumnToInsertAt = newValue;
                ShapeRotated?.Invoke(1);
            }
            
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShapeDropped?.Invoke();
                Board.InsertShape(CurrentShape, ColumnToInsertAt);

                _disabledInput = true;
            }
        }

        // Reenable inputs
        public void Enable()
        {
            if (!_disabledInput)
                return;
            
            _disabledInput = false;

            CurrentShape = NextShape;
            NextShape = Shape.GenerateRandomShape(RandomManager.Random);
            ColumnToInsertAt = 0;
            
            ShapeDoneDropping?.Invoke();
        }
    }
}