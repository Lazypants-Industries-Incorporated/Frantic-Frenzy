using System.Collections;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public Sprite[] batterySprites;
    public PowerPlug powerPlug;
    public AudioClip electricSound;
    AudioSource audio;

    private int health = 11;
	private SpriteRenderer spriteRenderer;
	private bool isPlugged = true;
	private bool beingHandled = false;

	void Start ()
	{
        audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if (isPlugged && health < 11 && health >= 0 && !beingHandled) 
		{
			Recharge ();
			StartCoroutine (HandleIt());//kad nebutu insta-recharge
		}
	}

	public void SetPowerState(bool state)
	{
		isPlugged = state;
	}

	public void TakeDamage (int damageValue)
	{
		if (!isPlugged && health > 0) 
		{
            audio.PlayOneShot(electricSound);
            health -= damageValue;
			SwitchSprites ();
		}
    }
	private void Recharge()
	{
        if (health == 0)
        {
            powerPlug.SendMessage("Discharge");
        }
		health += 1;
		SwitchSprites();
	}
	private IEnumerator HandleIt()
	{
		beingHandled = true;
		yield return new WaitForSeconds (1.0f);
		beingHandled = false;
	}
	private void SwitchSprites()
	{
        spriteRenderer.sprite = batterySprites[health == 0 ? 10  : Mathf.Abs(health - 11)];
	}
}
