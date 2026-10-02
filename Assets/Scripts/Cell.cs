

using UnityEngine;

public class Cell
{
     public int GridX { get; }
     public int GridY { get; }
     public bool IsWalkable {get; private set;}
     public Vector2 WorldPosition { get; }

     public Cell(int gridX, int gridY, Vector2 worldPosition, bool isWalkable)
     {
          GridX = gridX;
          GridY = gridY;
          WorldPosition = worldPosition;
          IsWalkable = isWalkable;
     }
}
