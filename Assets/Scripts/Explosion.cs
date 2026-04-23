using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Explosion : MonoBehaviour
{

    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private float explosionRadius = 4f;
    [SerializeField] private LayerMask explosionLayers;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private Slider healthBar;
    private WeaponShoot weapon; 
    private HealthComponent barrelHealth;
    
    
    private int playerExplosionDamage = 50;
    private int enemyExplosionDamage = 100;
    private SpawnCollectibles barrelSpawner;
    private bool exploded = false;

    private void Start()
    {
        exploded = false;
        barrelSpawner = GameObject.FindGameObjectWithTag("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
        barrelHealth = GetComponent<HealthComponent>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponShoot>();
        healthBar.maxValue = barrelHealth.maxHealth;
    }

    private void Update()
    {
        if (barrelHealth.health <= 0 && !exploded)
        {
            Explode();
            exploded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            int i =  Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[i], transform.position,0.3f);
            barrelHealth.TakeDamage(weapon.damage);
            healthBar.value = barrelHealth.health;
            Destroy(other.gameObject);
        }

    }

    private void Explode()
    {
        SoundsManager.Instance.PlaySFX(explosionSound,0.5f);
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion,1f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayers);
        foreach (Collider2D coll in colliders)
        {
            HealthComponent health = coll.gameObject.GetComponent<HealthComponent>();
            if (coll.gameObject.CompareTag("Player"))
            {
                if (health != null)
                {
                    health.TakeDamage(playerExplosionDamage);
                }
            }
            else if (health != null)
                health.TakeDamage(enemyExplosionDamage);
        }
        Destroy(gameObject);
        barrelSpawner.SpawnBarrels();
    }
    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }*/
}
