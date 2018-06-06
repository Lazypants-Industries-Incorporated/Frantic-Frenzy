using System.Timers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int health; //To operate during the runtime
    public static int energy; //To operate during the runtime
    public static int healthPotions; //To operate during the runtime

    public float speed;
    public float energyRegenerationDelay;
    public PlayerFireball fireball;
    public int maxHealth;
    public int maxEnergy;
    public AudioClip walking;
    public AudioClip fireballSound;
    public AudioClip potionSound;
    public AudioClip takeDmgSound;
    AudioSource audio;

    private Animator anim;
    private AudioSource walkingSound;
    private Vector2 prevDir;
    private bool playerMoving;
    private Vector2 lastMove;
    private Timer energyRegeneration;

    void Start()
    {

        anim = GetComponent<Animator>();
        walkingSound = GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();

        prevDir = Vector2.down;
        playerMoving = false;
        lastMove = Vector2.down;

        //Energy regeneration timer
        energyRegeneration = new Timer();
        energyRegeneration.Interval = energyRegenerationDelay;
        energyRegeneration.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        energyRegeneration.Enabled = true;
        energyRegeneration.Start();
    }

    void Update()
    {
        if (CameraFollow.lockCamera == 0) //If camera locked
        {
            playerMoving = false;

            //Get input (or lack of it)
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            //Set new player position if the player is moving
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                playerMoving = true;
                lastMove.Set(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                playerMoving = true;
                lastMove.Set(0f, Input.GetAxisRaw("Vertical"));
            }

            //Apply input to animator
            anim.SetFloat("MoveX", horizontal);
            anim.SetFloat("MoveY", vertical);
            anim.SetBool("PlayerMoving", playerMoving);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);

            //Play sound if moving and update position
            if (playerMoving)
            {
                if (!walkingSound.isPlaying)
                {
                    audio.PlayOneShot(walking);
                }
                transform.Translate(horizontal * Time.deltaTime * speed, vertical * Time.deltaTime * speed, 0f);
            }

            //Save facing direction to prevDir (used for firing projectiles)
            Vector2 currentMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            prevDir = (currentMovement.Equals(Vector2.zero)) ? prevDir : currentMovement;

            if (Input.GetButtonDown("Fire1") && energy > 0)
            {
                //Show firing animation
                anim.SetTrigger("AttackTrigger");
                audio.PlayOneShot(fireballSound);

                PlayerFireball newFireball = Instantiate(fireball, transform.position, Quaternion.identity);
                newFireball.Dir(prevDir.normalized);

                //Code to reduce energy goes here
                energy--;
            }

            if (Input.GetButtonDown("Fire2") && energy >= 5)
            {
                //Show firing animation
                anim.SetTrigger("AttackTrigger");
                audio.PlayOneShot(fireballSound);

                //Straight lines
                for (int i = -1; i <= 1; i += 2)
                {
                    Instantiate(fireball, transform.position, Quaternion.identity).Dir(new Vector2(i, 0f).normalized);
                    Instantiate(fireball, transform.position, Quaternion.identity).Dir(new Vector2(0f, i).normalized);
                    Instantiate(fireball, transform.position, Quaternion.identity).Dir(new Vector2(i, i).normalized);
                    Instantiate(fireball, transform.position, Quaternion.identity).Dir(new Vector2(i, -i).normalized);
                }

                //Code to reduce energy goes here
                energy -= 5;
            }

            if (Input.GetKeyDown(KeyCode.F)) //Health potion using?
            {
                if (healthPotions > 0 && health < maxHealth)
                {
                    audio.PlayOneShot(potionSound);
                    healthPotions--;
                    health++; //Health regen
                }
            }
        }
    }

    public void TakeDamage(int damageValue)
    {
        audio.PlayOneShot(takeDmgSound);
        health -= damageValue;
    }


    // Regenerate energy
    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        if (energy < maxEnergy) //It would be good to fix such things just by using singletone
        {
            energy++;
        }
    }

    public void HealUp(int hp)
    {
        if (health + hp <= maxHealth)
            health += hp;
    }
}
