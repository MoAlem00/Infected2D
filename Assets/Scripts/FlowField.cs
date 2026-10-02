
using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
        private readonly PathGrid grid;
        private readonly int[,] distances;
        private readonly Vector2[,] directions;
        private readonly Vector2Int[] offsets;
        private readonly Queue<Cell> queue;

        
        public FlowField(PathGrid grid)
        {
            this.grid = grid;
            distances = new int[grid.Width, grid.Height];
            directions = new Vector2[grid.Width, grid.Height];
            offsets = new[] { new Vector2Int(1,0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1),new Vector2Int(1,1),new Vector2Int(-1, 1),new Vector2Int(1, -1),new Vector2Int(-1, -1) };
            queue = new Queue<Cell>();
        }

        public void Calculate(Cell targetCell)
        {
            queue.Clear();
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    distances[x, y] = -1;
                    directions[x, y] = Vector2.zero;
                }
            }
            distances[targetCell.GridX,targetCell.GridY] = 0;
            queue.Enqueue(targetCell);
            while (queue.Count > 0)
            {
                Cell current = queue.Dequeue();
                foreach (var offset in offsets)
                {
                    var neighbour = grid.GetCell(current.GridX + offset.x,  current.GridY + offset.y);
                    if (neighbour == null) continue;
                    if(distances[neighbour.GridX, neighbour.GridY] != -1) continue;
                    if(!neighbour.IsWalkable) continue;
                    distances[neighbour.GridX,neighbour.GridY] = distances[current.GridX,current.GridY] + 1;
                    queue.Enqueue(neighbour);
                }
            }
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var cell = grid.GetCell(x, y);
                    if(distances[x, y] == -1 || distances[x, y] == 0 ) continue;
                    Cell best = null;
                    int bestDistance = distances[x, y];
                    foreach (var offset in offsets)
                    {
                        var neighbour = grid.GetCell(cell.GridX + offset.x, cell.GridY + offset.y);
                        if (neighbour == null) continue;
                        if(distances[neighbour.GridX, neighbour.GridY] == -1) continue;
                        if(!neighbour.IsWalkable) continue;
                        if (distances[neighbour.GridX, neighbour.GridY] < bestDistance)
                        {
                            best =  neighbour;
                            bestDistance = distances[neighbour.GridX, neighbour.GridY];
                        }
                    }
                    if(best != null)
                        directions[x, y] = (best.WorldPosition - cell.WorldPosition).normalized;
                }
            }
        }
        
        public Vector2 GetDirection(Cell cell)
        {
            if(cell != null)
                return directions[cell.GridX, cell.GridY];
            return Vector2.zero;
        }
        
}
