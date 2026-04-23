using UnityEngine;

//script that handles the custom crosshair movement
public class Crosshair : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //convert mouse pos from screen space to world space.
        mousePos.z = 0f;//keep z at 0 because its 2D
        transform.position = mousePos; //move the crosshair to mouse position
    }
}