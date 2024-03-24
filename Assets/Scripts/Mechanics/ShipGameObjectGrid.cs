using UnityEngine;
using System;

namespace Mechanics
{
    public class ShipGameObjectGrid
    {
        public readonly int Size;
        public string shipName;
        private GameObject[,] _grid;
        public GameObject[,] Grid
        {
            get { return _grid; } //will this return all objects in the array?
        }

        public ShipGameObjectGrid(string name, int size, GameObject initialGameObject)
        {
            shipName = name;
            if (size % 2 == 0)
                throw new ArgumentException("Size must be an odd number.");
            Size = size;
            _grid = new GameObject[Size, Size];
            int centerIndex = Size / 2;
            _grid[centerIndex, centerIndex] = initialGameObject;
        }

        public bool TryAddObject(GameObject gameObject, bool[] directions)
        {
            if (directions.Length != 4)
                throw new ArgumentException("Directions array must have length 4.");
            int centerIndex = Size / 2;
            int[] dx = { -1, 0, 1, 0 }; // up, right, down, left on x
            int[] dy = { 0, 1, 0, -1 }; // up, right, down, left on y
            for (int i = 0; i < 4; i++)
            {
                if (directions[i] && IsInside(centerIndex + dx[i], centerIndex + dy[i]) &&
                    _grid[centerIndex + dx[i], centerIndex + dy[i]] == null)
                {
                    _grid[centerIndex + dx[i], centerIndex + dy[i]] = gameObject;
                    return true;
                }
            }

            return false;
        }

        // checks if (x, y) is inside the grid
        private bool IsInside(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Size && y < Size;
        }
    }
}
