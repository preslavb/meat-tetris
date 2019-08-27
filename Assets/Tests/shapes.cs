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
                
                // Check if they match
                for (int i = 0; i < newShape.State.GetLength(0); i++)
                {
                    for (int j = 0; j < newShape.State.GetLength(1); j++)
                    {
                        if (newShape.State[i, j] != Shape.Definitions[shape][i, j])
                        {
                            allPassed = false;
                        }
                    }
                }
                
            }
            
            // Assert
            Assert.That(allPassed);
        }
    }
}
