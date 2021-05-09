using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseSpawn : MonoBehaviour
{
    [SerializeField] private GameObject cheese;
    [SerializeField] private GameObject player;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private LayerMask groundLayer;
    private double lastSpawnX;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", Random.Range(minTime, maxTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        RaycastHit2D onBuilding = Physics2D.Raycast(transform.position, Vector2.down, Camera.main.orthographicSize * 2, groundLayer);
        float spawnY = Random.Range(Camera.main.orthographicSize * 1.3f, player.transform.position.y);
        float spawnX = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
        if (onBuilding && 
            !(onBuilding.collider.gameObject.GetComponent<buildingFall>().cheeseSpawned))
        {
            spawnY = Random.Range(Camera.main.orthographicSize * 1.3f, onBuilding.collider.bounds.max.y);
            Vector3 spawnLoc = new Vector3(spawnX, spawnY, player.transform.position.z);
            Instantiate(cheese, spawnLoc, Quaternion.identity);
            onBuilding.collider.gameObject.GetComponent<buildingFall>().cheeseSpawned = true;
            Invoke("Spawn", Random.Range(minTime, maxTime));
        }
        else
        {
            Invoke("Spawn", Random.Range(minTime / 2f, maxTime / 2f));
        }
        /*
        Vector3 spawnLoc = new Vector3(transform.position.x, spawnY, player.transform.position.z);
        if (lastSpawnX != spawnLoc.x)
        {
            Instantiate(cheese, spawnLoc, Quaternion.identity);
            lastSpawnX = spawnLoc.x;
        } 
        */
        
    }
}
