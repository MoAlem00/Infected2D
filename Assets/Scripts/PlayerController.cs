using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//script that handles player controller(movement, dead, sounds)
public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed = 4f;
    public float maxMoveSpeed = 10f;
    
    private PlayerAnimations anim;
    private Rigidbody2D rb;
    private Vector2 input;
    private HealthComponent health;
    private bool isDead;
    private float stepsDelay = 0.45f;
    private AudioSource playerAudioSource;
    private float hitSoundCooldown = 0.2f;
    private float nextHitSound;
    private Coroutine moveCoroutine;
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
        health = GetComponent<HealthComponent>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (health.health <= 0 && !isDead)//if player health reach 0 -> die
        {
            HandleDeath();
            return;
        }
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();
        anim.ResetAnimations();
        if (input != Vector2.zero && !isDead) //if player is moving 
        {
            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(WalkStepsEffect());//play walk sounds
            }
        }
        else//if player not moving
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);//stop walk sounds
                moveCoroutine = null;
            }
        }
        //update movement animations
        if (input.y > 0)
            anim.MoveUp();
        else if (input.y < 0)
            anim.MoveDown();
        else if (input.x < 0)
            anim.MoveLeft();
        else if (input.x > 0)
            anim.MoveRight();
        
    }

    private void HandleDeath()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject zombie in zombies)
        {
            AudioSource audio = zombie.GetComponent<AudioSource>();
            if (audio != null)
                SoundsManager.Instance.StopSoundsEffects(audio);
        }
        SoundsManager.Instance.PlaySFX(deathSound,0.7f);
        moveSpeed = 0;
        anim.Dead();
        isDead = true;
        moveCoroutine = null;
        gameManager.ShowLosePanel();
    }

    //when player got hit with axe i play sound from here,
    //before i played it from axe script but when many axes hit player,
    //many sounds will play and i cant control it there because axe will be destroyed.
    public void TryPlayHitSound()
    {
        if (Time.time >= nextHitSound)
        {
            SoundsManager.Instance.PlaySFX(hitSound,0.5f);
            nextHitSound = Time.time + hitSoundCooldown;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = input * moveSpeed;
    }
    
    private IEnumerator WalkStepsEffect()//coroutine to play walk sounds while player is moving
    {
        while (input != Vector2.zero && !isDead)//while moving play the walk sound
        {
            //audio source that plays only walk sounds so it dont get interrupted by other sound effects  
            playerAudioSource.PlayOneShot(walkSound, 0.1f);
            yield return new WaitForSeconds(stepsDelay);
        }
        moveCoroutine = null;
    }
    
    public void UpgradeSpeed(float upgradeAmount)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + upgradeAmount, 4, maxMoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
