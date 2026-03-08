using System.Collections;
using UnityEngine;


public enum EnemyState
{
    Patrol,
    Chase,
    IsHurt
}
public class EnemyController : MonoBehaviour
{
    private UIManager uiManager;
    private WaveManager waveManager;
    private Transform player;
    public Transform[] patrolPoints;
    private Vector2 pointA;
    private Vector2 currentPoint;
    public float moveSpeed = 1f;
    public float chaseSpeed = 1.3f;
    public float chaseRadius = 2f;
    public float attackRadius = 10f;
    private Animator anim;
    private int randomA;
    private int randomB;
    public GameObject axePrefab;
    public Transform shootPoint;
    private Vector3 dir;
    public float fireRate = 0.2f;
    private float nextFireTime;
    private HealthComponent health;
    private bool hasDied = false;
    public AudioClip[] zombieSounds;
    public AudioClip[] deathSounds;
    private AudioSource audioSource;
    
    EnemyState state = EnemyState.Patrol;
    
    void Start()
    {
        uiManager = GameObject.Find("HUD").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        patrolPoints = waveManager.patrolPoints;
        health = GetComponent<HealthComponent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        if (patrolPoints.Length > 0)
        {
            randomA = Random.Range(0, patrolPoints.Length);
            pointA = patrolPoints[randomA].position;
            currentPoint = pointA;
        }
        else
        {
            Debug.Log("no patrol points");
        }
        StartCoroutine(PlayZombieSound());
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0 && !hasDied)
        {
            EnemyDead();
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position,player.position);
        if (distanceToPlayer < chaseRadius)
        {
            state = EnemyState.Chase;
        }
        else
        {
            state = EnemyState.Patrol;
        }

        switch (state)
        {
            case EnemyState.Chase:
                ResetAnimations();
                SetAnimationsToChase();
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
                if (distanceToPlayer <= attackRadius && Time.time >= nextFireTime)
                {
                    if(hasDied)
                        return;
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
        }

        
    }

    void EnemyDead()
    {
        int i  = Random.Range(0, deathSounds.Length);
        SoundsManager.Instance.PlaySFX(deathSounds[i],0.7f);
        audioSource.Stop();
        moveSpeed = 0;
        chaseSpeed = 0;
        ResetAnimations();
        anim.SetTrigger("isDead");
        waveManager.enemyKilled++;
        waveManager.totalEnemiesKilled++;
        uiManager.SetEnemiesText(waveManager.waveSize - waveManager.enemyKilled);
        hasDied = true;
        Destroy(gameObject,4f);
    }

    private void ResetAnimations()
    {
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveDown", false);
    }

    private void SetAnimationsToChase()
    {
        if (player.position.x > transform.position.x)
            anim.SetBool("MoveRight", true);
        else if(player.position.x < transform.position.x)
            anim.SetBool("MoveLeft", true);
        else if(player.position.y >= transform.position.y)
            anim.SetBool("MoveUp", true);
        else if(player.position.y <= transform.position.y)
            anim.SetBool("MoveDown", true);
    }
    private void SetAnimationsToPatrol()
    {
        if (currentPoint.x > transform.position.x)
            anim.SetBool("MoveRight", true);
        else if(currentPoint.x < transform.position.x)
            anim.SetBool("MoveLeft", true);
        else if(currentPoint.y >= transform.position.y)
            anim.SetBool("MoveUp", true);
        else if(currentPoint.y <= transform.position.y)
            anim.SetBool("MoveDown", true);
    }
    
    private void Patrol()
    {
        ResetAnimations();
        SetAnimationsToPatrol();
        transform.position = Vector3.MoveTowards(transform.position, currentPoint, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentPoint) < 0.1f)
        {
            currentPoint = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        }
    }

    private void Shoot()
    { 
        Instantiate(axePrefab, shootPoint.position, Quaternion.identity);
    }

    IEnumerator PlayZombieSound()
    {
        while (!hasDied)
        {
            int i = Random.Range(0, zombieSounds.Length);
            audioSource.PlayOneShot(zombieSounds[i],0.5f);
            yield return new WaitForSeconds(zombieSounds[i].length + 0.5f);
        }
    }
}
