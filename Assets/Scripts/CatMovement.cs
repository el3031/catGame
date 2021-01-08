using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CatMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxSpeed = 5f;
    private bool facingLeft = true;
    private Animator anim;
    [SerializeField] private Animator gameOverAnim;
    [SerializeField] private string nextScene;
    private BoxCollider2D boxcollider2D;
    private Vector3 currentEuler;
    private Quaternion newRotation;
    private bool grounded;
    [SerializeField] private LayerMask Ground;
    private float jumpForce = 400f;
    private float groundSlopeAngle = 0f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxcollider2D = transform.GetComponent<BoxCollider2D>();
        
        if (PlayerPrefs.GetInt("Restart") == 1)
        {
            gameOverAnim.SetTrigger("restart");
        }
    }

    void FixedUpdate()
    {
        //vertical motion
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        grounded = isGrounded();
        anim.SetBool("Ground", grounded);
        
        //horizontal motion
        float move = Input.GetAxis("Horizontal");
        if (grounded)
        {
            CheckGround(new Vector3(transform.position.x, transform.position.y - (boxcollider2D.size.x / 2) + 0.2f, transform.position.z));
        }
        else
        {
            groundSlopeAngle = 0f;
        }
        Quaternion newAngle = Quaternion.Euler(0, 0, groundSlopeAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, newAngle, 0.5f);

        GetComponent<Rigidbody2D>().velocity = new Vector3
                    (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move < 0 && facingLeft || move > 0 && !facingLeft)
        {
            flip();
        }
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
        float extraHeight = 0.5f;
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

        return raycastHit.collider != null;
    }

    public void CheckGround(Vector3 origin)
    {
        Vector3 groundSlopeDir;
        float startDistanceFromBottom = 0.2f;   // Should probably be higher than skin width
        float sphereCastRadius = 0.25f;
        float sphereCastDistance = 0.75f;
        Vector3 rayOriginOffset1 = new Vector3(-0.2f, 0f, 0.16f);
        Vector3 rayOriginOffset2 = new Vector3(0.2f, 0f, -0.16f);
        float raycastLength = 0.75f;
        bool showDebug = true;
        
        // Out hit point from our cast(s)
        RaycastHit hit;

        // SPHERECAST
        // "Casts a sphere along a ray and returns detailed information on what was hit."
        if (Physics.SphereCast(origin, sphereCastRadius, Vector3.down, out hit, sphereCastDistance, Ground))
        {
            // Angle of our slope (between these two vectors). 
            // A hit normal is at a 90 degree angle from the surface that is collided with (at the point of collision).
            // e.g. On a flat surface, both vectors are facing straight up, so the angle is 0.
            groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);
        }

        // Now that's all fine and dandy, but on edges, corners, etc, we get angle values that we don't want.
        // To correct for this, let's do some raycasts. You could do more raycasts, and check for more
        // edge cases here. There are lots of situations that could pop up, so test and see what gives you trouble.
        RaycastHit slopeHit1;
        RaycastHit slopeHit2;

        // FIRST RAYCAST
        if (Physics.Raycast(origin + rayOriginOffset1, Vector3.down, out slopeHit1, raycastLength))
        {
            // Debug line to first hit point
            if (showDebug) { Debug.DrawLine(origin + rayOriginOffset1, slopeHit1.point, Color.red); }
            // Get angle of slope on hit normal
            float angleOne = Vector3.Angle(slopeHit1.normal, Vector3.up);

            // 2ND RAYCAST
            if (Physics.Raycast(origin + rayOriginOffset2, Vector3.down, out slopeHit2, raycastLength))
            {
                // Debug line to second hit point
                if (showDebug) { Debug.DrawLine(origin + rayOriginOffset2, slopeHit2.point, Color.red); }
                // Get angle of slope of these two hit points.
                float angleTwo = Vector3.Angle(slopeHit2.normal, Vector3.up);
                // 3 collision points: Take the MEDIAN by sorting array and grabbing middle.
                float[] tempArray = new float[] { groundSlopeAngle, angleOne, angleTwo };
                Array.Sort(tempArray);
                groundSlopeAngle = tempArray[1];
            }
            else
            {
                // 2 collision points (sphere and first raycast): AVERAGE the two
                float average = (groundSlopeAngle + angleOne) / 2;
		        groundSlopeAngle = average;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Street") || other.CompareTag("Pigeon"))
        {
            transform.position = Vector3.zero;
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        gameOverAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextScene);
    }
}
