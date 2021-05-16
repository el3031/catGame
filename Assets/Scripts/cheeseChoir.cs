using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseChoir : MonoBehaviour
{
    private AudioSource choirSound;
    // Start is called before the first frame update
    void Start()
    {
        choirSound = GetComponent<AudioSource>();
        choirSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < Camera.main.rect.xMax - 2f)
        {
            choirSound.Play();
        }
    }
}
