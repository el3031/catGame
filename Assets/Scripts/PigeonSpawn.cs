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

    public float maxSpeed = 0.5f;
    public float minSpeed = 0.6f;

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
        
        if (pigeonSpawnLeft && pigeonSpawnRight &&
            pigeonSpawnLeft.collider.gameObject == pigeonSpawnRight.collider.gameObject &&
            Mathf.Abs(buildingSlope) < 1f && 
            !(pigeonSpawnLeft.collider.gameObject.GetComponent<buildingFall>().pigeonSpawned))
        {
            lastSpawnBuildingLoc = pigeonSpawnLeft.collider.gameObject;
            GameObject instantiatedPigeon = (GameObject) Instantiate(pigeon, spawnLoc, Quaternion.identity);
            instantiatedPigeon.GetComponent<PigeonMovement>().minSpeed = (minSpeed += 0.01f);
            instantiatedPigeon.GetComponent<PigeonMovement>().maxSpeed = (maxSpeed += 0.01f);
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
