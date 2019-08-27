using System.Collections.Generic;

namespace Code
{
    public class Shape
    {
        
        // The definitions for shapes (TODO: Ideally move this to a JSON)
        public static readonly Dictionary<Shapes, byte[,]> Definitions = new Dictionary<Shapes, byte[,]>
        {
            {
                Shapes.S,
                new byte[2, 3]
                {
                    {0, 1, 1},
                    {1, 1, 0}
                }
            },
            {
                Shapes.Box,
                new byte[2, 2]
                {
                    {1, 1},
                    {1, 1}
                }
            },
            {
                Shapes.L,
                new byte[2, 3]
                {
                    {0, 0, 1},
                    {1, 1, 1}
                }
            },
            {
                Shapes.Long,
                new byte[4, 1]
                {
                    {1},
                    {1},
                    {1},
                    {1}
                }
            },
            {
                Shapes.T,
                new byte[2, 3]
                {
                    {1, 1, 1},
                    {0, 1, 0}
                }
            },
            {
                Shapes.Z,
                new byte[2, 3]
                {
                    {1, 1, 0},
                    {0, 1, 1}
                }
            },
            {
                Shapes.ReverseL,
                new byte[2, 3]
                {
                    {1, 0, 0},
                    {1, 1, 1}
                }
            }
        };
        
        
        // The constructor takes a shape enum, and generates the definition for the shape
        public Shape(Shapes shapeEnum)
        {
            GenerateShape(shapeEnum);
        }
        
        // The definition of the shape
        public byte[,] State { get; private set; }
        
        // Generate the correct state for the given shape
        private void GenerateShape(Shapes newShapeEnum)
        {

            
            var definitionRequired = Definitions[newShapeEnum];
            
            // Initialize the state with the same dimensions
            State = new byte[definitionRequired.GetLength(0), definitionRequired.GetLength(1)];

            for (int i = 0; i < definitionRequired.GetLength(0); i++)
            {
                for (int j = 0; j < definitionRequired.GetLength(1); j++)
                {
                    State[i, j] = definitionRequired[i, j];
                }
            }
        }
    }
}