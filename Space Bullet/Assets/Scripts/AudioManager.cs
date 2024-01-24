using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] private AudioClip backgroundMusic;
    private static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
        else{
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            Destroy(gameObject);
        }
    }
}
