using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Code
{
    public class Shape
    {

        // Get the shape definition from the json file
        public static List<List<List<bool>>> GetShapeFromJSON(Shapes newShapeEnum)
        {
            string jsonString = Resources.Load<TextAsset>("definitions").text;

            // Create the jobject
            JObject jObject = JObject.Parse(jsonString);

            // Find the definition of the shape
            var shape = jObject[newShapeEnum.ToString()];

            return shape.ToObject<List<List<List<bool>>>>();
        }
        
        // The constructor takes a shape enum, and generates the definition for the shape
        public Shape(Shapes shapeEnum, int rotation = 0)
        {
            GenerateShape(shapeEnum, rotation);
        }
        
        // The absolute definition of this shape
        private List<List<List<bool>>> absoluteDefinition;
        
        // The definition of the shape at its current rotation
        public List<List<bool>> State { get; private set; }
        
        // The rotation of the shape
        public int Rotation = 0;
        
        // The index at which the shape starts, minding its current rotation
        public int LeftEdge
        {
            get
            {
                // Store the last checked inaccurate index
                var lastEmptyIndex = 0;

                for (int xIndex = 0; xIndex < State[0].Count; xIndex++)
                {
                    // Column filled boxes
                    var filledBoxes = 0;
                    
                    // Go through the columns to see if there are any filled ones
                    for (int yIndex = 0; yIndex < State.Count; yIndex++)
                    {
                        filledBoxes += State[yIndex][xIndex] ? 1 : 0;
                    }
                    
                    // If there are no filled boxes, this is not the left edge of the shape
                    if (filledBoxes == 0)
                    {
                        lastEmptyIndex++;
                    }

                    // Otherwise, this is the edge of the shape
                    else
                    {
                        break;
                    }
                }

                return lastEmptyIndex;
            }
        }

        // The index at which the shape ends, minding its current rotation
        public int RightEdge
        {
            get
            {
                // Store the last checked inaccurate index
                var lastEmptyIndex = State[0].Count;

                for (int xIndex = State[0].Count - 1; xIndex >= 0; xIndex--)
                {
                    // Column filled boxes
                    var filledBoxes = 0;

                    // Go through the columns to see if there are any filled ones
                    for (int yIndex = 0; yIndex < State.Count; yIndex++)
                    {
                        filledBoxes += State[yIndex][xIndex] ? 1 : 0;
                    }

                    // If there are no filled boxes, this is not the right edge of the shape
                    if (filledBoxes == 0)
                    {
                        lastEmptyIndex--;
                    }

                    // Otherwise, this is the edge of the shape
                    else
                    {
                        break;
                    }
                }

                return lastEmptyIndex;
            }
        }
        
        // The width of the shape
        public int Width => State[0].Count;
        
        // The height of the shape
        public int Height => State.Count;
        
        // Generate the correct state for the given shape
        private void GenerateShape(Shapes newShapeEnum, int rotation)
        {
            // Get the absolute definition
            absoluteDefinition = GetShapeFromJSON(newShapeEnum);
            
            // Get the actual state
            var actualShape = absoluteDefinition[rotation];
            
            // Initialize the state with the same dimensions
            State = new List<List<bool>>(4);

            for (int i = 0; i < actualShape.Count; i++)
            {
                State.Insert(i, new List<bool>(4));
                
                for (int j = 0; j < actualShape[i].Count; j++)
                {
                    State[i].Insert(j, actualShape[i][j]);
                }
            }
        }
    }
}