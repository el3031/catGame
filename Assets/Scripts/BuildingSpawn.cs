using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] buildings;
    [SerializeField] private float maxSpawn = 3f;
    [SerializeField] private float minSpawn = 2f;
    [SerializeField] private GameObject lastConstructedBuilding;
    private float lastBuildingLeft;
    private float lastBuildingRight;
    
    // Start is called before the first frame update
    void Start()
    {
        lastBuildingRight = lastConstructedBuilding.GetComponent<PolygonCollider2D>().bounds.max.x;
        lastBuildingLeft = lastConstructedBuilding.GetComponent<PolygonCollider2D>().bounds.min.x;
        Spawn();
    }

    void Spawn()
    {
        int rand = Random.Range(0, buildings.Length);
        GameObject building = buildings[rand];
        Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject buildingClone = Instantiate(building, spawnLocation, Quaternion.identity);
        float buildingBounds = buildingClone.GetComponent<PolygonCollider2D>().bounds.extents.x;
        float buildingRight = transform.position.x + buildingBounds;
        float buildingLeft = transform.position.x - buildingBounds;


        if (buildingLeft <= lastBuildingRight)
        {
            Destroy(buildingClone);
            Invoke("Spawn", Random.Range(0.3f, 0.5f));
        }
        else
        {
            lastBuildingLeft = buildingLeft;
            lastBuildingRight = buildingRight;
            Invoke("Spawn", Random.Range(minSpawn, maxSpawn));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
