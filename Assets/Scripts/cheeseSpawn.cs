using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseSpawn : MonoBehaviour
{
    [SerializeField] private GameObject cheese;
    [SerializeField] private GameObject player;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Vector3 spawnLoc = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
        Debug.Log("cheese spawn spot: " + spawnLoc);
        Instantiate(cheese, spawnLoc, Quaternion.identity);
        Invoke("Spawn", Random.Range(minTime, maxTime));
    }
}
