﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Building") || other.CompareTag("Pigeon"))
        {
            //Debug.Log("Destroyed building");
            Destroy(other.gameObject);
        }
    }
}
