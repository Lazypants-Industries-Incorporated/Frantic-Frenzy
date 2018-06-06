using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public bool created = false; //It's for disabling a main menu sound when game scene loads

    public static AudioManager instance = null;
    // Use this for initialization
    void Start()
    {
        created = false;
    }

    public static AudioManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        /*DontDestroyOnLoad(this);
        if (created)
            DestroyObject(gameObject);*/

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(created)
            Destroy(this.gameObject);
    }
}
