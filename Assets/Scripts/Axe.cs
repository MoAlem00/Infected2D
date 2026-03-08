using UnityEngine;

public class Axe : MonoBehaviour
{
    public float speed;
    private Animator anim;
    private Transform target;
    private Vector3 dir;
    public int damage = 10;
    public GameObject bloodEffect;
    public GameObject sparkEffect;
    public AudioClip[] hitSounds;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 2f);
        anim.SetTrigger("AxeAttack");
        target = GameObject.FindWithTag("Player").transform; //getting the player transform to shoot at the player position.
        dir = target.position - transform.position;//calculate the direction to the player.
    }

    private void Update()
    {
        transform.Translate(dir.normalized * (speed * Time.deltaTime));//moving the axe.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
                playerController.TryPlayHitSound(); //for not playing the hit sound many times.
            GameObject blood = Instantiate(bloodEffect, transform.position, transform.rotation); //blood effect.
            Destroy(blood, 1f);
            speed = 0;
            anim.SetTrigger("Land"); //land animation to stop spinning.
            transform.SetParent(other.transform);//making it a child of the player so it stick on it.
            transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.3f, other.transform.position.z);//to stick the axe on the player's head.
            HealthComponent healthComponent = other.GetComponent<HealthComponent>();
            if (healthComponent != null)
                healthComponent.TakeDamage(damage);//dealing damage to the player.
            //Debug.Log($"Hit {other.name}");
            Destroy(gameObject, 1f);//destroy it after 1s of sticking on the player.
        }

        if (other.CompareTag("Metal"))
        {
            GameObject spark = Instantiate(sparkEffect, transform.position, transform.rotation);
            Destroy(spark,0.3f);
            int rand = Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[rand], transform.position,0.7f);
            Destroy(gameObject);
        }

        if (other.CompareTag("Buildings"))
        {
            Destroy(gameObject);
        }
    }
}
