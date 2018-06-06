using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDischarge : MonoBehaviour {

    public float lifetime;
    public AudioClip dischargeSound;
    AudioSource audio;

    private Animator anim;
    private float shockTime;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(dischargeSound);
        anim = GetComponent<Animator>();
        shockTime = Time.time;
    }

    void Update () {
        if (Time.time > shockTime + lifetime)
            Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
        {
            collision.SendMessage("TakeDamage");
        }
    }
}
