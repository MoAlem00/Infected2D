using System;
using System.Collections;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(AnimateHeart());
    }

    private IEnumerator AnimateHeart()
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
