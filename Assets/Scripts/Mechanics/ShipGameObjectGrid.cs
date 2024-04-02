using UnityEngine;
using System;
using System.Text;
using Content;

namespace Mechanics
{
    public class ShipGameObjectGrid
    {
        public readonly int Size;
        public string shipName;
        private GameObject[,] _grid;
        private int centerIndex;
        public GameObject[,] Grid
        {
            get { return _grid; }
        }

        public ShipGameObjectGrid(string name, int size, GameObject initialGameObject)
        {
            shipName = name;
            if (size % 2 == 0)
                throw new ArgumentException("Size must be an odd number.");
            Size = size;
            _grid = new GameObject[Size, Size];
            centerIndex = Size / 2;
            _grid[centerIndex, centerIndex] = initialGameObject;
        }

        public bool TryAddObject(GameObject gameObject, Vector3 position, bool[] attachPoints)
        {
            //Size = the length size of 2d array
            //CenterIndex = Size / 2 (typecasted to int to remove decimal)
            if (attachPoints.Length != 4)
                throw new ArgumentException("Directions array must have length 4.");
            //change from world position to array position
            int x = Size/2 + (int)position.x;
            int y = Size/2 - (int)position.y; //Y value is reversed in 2d arrays
            if (IsInside(x, y) && !_grid[y,x])
            {
                for (var i = 0; i < 4; i++)
                {
                    if(!attachPoints[i]) //if there is no connection possibility, then skip
                    {
                        continue;
                    }
                    // Calculate checks for neighbors
                    int checkX = x + (i == 1 ? 1 : (i == 3 ? -1 : 0));
                    int checkY = y - (i == 0 ? 1 : (i == 2 ? -1 : 0)); //Y value is reversed in 2d arrays

                    if (!IsInside(checkX, checkY)) //if outside grid, then skip
                    {
                        continue;                  
                    }
                    GameObject neighbor = _grid[checkY, checkX];
                    if (!neighbor) // if nothing beside, then skip
                    {
                        continue;
                    }
                    neighbor.TryGetComponent<Block>(out Block neighborBlock);
                    if (neighborBlock.AttachPoints[(i + 2) % 4])
                    {
                        Grid[y, x] = gameObject;
                        return true;
                    }
                }
            }
            return false;
        }
        
        public string DisplayGrid()
        {
            // Create a StringBuilder instance for efficient string concatenation in loop
            var gridString = new StringBuilder();

            // iterate through all rows
            for(int x = 0; x < _grid.GetLength(0); x++)
            {
                // iterate through all columns in a row
                for(int y = 0; y < _grid.GetLength(1); y++)
                {
                    // if the current cell contains a game object, represent it as 'O',
                    // otherwise as '-'
                    string cell = _grid[x, y] != null ? "O" : "-";

                    // add current cell representation to the grid string
                    gridString.Append(cell + " ");
                }
                // Add newline at the end of each row
                gridString.Append(Environment.NewLine);
            }
            // Return the final grid string
            return gridString.ToString();
        }

        // checks if (x, y) is inside the grid
        private bool IsInside(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Size && y < Size;
        }
    }
}
