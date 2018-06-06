using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int playerLives;
    public int playerEnergy;
    public int playerHealthPotions;
    public GameObject heart;
    public GameObject energyBolt;
    public GameObject healthPotion;
    public GameObject mainCamera;
    public L1BossAI boss;
    public Text healthPotionsText;
    public AudioClip deathSound;
    public VictoryFade victoryFade;
    public float fadeTime;

    private AudioSource playerAudio;
    private GameObject[] lives;
    private GameObject[] energy;
    private Vector3 cameraPosition;
    private Vector2 heartPosition, energyPosition;
    private bool isDying;
    private bool isFading;

    // Use this for initialization
    void Start()
    {
        isFading = false;
        //Turn off sound from main menu:)
        if (AudioManager.Instance != null)
        {
            AudioManager am = AudioManager.Instance;
            am.created = true;
        }

        playerAudio = GetComponent<AudioSource>();
        isDying = false;
        lives = new GameObject[playerLives];
        energy = new GameObject[playerEnergy];
        cameraPosition = mainCamera.transform.position; //Setting camera position

        PlayerController.healthPotions = playerHealthPotions;
        PlayerController.energy = playerEnergy;
        PlayerController.health = playerLives;

        SpawnHearts();
        SpawnEnergy();
    }

    void Update() //Update that activates first
    {
        ChangeStatsActivity(); // Hearts or Lightnings Visible/Invisible
        Rigidbody2D player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        PolygonCollider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<PolygonCollider2D>();
        if (PlayerController.health <= 0 && !isDying)
        {
            isDying = true;
            playerAudio.clip = deathSound;
            playerAudio.loop = false;
            playerAudio.Play();
            playerCollider.isTrigger = true;
            player.rotation = 90f;
        }
        if (PlayerController.health <= 0 && isDying && !playerAudio.isPlaying)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        if (boss.transform.position.y < player.transform.position.y && !isFading)
        {
            victoryFade.fadeOut();
            isFading = true;
            Invoke("DelayedCredits", fadeTime);
        }
    }

    void LateUpdate() //Update that activates after "Update" method
    {
        ChangeStatsPosition();
    }

    private void DelayedCredits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    void ChangeStatsActivity()
    {
        for (int i = 0; i < playerLives; i++)
        {
            if (PlayerController.health < (i + 1))
            {
                lives[i].SetActive(false);
            }
            else
                lives[i].SetActive(true);
        }

        for (int i = 0; i < playerEnergy; i++)
        {
            if (PlayerController.energy < (i + 1))
            {
                energy[i].SetActive(false);
            }
            else
                energy[i].SetActive(true);
        }

        healthPotionsText.text = PlayerController.healthPotions.ToString();
    }

    void ChangeStatsPosition()
    {
        if (mainCamera.transform.position != cameraPosition) //If camera position changed
        {
            for (int i = 0; i < playerLives; i++)
            {
                lives[i].transform.position += mainCamera.transform.position - cameraPosition;
            }

            for (int i = 0; i < playerEnergy; i++)
            {
                energy[i].transform.position += mainCamera.transform.position - cameraPosition;
            }

            healthPotion.transform.position += mainCamera.transform.position - cameraPosition;

            cameraPosition = mainCamera.transform.position;
        }
    }

    void SpawnHearts()
    {
        //Spawning health hearts
        heartPosition = heart.transform.position;
        heartPosition.x = -7f;
        heart.transform.position = heartPosition;
        for (int i = 0; i < playerLives; i++)
        {
            lives[i] = Instantiate(heart); //heart, heart.transform);
            heartPosition.x += 0.7f;
            heart.transform.position = heartPosition; //heartTransform.position = heartPosition;
        }
    }

    void SpawnEnergy()
    {
        //Spawning energy
        energyPosition = energyBolt.transform.position;
        energyPosition.x = -7f;
        energyPosition.y = heartPosition.y - 0.8f;
        energyBolt.transform.position = energyPosition;
        for (int i = 0; i < playerEnergy; i++)
        {
            energy[i] = Instantiate(energyBolt);
            energyPosition.x += 0.7f;
            energyBolt.transform.position = energyPosition;
        }
    }
}
