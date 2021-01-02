using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxSpeed = 3f;
    private bool facingLeft = true;
    private Animator anim;
    private BoxCollider2D boxcollider2D;
    private Vector3 currentEuler;
    private Quaternion newRotation;

    private bool grounded;
    [SerializeField] private LayerMask Ground;
    private float jumpForce = 300f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxcollider2D = transform.GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        //vertical motion
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        grounded = isGrounded();
        anim.SetBool("Ground", grounded);
        
        //horizontal motion
        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector3
                    (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move < 0 && facingLeft || move > 0 && !facingLeft)
        {
            flip();
        }

        if (Mathf.Abs(transform.rotation.z) >= 0.5)
        {
            float x = 1 - transform.rotation.x;
            float y = 1 - transform.rotation.y;
            float z = 1 - transform.rotation.z;
            currentEuler = new Vector3(x, y, z) * Time.deltaTime * 45;

            newRotation.eulerAngles = currentEuler;
            transform.rotation = newRotation;
        }
        
        
        
        /*
        Debug.Log(transform.rotation.z);
        if (Mathf.Abs(transform.rotation.z) >= 0.1)
        {
            Quaternion cancelRotate = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, cancelRotate, Time.deltaTime * 0.1f);
        }*/
    }

    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
    }

    void flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool isGrounded()
    {
        float extraHeight = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider2D.bounds.center, boxcollider2D.bounds.size, 0f, Vector2.down, extraHeight, Ground);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        
        Debug.DrawRay(boxcollider2D.bounds.center + new Vector3(boxcollider2D.bounds.extents.x, 0), Vector2.down * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxcollider2D.bounds.center - new Vector3(boxcollider2D.bounds.extents.x, 0), Vector2.down * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxcollider2D.bounds.center - new Vector3(0, boxcollider2D.bounds.extents.y), Vector2.right * (boxcollider2D.bounds.extents.y + extraHeight), rayColor);

        
        //Debug.DrawRay(boxcollider2D.bounds.center, Vector2.down, rayColor);
        return raycastHit.collider != null;
        
        /*
        Vector2 RayCastCenter = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        
        RaycastHit2D hit = Physics2D.Raycast(RayCastCenter, direction, distance, Ground);
        Debug.DrawRay(RayCastCenter, direction, Color.green);
        if (hit.collider != null)
        {
            return true;
        }
        return false;*/
    }
}
