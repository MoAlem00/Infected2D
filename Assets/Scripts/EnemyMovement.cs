using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private FlowFieldController navigator;
    private float speed;
    private bool isChasing;
    public Vector2 Direction { get;private set; }
    
    private void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(navigator == null) return;
        if (!isChasing) return;
        Direction = navigator.GetDirection(rb.position);
        rb.linearVelocity =  Direction * speed;
    }

    public void SetFlowField(FlowFieldController flowFieldController)
    {
        navigator = flowFieldController;
    }

    public void ChasePlayer(float speed)
    {
        this.speed = speed;
        isChasing = true;
    }
    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
        isChasing = false;
    }
    
    
}
