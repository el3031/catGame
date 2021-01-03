using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetView : MonoBehaviour
{
    public GameObject street;
    float lastx;
    
    // Start is called before the first frame update
    void Start()
    {
        lastx = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("transform.position.x: " + transform.position.x);
        Debug.Log("lastx: " + lastx);
        if (transform.position.x + (float) 2 * street.GetComponent<BoxCollider2D>().size.x * street.transform.localScale.x > lastx)
        {
            Instantiate (street, transform.position, Quaternion.identity);
			lastx += (float) street.GetComponent<BoxCollider2D>().size.x * street.transform.localScale.x;
        }
    }
}
