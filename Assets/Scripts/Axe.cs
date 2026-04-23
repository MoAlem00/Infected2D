using UnityEngine;

//script that handles the axe that shot by zombies
public class Axe : MonoBehaviour
{
    private float speed = 5;
    private Animator anim;
    private Transform target;
    private Vector3 dir;
    
    private ParticleSystem spinningEffect;
    
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject sparkEffect;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private int damage = 20;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 2f);//destroy after 2s if it didnt hit anything
        anim.SetTrigger("AxeAttack");//start spinning animation when axe is created
        target = GameObject.FindWithTag("Player").transform; //getting the player transform to shoot at the player position.
        dir = target.position - transform.position;//calculate the direction to the player.
        spinningEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        transform.Translate(dir.normalized * (speed * Time.deltaTime));//moving the axe.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if it hit player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();//get the player reference to play hit sound from there
            if (playerController != null)
                playerController.TryPlayHitSound(); //so player hit sound dont play many times
            GameObject blood = Instantiate(bloodEffect, transform.position, transform.rotation); //blood effect.
            Destroy(blood, 1f);
            speed = 0;//stop moving
            anim.SetTrigger("Land"); //land animation to stop spinning.
            spinningEffect.Stop();//stop particle effect
            transform.SetParent(other.transform);//making it a child of the player so it stick on it.
            transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.3f, other.transform.position.z);//to stick the axe on the player's head.
            HealthComponent healthComponent = other.GetComponent<HealthComponent>();//getting health reference for the player
            if (healthComponent != null)
                healthComponent.TakeDamage(damage);//dealing damage to the player.
            Destroy(gameObject, 1f);//destroy it after 1s of sticking on the player.
        }
        //if it hit metals
        if (other.CompareTag("Metal"))
        {
            GameObject spark = Instantiate(sparkEffect, transform.position, transform.rotation);//make spark effect
            Destroy(spark,0.3f);
            int rand = Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[rand], transform.position,0.7f);//play random metal sound
            Destroy(gameObject);
        }
        //if it hit buildings just destroy
        if (other.CompareTag("Buildings"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Barrel"))
        {
            Destroy(gameObject);
        }
    }
}
