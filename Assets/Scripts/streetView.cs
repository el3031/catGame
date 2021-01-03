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
        lastx = -Camera.main.orthographicSize * 9 / 5;
        while (lastx <= 2 * Camera.main.orthographicSize * 9 / 5)
        {
            Instantiate(street, transform.position, Quaternion.identity);
            lastx += street.GetComponent<BoxCollider2D>().size.x * street.transform.localScale.x;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("transform.position.x: " + transform.position.x);
        Debug.Log("lastx: " + lastx);
        if (transform.position.x + (float) 2 * street.transform.position.x > lastx)
        {
            Instantiate (street, transform.position, Quaternion.identity);
			lastx += (float) 2 * street.transform.position.x;
        }
    }
}
