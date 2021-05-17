using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseChoir : MonoBehaviour
{
    private AudioSource choirSound;
    private bool canPlay;
    //[SerializeField] private Transform focus;
    // Start is called before the first frame update
    void Start()
    {
        choirSound = GetComponent<AudioSource>();
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < Camera.main.transform.position.x + 8f && canPlay)
        {
            choirSound.Play();
            canPlay = false;
        }
    }
}
