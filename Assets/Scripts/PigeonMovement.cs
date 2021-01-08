using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private float movespeed = 1f;

    //initializing direction to start moving to the left
    private bool facingLeft = true;
    private Vector3 userDirection = Vector3.left;
    
    //variable to see if the pigeon has just changed directions
    private bool justFlipped = false;


    private BoxCollider2D boxcollider2D;
    [SerializeField] private LayerMask Ground;


    void Start()
    {
        boxcollider2D = transform.GetComponent<BoxCollider2D>();

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
        bool grounded = isGrounded();
        if (justFlipped) {
            if (grounded) {
                justFlipped = false;
            }
        }
        else if (!grounded) {
            flip();
            justFlipped = true;
        }
        transform.Translate(userDirection * movespeed * Time.deltaTime);
        
        /*
        if (numMoves == maxMoves) {
            numMoves = 0;
            flip();
            if (facingLeft) {
                userDirection = Vector3.left;
            }
            else {
                userDirection = Vector3.right;
            }
        }
        numMoves = numMoves + 1;
        transform.Translate(userDirection * movespeed * Time.deltaTime);*/
    }

    //freeze game upon the pigeon touching the cat

    void OnTriggerStay2D(Collider2D other) {
    }

    void OnTriggerExit2D(Collider2D other) {
    }

    bool isGrounded()
    {
        float extraHeight = 0.5f;
        //RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider2D.bounds.center, boxcollider2D.bounds.size/2, 0f, Vector2.down, extraHeight, Ground);
        Color rayColor;

        Vector2 minOrigin = new Vector2(boxcollider2D.bounds.min.x, transform.position.y);
        Vector2 maxOrigin = new Vector2(boxcollider2D.bounds.max.x, transform.position.y);

        RaycastHit2D raycastHitMin = Physics2D.Raycast(minOrigin, Vector2.down, boxcollider2D.bounds.extents.y + extraHeight, Ground);
        RaycastHit2D raycastHitMax = Physics2D.Raycast(maxOrigin, Vector2.down, boxcollider2D.bounds.extents.y + extraHeight, Ground);


        if (raycastHitMin.collider != null && raycastHitMax.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        
        Debug.DrawRay(boxcollider2D.bounds.center + new Vector3(boxcollider2D.bounds.extents.x, 0), Vector2.down * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxcollider2D.bounds.center - new Vector3(boxcollider2D.bounds.extents.x, 0), Vector2.down * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);
        //Debug.DrawRay(boxcollider2D.bounds.center - new Vector3(0, boxcollider2D.bounds.extents.y), Vector2.right * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);

        
        //Debug.DrawRay(boxcollider2D.bounds.center, Vector2.down, rayColor);
        return raycastHitMin.collider != null && raycastHitMax.collider != null;
    }
}
