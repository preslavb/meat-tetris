using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Code
{
    public class Board
    {
        [OdinSerialize]
        [ListDrawerSettings(Expanded = true, IsReadOnly = true)]
        public List<List<bool>> Representation { get; private set; }

        public Board(int sizeX, int sizeY)
        {
            // Initialize the representation matrix of the board
            Representation = new List<List<bool>>(sizeY);

            for (int i = 0; i < sizeY; i++)
            {
                Representation.Insert(i, new List<bool>(sizeX));

                for (int j = 0; j < sizeX; j++)
                {
                    Representation[i].Insert(j, false);
                }
            }
        }
        
        // Clear the bottom row
        public void ClearRow(int rowIndex)
        {
            // Substitute all values with the one above them (basically moving everything down)
            for (int row = rowIndex; row >= 0; row--)
            {
                for (int i = 0; i < Representation[rowIndex].Count; i++)
                {
                    // The value above the current cell (default to 0 if at the top)
                    var valueAbove = row > 0 ? Representation[row - 1][i] : false;
                    
                    // Substitute with the value above
                    Representation[row][i] = valueAbove;
                }
            }
            
        }
        
        // Insert a block at the given coordinate
        public void InsertBlockAt(int x, int y)
        {
            Representation[y][x] = true;
        }

        // Remove a block at the given coordinate
        public void RemoveBlockAt(int x, int y)
        {
            Representation[y][x] = false;
        }
        
        // Get the indeces of the rows which have matches
        public int[] GetMatchingRows()
        {
            List<int> matchingRows = new List<int>(4);
            
            for (int i = 0; i < Representation.Count; i++)
            {
                
                // Track the number of filled boxes in the row
                var filledBoxes = 0;
                
                // Add to the counter for each box in the row
                for (int j = 0; j < Representation[i].Count; j++)
                {
                    filledBoxes += Representation[i][j] ? 1 : 0;
                }
                
                // If the boxes in the row are equal to the possible boxes in the row, that means its full
                if (filledBoxes == Representation[i].Count)
                {
                    // Add the row to the indexes
                    matchingRows.Add(i);
                }
            }
            
            // Return the matching rows as an array of integers
            return matchingRows.ToArray();
        }
        
        // Insert a shape into the world
        public void InsertShape(Shape shapeToInsert, int? columnToInsertAt = null, int rotation = 0)
        {
            
            // Initialize the columnToInsertAt
            var columnToInsertAtSolid = columnToInsertAt ?? 0;
            
            // Check when a collision would occur
            var collisionDetected = false;
            
            // The last vertical index where there was no collision
            var lastVerticalIndexFree = 0;

            while (!collisionDetected)
            {

                for (int xIndex = 0; xIndex < shapeToInsert.Width; xIndex++)
                {
                    for (int yIndex = 0; yIndex < shapeToInsert.Height; yIndex++)
                    {

                        // The piece has reached the end of the board without collisions, so detect a collision automatically with the ground
                        if (shapeToInsert.State[yIndex][xIndex] && lastVerticalIndexFree + yIndex >= Representation.Count)
                        {
                            // Collision was detected
                            collisionDetected = true;
                            lastVerticalIndexFree--;
                        }
                        
                        // Check if the block is taken, and if there is a collision detected with it
                        else if (shapeToInsert.State[yIndex][xIndex] && Representation[lastVerticalIndexFree + yIndex][columnToInsertAtSolid + xIndex])
                        {
                            // Collision was detected
                            collisionDetected = true;
                            lastVerticalIndexFree--;
                        }                        
                    }
                }

                lastVerticalIndexFree += collisionDetected ? 0 : 1;
            }

            if (lastVerticalIndexFree < 0)
            {
                Debug.Log("Cannot Place");
            }

            // We can place it, so do that
            else
            {
                for (int xIndex = 0; xIndex < shapeToInsert.Width; xIndex++)
                {
                    for (int yIndex = 0; yIndex < shapeToInsert.Height; yIndex++)
                    {

                        if (shapeToInsert.State[yIndex][xIndex])
                        {
                            Representation[lastVerticalIndexFree + yIndex][columnToInsertAtSolid + xIndex] = true;
                        }
                    }
                }
            }
        }
    }
}