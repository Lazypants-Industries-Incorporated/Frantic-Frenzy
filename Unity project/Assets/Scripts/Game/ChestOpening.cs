using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpening : MonoBehaviour {

    private Animator animator;
    private AudioSource audioSource;
    private bool opened;

    // Use this for initialization
    void Start () {
        opened = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player") && !opened)
        {
            opened = true;
            animator.Play("ChestOpening");
            audioSource.Play();
            PlayerController.healthPotions++; //+1 potion per chest
        }
    }
}
