using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class shapes
    {

        // Whether all of the shapes are generated correctly
        [Test]
        public void generates_shapes_correctly()
        {
            // Flag whether all of them passed
            bool allPassed = true;
            
            // Create a new shape for each type defined in the enum
            foreach (Shapes shape in (Shapes[]) Enum.GetValues(typeof(Shapes)))
            {
                
                // Create the shape
                var newShape = new Shape(shape);
                
                // Get the definition of that shape
                var shapeDefinition = Shape.GetShapeFromJSON(shape)[0];
                
                // Check if they match
                for (int i = 0; i < newShape.State.Count; i++)
                {
                    for (int j = 0; j < newShape.State[i].Count; j++)
                    {
                        if (newShape.State[i][j] != shapeDefinition[i][j])
                        {
                            allPassed = false;
                        }
                    }
                }
            }
            
            // Assert
            Assert.That(allPassed);
        }

        // Whether all of the shapes are generated correctly with a rotation of 90
        [Test]
        public void generates_shapes_correctly_90_degrees()
        {
            // Flag whether all of them passed
            bool allPassed = true;

            // Create a new shape for each type defined in the enum
            foreach (Shapes shape in (Shapes[]) Enum.GetValues(typeof(Shapes)))
            {

                // Create the shape
                var newShape = new Shape(shape, 1);

                // Get the definition of that shape
                var shapeDefinition = Shape.GetShapeFromJSON(shape)[1];

                // Check if they match
                for (int i = 0; i < newShape.State.Count; i++)
                {
                    for (int j = 0; j < newShape.State[i].Count; j++)
                    {
                        if (newShape.State[i][j] != shapeDefinition[i][j])
                        {
                            allPassed = false;
                        }
                    }
                }
            }

            // Assert
            Assert.That(allPassed);
        }

        [Test]
        public void detects_edges_of_long()
        {
            // Create the long shape
            var longShape = new Shape(Shapes.Long);
            
            Assert.That(longShape.LeftEdge == 0 && longShape.RightEdge == 1, () => $"The left edge is {longShape.LeftEdge} and the right is {longShape.RightEdge}");
        }
    }
}
