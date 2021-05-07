using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSpawn : MonoBehaviour
{
    [SerializeField] private GameObject pigeon;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D pigeonSpawnCollider;

    private float lastSpawnX;
    public bool canSpawn;
    private GameObject lastSpawnBuildingLoc;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnX = transform.position.x;
        pigeonSpawnCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (canSpawn)
        {
            Invoke("Spawn", Random.Range(minTime, maxTime));
            canSpawn = false;
        }
    }

    void Spawn()
    {
        Vector2 pigeonSpawnRayCastLeft = new Vector2(pigeonSpawnCollider.bounds.min.x, transform.position.y);
        Vector2 pigeonSpawnRayCastRight = new Vector2(pigeonSpawnCollider.bounds.max.x, transform.position.y);

        RaycastHit2D pigeonSpawnLeft = Physics2D.Raycast(pigeonSpawnRayCastLeft, Vector2.down, Camera.main.orthographicSize * 2, groundLayer);
        RaycastHit2D pigeonSpawnRight = Physics2D.Raycast(pigeonSpawnRayCastRight, Vector2.down, Camera.main.orthographicSize * 2, groundLayer);

        
        
        RaycastHit2D onBuilding = Physics2D.Raycast(transform.position, Vector2.down, Camera.main.orthographicSize * 2, groundLayer);
        float buildingSlope = Vector2.Angle(onBuilding.normal, Vector2.up);
        
        Debug.DrawRay(pigeonSpawnRayCastLeft, Vector2.down * Camera.main.orthographicSize * 2, Color.green);
        Debug.DrawRay(pigeonSpawnRayCastRight, Vector2.down * Camera.main.orthographicSize * 2, Color.green);


        //Debug.DrawRay(transform.position, Vector2.down * Camera.main.orthographicSize * 2, Color.green);
        Vector3 spawnLoc = new Vector3(onBuilding.point.x, onBuilding.point.y + 3f, 0);
        
        /*
        if (pigeonSpawnLeft.collider != null && pigeonSpawnRight.collider != null &&
            pigeonSpawnLeft.collider == pigeonSpawnRight &&  
            pigeonSpawnLeft.collider.gameObject != lastSpawnBuildingLoc && 
            pigeonSpawnRight.collider.gameObject != lastSpawnBuildingLoc && 
            Mathf.Abs(buildingSlope) < 1f)*/
        /*
        if (Mathf.Abs(spawnLoc.x - lastSpawnX) > 5f &&
            Mathf.Abs(buildingSlope) < 1f &&
            pigeonSpawnLeft.collider != null && pigeonSpawnRight != null &&
            pigeonSpawnLeft.collider.gameObject == pigeonSpawnRight.collider.gameObject)
            */
        if (pigeonSpawnLeft && pigeonSpawnRight &&
            pigeonSpawnLeft.collider.gameObject == pigeonSpawnRight.collider.gameObject &&
            Mathf.Abs(buildingSlope) < 1f && 
            !(pigeonSpawnLeft.collider.gameObject.GetComponent<buildingFall>().pigeonSpawned))
        {
            lastSpawnBuildingLoc = pigeonSpawnLeft.collider.gameObject;
            Instantiate(pigeon, spawnLoc, Quaternion.identity);
            lastSpawnX = spawnLoc.x;
            pigeonSpawnLeft.collider.gameObject.GetComponent<buildingFall>().pigeonSpawned = true;
            Invoke("Spawn", Random.Range(minTime * 1.5f, maxTime * 1.5f));
        }
        else
        {
            Invoke("Spawn", Random.Range(minTime, maxTime));
        }
    }
}
