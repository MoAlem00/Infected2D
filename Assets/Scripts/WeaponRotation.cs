using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90) //flip the sprite
            transform.localScale = new Vector3(originalScale.x, -1 * originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(originalScale.x, 1 * originalScale.y, originalScale.z);
            

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
