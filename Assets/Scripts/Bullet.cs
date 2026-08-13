using System;
using UnityEngine;
using Random = UnityEngine.Random;

//script that handles bullet movement, hit effects, hit sounds
public class Bullet : PooledBehaviour
{
    private float speed = 10;
    
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip[] metalHitSounds;
    [SerializeField] private AudioClip[] rockHitSounds;
    [SerializeField] private AudioClip[] woodHitSounds;
    [SerializeField] private GameObject bulletHitMetalEffect;
    [SerializeField] private GameObject bulletHitWoodEffect;
    [SerializeField] private GameObject bloodEffect;
    private float bulletTimer;
    private float bulletTime = 3f;
    private int damage;
    private bool hasHit;

    public void SetDamage(int d)
    {
        damage = d;
    }

    private void Update()
    {
        //move the bullet
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
        bulletTimer -= Time.deltaTime;
        if (bulletTimer <= 0f)
            Despawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(hasHit) return;
        //if bullet hits enemy
        if (other.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position,0.3f);//play hit sound for enemy
            GameObject blood = Instantiate(bloodEffect, transform.position, transform.rotation);//make blood effect where the bullet hit
            Destroy(blood, 1f);
            HealthComponent healthComponent = other.GetComponent<HealthComponent>();//getting health reference for enemy
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);//deal damage for enemy
            }
            hasHit = true;
            Despawn();
        }
        //if bullet hits metal
        if (other.CompareTag("Metal"))
        {
            hasHit = true;
            int i = Random.Range(0, metalHitSounds.Length);
            AudioSource.PlayClipAtPoint(metalHitSounds[i], transform.position,0.3f);//play random metal sounds
            GameObject fire = Instantiate(bulletHitMetalEffect, transform.position, transform.rotation);//play hitting metal effect
            Destroy(fire, 0.3f);//destroy the effect
            Despawn();//destroy the bullet
        }
        //if bullet hits buildings
        if (other.CompareTag("Buildings"))
        {
            hasHit = true;
            int i = Random.Range(0, rockHitSounds.Length);
            AudioSource.PlayClipAtPoint(rockHitSounds[i], transform.position,0.7f);//play random concrete sounds
            Despawn();
        }
        //if bullet hit tress
        if (other.CompareTag("Trees"))
        {
            hasHit = true;
            int i = Random.Range(0, woodHitSounds.Length);
            AudioSource.PlayClipAtPoint(woodHitSounds[i], transform.position,0.3f);//play random hitting wood sounds
            GameObject woodEffect = Instantiate(bulletHitWoodEffect, transform.position, transform.rotation);//play hitting wood effect
            Destroy(woodEffect, 0.3f);//destroy effect
            Despawn();//destroy bullet
        }
    }

    public override void OnSpawned()
    {
        hasHit = false;
        bulletTimer = bulletTime;
    }

    public override void OnDespawned()
    {
    }
}