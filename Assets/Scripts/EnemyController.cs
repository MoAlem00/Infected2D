using System.Collections;
using UnityEngine;

//scripts that handles Enemy states(chase,patrol), movement, death, animations, shoot and sounds 
public enum EnemyState
{
    Patrol,
    Chase
}
public class EnemyController : MonoBehaviour
{
    private Animator anim;
    private WaveManager waveManager;
    private Transform player;
    private AudioSource audioSource;
    private HealthComponent health;
    private Collider2D enemyCollider;
    
    private bool hasDied = false;
    private float nextFireTime;
    private Vector2 currentPoint;
    private float moveSpeed = 1f;
    private float chaseSpeed = 3f;
    private float chaseRadius = 40f;
    private float attackRadius = 10f;
    private float fireRate = 3f;
    
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioClip[] zombieSounds;
    [SerializeField] private AudioClip[] deathSounds;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private AudioClip throwAxeSound;
    [SerializeField] private GameObject coinPrefab;
    
    EnemyState state = EnemyState.Patrol;
    
    private void Start()
    {
        enemyCollider =  GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        health = GetComponent<HealthComponent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        
        patrolPoints = waveManager.patrolPoints;//Get patrol points from the WaveManager
        if (patrolPoints.Length > 0)
        {
            //pick a random point to start the patrol to it
            var randomIndex = Random.Range(0, patrolPoints.Length);
            var point = patrolPoints[randomIndex].position;
            currentPoint = point;
        }
        else
        {
            Debug.Log("no patrol points");
        }
        StartCoroutine(PlayZombieSound());//start coroutine that plays zombie sounds randomly
    }

    // Update is called once per frame
    private void Update()
    {
        if (hasDied)
            return;
        if (health.health <= 0 && !hasDied)//if enemy health reach 0 -> die
        {
            EnemyDead();
            return;
        }
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

    private void EnemyDead()//handles enemy death
    {
        enemyCollider.enabled = false;
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Destroy(coin, 10f);
        int i  = Random.Range(0, deathSounds.Length);
        SoundsManager.Instance.PlaySFX(deathSounds[i],0.7f);//play random death sound
        audioSource.Stop();//stop zombie sounds
        anim.SetTrigger("isDead");//death animation
        waveManager.enemyKilled++;//tells wave manager to add +1 on enemy killed
        waveManager.totalEnemiesKilled++;//tells wave manager to add +1 on total Enemies killed
        UIManager.Instance.SetEnemiesText(waveManager.waveSize - waveManager.enemyKilled);//update enemies left
        hasDied = true;
        Destroy(gameObject,2f);
    }
    
    //*** i didnt use (up and down) animations ***//

    private void ResetAnimations()
    {
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveLeft", false);
        /*anim.SetBool("MoveUp", false);
        anim.SetBool("MoveDown", false);*/
    }

    //for chase animations we check player position to know which direction should enemy face
    private void SetAnimationsToChase()
    {
        if (player.position.x > transform.position.x)
            anim.SetBool("MoveRight", true);
        else if(player.position.x < transform.position.x)
            anim.SetBool("MoveLeft", true);
        /*else if(player.position.y >= transform.position.y)
            anim.SetBool("MoveUp", true);
        else if(player.position.y <= transform.position.y)
            anim.SetBool("MoveDown", true);*/
    }
    //for patrol animations we check patrol point position to know which direction should enemy face
    private void SetAnimationsToPatrol()
    {
        if (currentPoint.x > transform.position.x)
            anim.SetBool("MoveRight", true);
        else if(currentPoint.x < transform.position.x)
            anim.SetBool("MoveLeft", true);
        /*else if(currentPoint.y >= transform.position.y)
            anim.SetBool("MoveUp", true);
        else if(currentPoint.y <= transform.position.y)
            anim.SetBool("MoveDown", true);*/
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
        Instantiate(axePrefab, shootPoint.position, Quaternion.identity);
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
    
}
