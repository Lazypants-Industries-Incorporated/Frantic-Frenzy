using UnityEngine;

public class L1BossAI : MonoBehaviour
{

    public Transform target; //Player transform
    public L1BossIcicles icicles;
    public AudioClip deathSound;
    public AudioClip shotSound;
    public AudioClip takeDmgSound;

    public float speed;
    public float attackRange;
    public float attackDelay;
    public float damageStun;

    private SpriteRenderer sprite;
    private Animator anim;
    private AudioSource aSource;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private float lastX;
    private float lastAttack;
    private float distanceToPlayer;
    private float xDifference;
    private float lastDamaged;
    private int phase;
    private bool isMoving;
    private bool isDying;

    void Start()
    {
        isDying = false;
        phase = 1;
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        xDifference = Mathf.Abs(transform.position.x - target.position.x);
        isMoving = false;

        if (distanceToPlayer <= attackRange && !isDying)
        {
            if (xDifference > sprite.bounds.size.x / 2)
            {
                lastX = transform.position.x;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
                isMoving = true;
            }
            else if (Time.time > lastAttack + attackDelay && Time.time > lastDamaged + damageStun)
            {
                aSource.PlayOneShot(shotSound);
                anim.SetTrigger("AttackTrigger");
                Instantiate(icicles, transform.position, Quaternion.identity);
                lastAttack = Time.time;
            }
        }

        if (!isMoving)
            anim.SetFloat("MoveX", 0);
        else
            anim.SetFloat("MoveX", Mathf.Sign(transform.position.x - lastX));
    }

    void TakeDamage()
    {
        if (Time.time > lastDamaged + damageStun)
        {
            aSource.PlayOneShot(takeDmgSound);
            anim.SetTrigger("PhaseShiftTrigger");
            phase++;
            lastDamaged = Time.time;
            if (phase == 5)
            {
                aSource.clip = deathSound;
                aSource.Play();
                isDying = true;
                cc.isTrigger = true;
                rb.velocity = new Vector2(8f, 8f);
                rb.gravityScale = 2;
                rb.AddTorque(200f);
            }
        }
    }
}
