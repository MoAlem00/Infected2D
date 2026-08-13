using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

//scripts that handles Enemy states(chase,patrol), movement, death, animations, shoot and sounds 
public enum EnemyState
{
    Patrol,
    Chase
}
public class Enemy : PooledBehaviour
{
    
    private Animator anim;
    private Transform player;
    private AudioSource audioSource;
    private HealthComponent health;
    private Collider2D enemyCollider;
    private EnemySpawner spawner;
    
    private bool hasDied;
    private float nextFireTime;
    private Vector2 currentPoint;
    private float moveSpeed = 1f;
    private float chaseSpeed = 3f;
    private float chaseRadius = 40f;
    private float attackRadius = 10f;
    private float fireRate = 3f;
    
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioClip[] zombieSounds;
    [SerializeField] private AudioClip[] deathSounds;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private AudioClip throwAxeSound;
    [SerializeField] private GameObject coinPrefab;
    
    public static event Action OnEnemyDead;
    
    EnemyState state = EnemyState.Patrol;

    private void Awake()
    {
        enemyCollider =  GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        health = GetComponent<HealthComponent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (health.health <= 0 && !hasDied)//if enemy health reach 0 -> die
        {
            StartCoroutine(EnemyDead());
            return;
        }
        if (hasDied)
            return;
        //calculate distance to player to decide what should the enemy do 
        float distanceToPlayer = Vector3.Distance(transform.position,player.position);
        if (distanceToPlayer <= chaseRadius)//if distance <= chase radius
        {
            state = EnemyState.Chase;//chase player
        }
        else
        {
            state = EnemyState.Patrol;//patrol between points
        }

        switch (state)
        {
            case EnemyState.Chase:
                ResetAnimations();
                SetAnimationsToChase();
                //Move enemy towards the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
                //shoot if player inside attack range and cooldown is ready
                if (distanceToPlayer <= attackRadius && Time.time >= nextFireTime)
                {
                    SoundsManager.Instance.PlaySFX(throwAxeSound, 0.5f);
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
        }
    }

    private IEnumerator EnemyDead()//handles enemy death
    {
        OnEnemyDead?.Invoke();
        enemyCollider.enabled = false;
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Destroy(coin, 10f);
        int i  = Random.Range(0, deathSounds.Length);
        SoundsManager.Instance.PlaySFX(deathSounds[i],0.7f);//play random death sound
        audioSource.Stop();//stop zombie sounds
        anim.SetTrigger("isDead");//death animation
        hasDied = true;
        yield return new WaitForSeconds(2f);
        Despawn();
    }
    

    private void ResetAnimations()
    {
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveLeft", false);
    }

    //for chase animations we check player position to know which direction should enemy face
    private void SetAnimationsToChase()
    {
        if (player.position.x > transform.position.x)
            anim.SetBool("MoveRight", true);
        else if(player.position.x < transform.position.x)
            anim.SetBool("MoveLeft", true);
    }
    //for patrol animations we check patrol point position to know which direction should enemy face
    private void SetAnimationsToPatrol()
    {
        if (currentPoint.x > transform.position.x)
            anim.SetBool("MoveRight", true);
        else if(currentPoint.x < transform.position.x)
            anim.SetBool("MoveLeft", true);
    }
    
    
    private void Patrol()//handles patrol logic
    {
        ResetAnimations();
        SetAnimationsToPatrol();
        //move enemy toward current patrol point
        transform.position = Vector3.MoveTowards(transform.position, currentPoint, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentPoint) < 0.1f)//if enemy reach to the current point
        {
            currentPoint = patrolPoints[Random.Range(0, patrolPoints.Length)].position;//choose another random point
        }
    }

    private void Shoot()//spawn axe
    { 
        //Instantiate(axePrefab, shootPoint.position, Quaternion.identity);
        spawner.AxePooler.GetPooledObject(shootPoint.position,Quaternion.identity);
    }

    private IEnumerator PlayZombieSound()//plays zombie sounds randomly while alive
    {
        while (!hasDied)
        {
            int i = Random.Range(0, zombieSounds.Length);
            audioSource.PlayOneShot(zombieSounds[i],0.3f);
            yield return new WaitForSeconds(zombieSounds[i].length + 0.5f);
        }
    }

    public override void OnSpawned()
    {
        hasDied = false;
        enemyCollider.enabled = true;
        health.ResetHealth();
        PickRandomPoint();
        ResetAnimations();
        SetAnimationsToPatrol();
        StartCoroutine(PlayZombieSound());//start coroutine that plays zombie sounds randomly
    }

    public override void OnDespawned()
    {
        hasDied = true;
    }

    private void PickRandomPoint()
    {
        patrolPoints = spawner.SpawnPoints;
        if(patrolPoints.Length <= 0) return;
        var randomIndex = Random.Range(0, patrolPoints.Length);
        var point = patrolPoints[randomIndex].position;
        currentPoint = point;
    }
}
