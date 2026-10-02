using System;
using UnityEngine;

public class FlowFieldController : MonoBehaviour
{
    [SerializeField] private PathGrid pathGrid;

    [SerializeField] private Transform player;
    private FlowField flowField;

    private Cell lastPlayerCell;
    
    void Start()
    {
        flowField = new FlowField(pathGrid);
    }

   
    void Update()
    {
        var currentPlayerCell = pathGrid.GetCellFromWorld(player.position);
        if(currentPlayerCell == null) return;
        if(currentPlayerCell == lastPlayerCell) return;
        lastPlayerCell = currentPlayerCell;
        flowField.Calculate(currentPlayerCell);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if(flowField == null) return;
        for (int x = 0; x < pathGrid.Width; x++)
        {
            for (int y = 0; y < pathGrid.Height; y++)
            {
                Cell cell = pathGrid.GetCell(x, y);
                Vector2 dir = flowField.GetDirection(cell);
                if(dir ==  Vector2.zero) continue;
                Vector2 pos = cell.WorldPosition;
                Vector2 endPoint = pos + dir * 0.4f;
                Gizmos.DrawLine(pos,endPoint );
                Gizmos.DrawSphere(endPoint, 0.05f);
            }
        }
    }
}
