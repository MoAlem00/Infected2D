using UnityEngine;

//script to rotate weapon to the mouse position
public class WeaponRotation : MonoBehaviour
{
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//convert mouse position from screen space to world space
        mousePos.z = 0f; //set z to 0 because its 2D

        Vector3 direction = mousePos - transform.position;//get the direction vector from object pos to mouse pos
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//get the angle in radians then convert it to degrees
        if (angle > 90 || angle < -90) //flip the sprite
            transform.localScale = new Vector3(originalScale.x, -originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        
        transform.rotation = Quaternion.Euler(0f, 0f, angle);//rotate weapon to face the mouse
    }
}
