using UnityEngine;

public class PowerPlug : MonoBehaviour
{

	public GameObject player;
	public GameObject battery;

	public Sprite pluggedSprite;
	public Sprite unpluggedSprite;

	public bool isPlugged = true;
	public GameObject enemyClone;

    public BatteryDischarge batteryDischarge;
    public float dischargeCooldown;

    private Animator anim;
    private float lastDischarged;

    public AudioClip plugInSound;
    public AudioClip plugOutSound;
    public AudioClip dischargeSound;
    AudioSource audio;
    private AudioSource checkIfPlaying;
    private int count = 0;

    void Start ()
	{
        checkIfPlaying = GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
        lastDischarged = -dischargeCooldown;
		anim = GetComponent<Animator> ();
		enemyClone = GameObject.FindGameObjectWithTag ("EnemyClone");
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.E) && Vector2.Distance (transform.position, player.transform.position) < 1) 
		{
			if (anim.GetBool ("isPlugged") == true && Time.time > lastDischarged + dischargeCooldown) 
			{
                audio.PlayOneShot(plugOutSound);
                anim.SetBool ("isPlugged", false);
				battery.SendMessage ("SetPowerState", false);
				isPlugged = false;
                count = 0;

            } 
			else 
			{
                audio.PlayOneShot(plugInSound);
                anim.SetBool ("isPlugged", true);
				battery.SendMessage ("SetPowerState", true);
				isPlugged = true;
			}
		}
		else if(Vector2.Distance (transform.position, enemyClone.transform.position) < 1)
		{
            if (count == 0)
            {
                if (!checkIfPlaying.isPlaying)
                    audio.PlayOneShot(plugInSound);
            }
            count = 1;
            anim.SetBool ("isPlugged", true);
			battery.SendMessage ("SetPowerState", true);
			isPlugged = true;
		}
	}

    void Discharge()
    {      
        Instantiate(batteryDischarge, new Vector3(transform.position.x, transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y, transform.position.z), transform.rotation);
        audio.PlayOneShot(dischargeSound);
        lastDischarged = Time.time;
    }
}
