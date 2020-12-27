using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 3f;
    bool facingLeft = true;
    Animator anim;

    bool grounded = false;
    public Transform groundDetect;
    float groundRadius = 0.2f;
    public LayerMask groundLayer;
    public float jumpForce = 300f;

    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //vertical motion
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        grounded = Physics2D.OverlapCircle(groundDetect.position, 
                                           groundRadius, groundLayer);
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
    }
    
    // Update is called once per frame
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
}
