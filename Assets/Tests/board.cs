using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class board
    {
        // A Test behaves as an ordinary method
        [Test]
        public void detects_bottom_match()
        {
            // Setup the board
            var board = new Board(8, 10);
            
            // Setup the bottom row
            for (int i = 0; i < board.Representation[0].Count; i++)
            {
                board.InsertBlockAt(i, 9);
            }
            
            // Check if the bottom row has a match
            Assert.That(board.GetMatchingRows().Contains(9));
        }
        
        // Can successfully insert the L shape
        [Test]
        public void inserts_l_successfully()
        {
            // Setup the board
            var board = new Board(8, 10);
            
            // Insert the shape
            board.InsertShape(new Shape(Shapes.L));
            
            // Generate the expected layout
            bool[,] template = new bool[10, 8]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {true, false, false, false, false, false, false, false},
                {true, false, false, false, false, false, false, false},
                {true, true, false, false, false, false, false, false}
            };

            bool isCorrect = true;

            var foundErrorAtX = 0;
            var foundErrorAtY = 0;
            
            // Check if we have the same layout
            for (int xIndex = 0; xIndex < 8; xIndex++)
            {
                for (int yIndex = 0; yIndex < 10; yIndex++)
                {
                    if (board.Representation[yIndex][xIndex] != template[yIndex, xIndex])
                    {
                        isCorrect = false;

                        foundErrorAtX = xIndex;
                        foundErrorAtY = yIndex;

                        break;
                    }
                }

                if (!isCorrect)
                    break;
            }
            
            Assert.That(isCorrect, $"Found error at x:{foundErrorAtX}, y:{foundErrorAtY}");
        }

        // Can successfully insert the I shape
        [Test]
        public void inserts_i_successfully_offset_2()
        {
            // Setup the board
            var board = new Board(8, 10);

            // Insert the shape
            board.InsertShape(new Shape(Shapes.Long), 2);

            // Generate the expected layout
            bool[,] template = new bool[10, 8]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, true, false, false, false, false, false},
                {false, false, true, false, false, false, false, false},
                {false, false, true, false, false, false, false, false},
                {false, false, true, false, false, false, false, false}
            };

            bool isCorrect = true;

            var foundErrorAtX = 0;
            var foundErrorAtY = 0;

            // Check if we have the same layout
            for (int xIndex = 0; xIndex < 8; xIndex++)
            {
                for (int yIndex = 0; yIndex < 10; yIndex++)
                {
                    if (board.Representation[yIndex][xIndex] != template[yIndex, xIndex])
                    {
                        isCorrect = false;

                        foundErrorAtX = xIndex;
                        foundErrorAtY = yIndex;

                        break;
                    }
                }

                if (!isCorrect)
                    break;
            }

            Assert.That(isCorrect, $"Found error at x:{foundErrorAtX}, y:{foundErrorAtY}");
        }

        // Can successfully insert the reverse L shape at offset 3
        [Test]
        public void inserts_reverse_l_successfully_offset_3()
        {
            // Setup the board
            var board = new Board(8, 10);

            // Insert the shape
            board.InsertShape(new Shape(Shapes.ReverseL), 3);

            // Generate the expected layout
            bool[,] template = new bool[10, 8]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, true, false, false, false},
                {false, false, false, false, true, false, false, false},
                {false, false, false, true, true, false, false, false}
            };

            bool isCorrect = true;

            var foundErrorAtX = 0;
            var foundErrorAtY = 0;

            // Check if we have the same layout
            for (int xIndex = 0; xIndex < 8; xIndex++)
            {
                for (int yIndex = 0; yIndex < 10; yIndex++)
                {
                    if (board.Representation[yIndex][xIndex] != template[yIndex, xIndex])
                    {
                        isCorrect = false;

                        foundErrorAtX = xIndex;
                        foundErrorAtY = yIndex;

                        break;
                    }
                }

                if (!isCorrect)
                    break;
            }

            Assert.That(isCorrect, $"Found error at x:{foundErrorAtX}, y:{foundErrorAtY}");
        }

        // Can successfully insert the L shape
        [Test]
        public void inserts_reverse_l_successfully_offset_4_stacked_bumpy()
        {
            // Setup the board
            var board = new Board(8, 10);
            
            // Set up the bumpyness
            board.InsertBlockAt(0, 9);
            board.InsertBlockAt(1, 8);
            board.InsertBlockAt(2, 9);
            board.InsertBlockAt(3, 8);
            board.InsertBlockAt(4, 9);
            board.InsertBlockAt(5, 8);
            board.InsertBlockAt(6, 9);
            board.InsertBlockAt(7, 8);

            // Insert the shape
            board.InsertShape(new Shape(Shapes.ReverseL), 4);
            board.InsertShape(new Shape(Shapes.ReverseL), 3);

            // Generate the expected layout
            bool[,] template = new bool[10, 8]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, true, false, false, false},
                {false, false, false, false, true, true, false, false},
                {false, false, false, true, true, true, false, false},
                {false, false, false, false, true, true, false, false},
                {false, true, false, true, false, true, false, true},
                {true, false, true, false, true, false, true, false}
            };

            bool isCorrect = true;

            var foundErrorAtX = 0;
            var foundErrorAtY = 0;

            // Check if we have the same layout
            for (int xIndex = 0; xIndex < 8; xIndex++)
            {
                for (int yIndex = 0; yIndex < 10; yIndex++)
                {
                    if (board.Representation[yIndex][xIndex] != template[yIndex, xIndex])
                    {
                        isCorrect = false;

                        foundErrorAtX = xIndex;
                        foundErrorAtY = yIndex;

                        break;
                    }
                }

                if (!isCorrect)
                    break;
            }

            Assert.That(isCorrect, $"Found error at x:{foundErrorAtX}, y:{foundErrorAtY}");
        }
        
        // Can successfully insert the L shape
        [Test]
        public void inserts_reverse_l_successfully_offset_4_rotated_stacked_bumpy()
        {
            // Setup the board
            var board = new Board(8, 10);
            
            // Set up the bumpyness
            board.InsertBlockAt(0, 9);
            board.InsertBlockAt(1, 8);
            board.InsertBlockAt(2, 9);
            board.InsertBlockAt(3, 8);
            board.InsertBlockAt(4, 9);
            board.InsertBlockAt(5, 8);
            board.InsertBlockAt(6, 9);
            board.InsertBlockAt(7, 8);

            // Insert the shape
            board.InsertShape(new Shape(Shapes.ReverseL, 3), 4);
            board.InsertShape(new Shape(Shapes.ReverseL, 2), 3);

            // Generate the expected layout
            bool[,] template = new bool[10, 8]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, true, true, false, false, false},
                {false, false, false, true, false, false, false, false},
                {false, false, false, true, true, true, true, false},
                {false, true, false, true, false, true, true, true},
                {true, false, true, false, true, false, true, false}
            };

            bool isCorrect = true;

            var foundErrorAtX = 0;
            var foundErrorAtY = 0;

            // Check if we have the same layout
            for (int xIndex = 0; xIndex < 8; xIndex++)
            {
                for (int yIndex = 0; yIndex < 10; yIndex++)
                {
                    if (board.Representation[yIndex][xIndex] != template[yIndex, xIndex])
                    {
                        isCorrect = false;

                        foundErrorAtX = xIndex;
                        foundErrorAtY = yIndex;

                        break;
                    }
                }

                if (!isCorrect)
                    break;
            }

            Assert.That(isCorrect, $"Found error at x:{foundErrorAtX}, y:{foundErrorAtY}");
        }
        
        // Whether the board clears properly
        [Test]
        public void clears_properly()
        {
            // Setup the board
            var board = new Board(8, 10);
            
            // Fill the bottom 2 rows
            board.InsertBlockAt(0, 9);
            board.InsertBlockAt(1, 9);
            board.InsertBlockAt(2, 9);
            board.InsertBlockAt(3, 9);
            board.InsertBlockAt(4, 9);
            board.InsertBlockAt(5, 9);
            board.InsertBlockAt(6, 9);
            board.InsertBlockAt(7, 9);
            
            board.InsertBlockAt(0, 8);
            board.InsertBlockAt(1, 8);
            board.InsertBlockAt(2, 8);
            board.InsertBlockAt(3, 8);
            board.InsertBlockAt(4, 8);
            board.InsertBlockAt(5, 8);
            board.InsertBlockAt(6, 8);
            board.InsertBlockAt(7, 8);
            
            // Spawn two shapes
            board.InsertShape(new Shape(Shapes.L), 0);
            board.InsertShape(new Shape(Shapes.ReverseL), 6);
            
            // Clear the matched rows
            while (board.GetMatchingRows().Length > 0)
            {
                board.ClearRow(board.GetMatchingRows()[0]);
            }
            
            // Generate the expected layout
            bool[,] template = new bool[10, 8]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {true, false, false, false, false, false, false, true},
                {true, false, false, false, false, false, false, true},
                {true, true, false, false, false, false, true, true}
            };

            bool isCorrect = true;

            var foundErrorAtX = 0;
            var foundErrorAtY = 0;

            // Check if we have the same layout
            for (int xIndex = 0; xIndex < 8; xIndex++)
            {
                for (int yIndex = 0; yIndex < 10; yIndex++)
                {
                    if (board.Representation[yIndex][xIndex] != template[yIndex, xIndex])
                    {
                        isCorrect = false;

                        foundErrorAtX = xIndex;
                        foundErrorAtY = yIndex;

                        break;
                    }
                }

                if (!isCorrect)
                    break;
            }

            Assert.That(isCorrect, $"Found error at x:{foundErrorAtX}, y:{foundErrorAtY}");
        }
    }
}
