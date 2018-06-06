using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    public GameObject mainCamera; //To lock camera on object

    private Animator anim;
    private AudioSource audioSource;
    private GameObject fence;
    private Vector3 startCameraPosition, newCameraPosition;
    private Timer cameraTimer; //To return camera to start position
    private float startCameraSize;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        fence = GameObject.Find("Fence");
        startCameraSize = mainCamera.GetComponent<Camera>().orthographicSize;

        //Camera lock timer
        cameraTimer = new Timer();
        cameraTimer.Interval = 1500;
        cameraTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        cameraTimer.Enabled = true;
        cameraTimer.Stop();
        newCameraPosition.z = -10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel" || collision.gameObject.tag == "Player")
        {
            anim.SetBool("isTriggered", true);
            audioSource.Play();
            fence.SendMessage("Disappear");
            CameraFollow.lockCamera = 1; //Lock camera
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel" || collision.gameObject.tag == "Player")
        {
            anim.SetBool("isTriggered", false);
            audioSource.Play();
            fence.SendMessage("Appear");
            CameraFollow.lockCamera = 1; //Lock camera
        }
    }

    // Return camera to start position
    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        cameraTimer.Stop();
        CameraFollow.lockCamera = 2;
    }

    private void LateUpdate() //For camera locking
    {
        if (CameraFollow.lockCamera == 1) //1 is for locking
        {
            CameraFollow.lockCamera = 3; //Just to prevent repeating actions above
            startCameraPosition = mainCamera.transform.position;
            newCameraPosition.x = -3.9f;
            newCameraPosition.y = -29.62f;

            mainCamera.transform.position = newCameraPosition;
            mainCamera.GetComponent<Camera>().orthographicSize = 2.6f;
            /*bool stop = false;
            while (!stop)
            {
                newCameraPosition = Vector3.Lerp(newCameraPosition, new Vector3(0f, 0f, -10f), Time.deltaTime * speed);
                if (newCameraPosition == mainCamera.transform.position)
                    stop = true;
                mainCamera.transform.position = newCameraPosition;
            }*/
            cameraTimer.Start();
        }
        else if (CameraFollow.lockCamera == 2) //2 is for releasing
        {
            mainCamera.transform.position = startCameraPosition;
            mainCamera.GetComponent<Camera>().orthographicSize = startCameraSize;
            CameraFollow.lockCamera = 0; //Camera not locked
        }
    }
}
