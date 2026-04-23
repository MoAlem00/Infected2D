using System.Collections;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(AnimateObject());
    }

    private IEnumerator AnimateObject()
    {
        while (gameObject != null)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
