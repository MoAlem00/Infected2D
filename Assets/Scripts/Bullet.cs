using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage = 20;
    public GameObject bloodEffect;
    public AudioClip hitSound;
    public AudioClip[] metalHitSounds;
    public AudioClip[] rockHitSounds;
    public GameObject bulletHitMetalEffect;

    private void Start()
    {
        if(gameObject != null)
            Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (other.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position,0.3f);
            GameObject blood = Instantiate(bloodEffect, transform.position, transform.rotation);
            Destroy(blood, 1f);
            HealthComponent healthComponent = other.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);
                Debug.Log($"Hit {other.name} with {damage} damage");
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Metal"))
        {
            int i = Random.Range(0, metalHitSounds.Length);
            AudioSource.PlayClipAtPoint(metalHitSounds[i], transform.position,0.3f);
            GameObject fire = Instantiate(bulletHitMetalEffect, transform.position, transform.rotation);
            Destroy(fire, 0.3f);
            Destroy(gameObject);
        }

        if (other.CompareTag("Buildings"))
        {
            int i = Random.Range(0, rockHitSounds.Length);
            AudioSource.PlayClipAtPoint(rockHitSounds[i], transform.position,0.7f);
            Destroy(gameObject);
        }
    }
}