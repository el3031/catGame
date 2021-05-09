using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingFall : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    private bool stop;
    public bool pigeonSpawned;
    public bool cheeseSpawned;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stop = false;
        pigeonSpawned = false;
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            rb.velocity = new Vector3(0, -20f, 0);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Street"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            stop = true;
        }
    }
}
