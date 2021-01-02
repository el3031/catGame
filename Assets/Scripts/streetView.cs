using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetView : MonoBehaviour
{
    private GameObject street;
    float lastx = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > lastx + 0.02f)
        {
            Instantiate (street, transform.position, Quaternion.identity);
			lastx = transform.position.x;
        }
    }
}
