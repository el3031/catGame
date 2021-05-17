using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] foods;
    [SerializeField] private GameObject player;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private LayerMask groundLayer;
    private double lastSpawnX;
    private int lastFoodSpawn;
    private bool needSpawnBurger;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", Random.Range(minTime, maxTime));
        needSpawnBurger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        int spawnIndex;
        do
        {
            spawnIndex = Random.Range(0, foods.Length-1);
        } while (spawnIndex == lastFoodSpawn);
        
        RaycastHit2D onBuilding = Physics2D.Raycast(transform.position, Vector2.down, Camera.main.orthographicSize * 2, groundLayer);
        float spawnY = Random.Range(Camera.main.orthographicSize * 1.3f, player.transform.position.y);
        float spawnX = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
        if (onBuilding && !needSpawnBurger &&
            !(onBuilding.collider.gameObject.GetComponent<buildingFall>().cheeseSpawned))
        {
            spawnY = Random.Range(Camera.main.orthographicSize * 1.3f, onBuilding.collider.bounds.max.y);
            Vector3 spawnLoc = new Vector3(spawnX, spawnY, player.transform.position.z);
            Instantiate(foods[spawnIndex], spawnLoc, Quaternion.identity);
            lastFoodSpawn = spawnIndex;
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

    public void spawnBurger()
    {
        needSpawnBurger = true;
        int spawnIndex = foods.Length - 1;
        RaycastHit2D onBuilding = Physics2D.Raycast(transform.position, Vector2.down, Camera.main.orthographicSize * 2, groundLayer);
        float spawnY = Random.Range(Camera.main.orthographicSize * 1.3f, player.transform.position.y);
        float spawnX = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
        if (onBuilding &&
            !(onBuilding.collider.gameObject.GetComponent<buildingFall>().cheeseSpawned))
        {
            spawnY = Random.Range(Camera.main.orthographicSize * 1.3f, onBuilding.collider.bounds.max.y);
            Vector3 spawnLoc = new Vector3(spawnX, spawnY, player.transform.position.z);
            Instantiate(foods[spawnIndex], spawnLoc, Quaternion.identity);
            onBuilding.collider.gameObject.GetComponent<buildingFall>().cheeseSpawned = true;
            needSpawnBurger = false;
        }
        else
        {
            Invoke("spawnBurger", Random.Range(minTime / 4f, maxTime / 4f));
        }
    }
}
