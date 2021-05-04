﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private float movespeedf;

    //initializing direction to start moving to the left
    private bool facingLeft = true;
    private Vector3 userDirection = Vector3.left;
    private float prevSlope;
    
    //variable to see if the pigeon has just changed directions
    private bool justFlipped = false;
    private bool onBuilding;
    private AudioSource pigeonCoo;

    private BoxCollider2D boxcollider2D;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Vector2[] minMaxX;
    private int minIndex;
    private int maxIndex;
    private int index;
    [SerializeField] private float speed;


    void Start()
    {
        onBuilding = false;
        boxcollider2D = transform.GetComponent<BoxCollider2D>();
        pigeonCoo = GetComponent<AudioSource>();

        movespeedf = Random.Range(0.05f, 0.5f);

        adjustVolume();
        pigeonCoo.Play();
    }

    void flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        userDirection = userDirection * -1;
    }

    // Update is called once per frame
    void Update()
    {
        adjustVolume();
        
        if (onBuilding)
        { 
            transform.position = Vector2.MoveTowards(transform.position, minMaxX[index], Time.deltaTime * speed);
            
            if (transform.position.x == minMaxX[maxIndex].x)
            {
                flip();
                index = minIndex;
                Debug.Log("maxIndex reached. max_index: " + maxIndex + ", minIndex: " + minIndex);
            }
            else if (transform.position.x == minMaxX[minIndex].x)
            {
                flip();
                index = maxIndex;
                Debug.Log("minIndex reached: " + maxIndex);
            }
            /*
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1f, Ground);
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            Quaternion slopeAngleQ = Quaternion.Euler(0, 0, Vector2.Angle(hit.normal, Vector2.up));
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeAngleQ, 0.2f);
            
            bool grounded = isGrounded();
            if (justFlipped) {
                if (grounded) {
                    justFlipped = false;
                }
            }
            else if (!grounded || slopeAngle != prevSlope) {
                flip();
                justFlipped = true;
            }
            prevSlope = slopeAngle;
            GetComponent<Rigidbody2D>().velocity = new Vector3(userDirection.x * 2f, GetComponent<Rigidbody2D>().velocity.y);
            //ClimbSlope(new Vector3(userDirection.x * 2f, GetComponent<Rigidbody2D>().velocity.y), Vector2.Angle(hit.normal, Vector2.up));
            
            //GetComponent<Rigidbody2D>().velocity = new Vector3(userDirection.x * 2f, GetComponent<Rigidbody2D>().velocity.y);
            
            //transform.Translate(userDirection * movespeed * Time.deltaTime);
            */
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.down * 5f;
        }
    }

    //freeze game upon the pigeon touching the cat
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Building"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            GameObject buildingGameObject = other.gameObject;

            minMaxX = new Vector2[buildingGameObject.transform.childCount];
            if (minMaxX.Length == 0 || minMaxX.Length % 2 == 1)
            {
                Destroy(gameObject);
                return;
            }

            int i = 0;
            foreach (Transform child in buildingGameObject.transform)
            {
                if (!onBuilding)
                {
                    Vector2 position = new Vector2(child.position.x, transform.position.y);
                    minMaxX[i] = position;
                    i++;
                }
            }
            minMaxX = sort(minMaxX);
            
            
            float pigeonX = transform.position.x;
            for (int j = 0; j < minMaxX.Length; j+= 2)
            {
                Debug.Log("pigeonX: " + pigeonX + ", minMaxX[j].x: " + minMaxX[j].x + ", minMaxX[j+1].x: " + minMaxX[j+1].x);
                if (pigeonX >= minMaxX[j].x && pigeonX <= minMaxX[j+1].x)
                {
                    index = j;
                    maxIndex = index+1;
                    minIndex = index;
                    Debug.Log("initial index: " + index + ", maxIndex: " + maxIndex + ", minIndex: " + minIndex);
                }
            }
            onBuilding = true;
            

/*
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1f, Ground);
            prevSlope = Vector2.Angle(hit.normal, Vector2.up);
            */
        }
        else if (other.CompareTag("Street"))
        {
            Destroy(gameObject);
        }
    }

    Vector2[] sort(Vector2[] arr)
    {
        int len = arr.Length;

        for (int i = 1; i < len; i++) {
            Vector2 val = arr[i];
            int flag = 0;
            for (int j = i - 1; j >= 0 && flag != 1; ) {
               if (val.x < arr[j].x) {
                  arr[j + 1] = arr[j];
                  j--;
                  arr[j + 1] = val;
               }
               else flag = 1;
            }
        }
        return arr;
    }
    
    Vector3 ClimbSlope(Vector3 velocity, float angle)
    {
        
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(angle * Mathf.Deg2Rad) * moveDistance;

        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(angle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
        }
        return velocity;
    }

    bool isGrounded()
    {
        float extraHeight = 0.5f;
        //RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider2D.bounds.center, boxcollider2D.bounds.size/2, 0f, Vector2.down, extraHeight, Ground);
        Color rayColor;

        Vector2 minOrigin = new Vector2(boxcollider2D.bounds.min.x, transform.position.y);
        Vector2 maxOrigin = new Vector2(boxcollider2D.bounds.max.x, transform.position.y);
        Vector2 horizontal = transform.position;

        RaycastHit2D raycastHitMin = Physics2D.Raycast(minOrigin, Vector2.down, boxcollider2D.bounds.extents.y + extraHeight, Ground);
        RaycastHit2D raycastHitMax = Physics2D.Raycast(maxOrigin, Vector2.down, boxcollider2D.bounds.extents.y + 0.05f, Ground);
        //RaycastHit2D raycastHitHorizontal = Physics2D.Raycast(transform.position, Vector2.left, boxcollider2D.bounds.extents.x + extraHeight, Ground);

        if (raycastHitMin.collider != null && raycastHitMax.collider != null )//&& raycastHitHorizontal.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        
        Debug.DrawRay(boxcollider2D.bounds.center + new Vector3(boxcollider2D.bounds.extents.x, 0), Vector2.down * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxcollider2D.bounds.center - new Vector3(boxcollider2D.bounds.extents.x, 0), Vector2.down * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);
        //Debug.DrawRay(transform.position, Vector2.left * (boxcollider2D.bounds.extents.x + 0.05f), rayColor);
        //Debug.DrawRay(boxcollider2D.bounds.center - new Vector3(0, boxcollider2D.bounds.extents.y), Vector2.right * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);

        
        //Debug.DrawRay(boxcollider2D.bounds.center, Vector2.down, rayColor);
        return raycastHitMin.collider != null && raycastHitMax.collider != null; //&& raycastHitHorizontal.collider != null;
    }

    void adjustVolume()
    {
        float volume = Mathf.Abs(transform.position.x - Camera.main.transform.position.x) / -15f + 1f;
        float adjustedVolume = Mathf.Clamp(volume, 0f, 1f);
        pigeonCoo.volume = adjustedVolume;
    }
}
