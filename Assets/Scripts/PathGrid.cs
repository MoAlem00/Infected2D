using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathGrid : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private LayerMask unwalkableLayers;
    private Cell[,] cells;
    private Vector3Int origin;
    public int Width {get; private set;}
    public int Height {get; private set;}

    private void Awake()
    {
        BuildGrid();
    }

    private void BuildGrid()
    {
        groundTilemap.CompressBounds();
        origin = new Vector3Int(groundTilemap.cellBounds.xMin, groundTilemap.cellBounds.yMin, 0);
        Width = groundTilemap.cellBounds.size.x;
        Height = groundTilemap.cellBounds.size.y;
        cells = new Cell[Width, Height];
        Vector2 cellCheckSize = groundTilemap.cellSize * 0.8f;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector3Int tileCoords = new Vector3Int(x + origin.x, y + origin.y);
                Vector2 cellCenter = groundTilemap.GetCellCenterWorld(tileCoords);
                Collider2D hit = Physics2D.OverlapBox(cellCenter,cellCheckSize , 0f,unwalkableLayers);
                cells[x,y] = new Cell(x,y,cellCenter,hit==null);
            }
        }
    }

    public Cell GetCellFromWorld(Vector3 worldPosition)
    {
        Vector3Int tileCoords = groundTilemap.WorldToCell(worldPosition);
        int x = tileCoords.x - origin.x;
        int y = tileCoords.y - origin.y;
        return GetCell(x, y);
    }

    public Cell GetCell(int x, int y)
    {
        if(!InsideBounds(x, y)) return null;
        return cells[x,y];
    }

    private bool InsideBounds(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    /*private void OnDrawGizmos()
    {
        if (cells == null) return;
        foreach (var cell in cells)
        {
            if(cell.IsWalkable) Gizmos.color = new Color(0,1,0,0.15f);
            else Gizmos.color = new Color(1,0,0,0.2f);
            Gizmos.DrawCube(cell.WorldPosition, Vector3.one);
        }
        
    }*/
}
