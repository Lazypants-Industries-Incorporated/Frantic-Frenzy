using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public AudioClip audioClipClick;
    public AudioClip audioClipHover;

    private AudioSource audioSource;
    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnMouseEnter() //Mouse HOVER
    {
        GetComponent<TextMesh>().color = Color.red;
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClipHover;
        audioSource.Play();
    }

    private void OnMouseExit() //Mouse Hover cancel
    {
        GetComponent<TextMesh>().color = new Color32(255, 250, 205, 255);
    }

    private void OnMouseDown() //My method
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClipClick;
        audioSource.Play();
        //StartCoroutine(SoundPlay());
        switch (gameObject.name)//transform.name)
        {
            case "Start Text":
                //AudioManager.created = true; //Disable main menu sound
                //Debug.Log("Created " + am.created);
                SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
                break;
            case "Controls Text":
				SceneManager.LoadScene("Controls", LoadSceneMode.Single);
                break;
            case "Credits Text":
                SceneManager.LoadScene("Credits", LoadSceneMode.Single);
                break;
            case "Back Text":
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
            case "Quit Text":
                Application.Quit();
                break;
        }
    }

    /*IEnumerator SoundPlay()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClipClick;
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        //do something
    }*/
}
