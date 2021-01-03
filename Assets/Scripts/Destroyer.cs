using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Break();
            return;
        }
        if (other.gameObject.transform.parent) 
        {
            Debug.Log("destroyed parent gameobject");
            Destroy (other.gameObject.transform.parent.gameObject);
        } 
        else
        {
            Debug.Log("destroyed object");
            Destroy (other.gameObject);		
        }
    }
}
