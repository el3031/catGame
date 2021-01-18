using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource bgMusic;
    public bool stop = false;
    
    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
        bgMusic.Play();
    }

    void FixedUpdate()
    {
        if (stop)
        {
            bgMusic.Stop();
        }
    }
}
