using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 3f;
    
    private PlayerAnimations anim;
    private GameManager gameManager;
    private Rigidbody2D rb;
    Vector2 input;
    private HealthComponent health;
    bool isDead;
    public AudioClip walkSound;
    private float stepsDelay = 0.45f;
    private AudioSource playerAudioSource;
    private float hitSoundCooldown = 0.2f;
    private float nextHitSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    Coroutine moveCoroutine;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudioSource = GetComponent<AudioSource>();
        health = GetComponent<HealthComponent>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0 && !isDead)
        {
            SoundsManager.Instance.PlaySFX(deathSound);
            moveSpeed = 0;
            anim.Dead();
            Debug.Log($"Player is dead");
            isDead = true;
            moveCoroutine = null;
            gameManager.ShowLosePanel();
        }
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();
        anim.ResetAnimations();
        if (input != Vector2.zero && !isDead)
        {
            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(WalkStepsEffect());
            }
        }
        else
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
        }
        if (input.y > 0)
        {
            anim.MoveUp();
        }
        else if (input.y < 0)
        {
            anim.MoveDown();
        }
        else if (input.x < 0)
        {
            anim.MoveLeft();
        }
        else if (input.x > 0)
        {
            anim.MoveRight();
        }
        
    }

    public void TryPlayHitSound()
    {
        if (Time.time >= nextHitSound)
        {
            SoundsManager.Instance.PlaySFX(hitSound);
            nextHitSound = Time.time + hitSoundCooldown;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = input * moveSpeed;
    }
    
    IEnumerator WalkStepsEffect()
    {
        while (input != Vector2.zero && !isDead)
        {
            playerAudioSource.PlayOneShot(walkSound, 0.1f);
            yield return new WaitForSeconds(stepsDelay);
        }
        moveCoroutine = null;
    }
}
