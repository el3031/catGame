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
        Spawn();
        lastBuildingRight = lastConstructedBuilding.GetComponent<PolygonCollider2D>().bounds.max.x;
        lastBuildingLeft = lastConstructedBuilding.GetComponent<PolygonCollider2D>().bounds.min.x;

    }

    void Spawn()
    {
        int rand = Random.Range(0, buildings.Length);
        GameObject building = buildings[rand];
        GameObject buildingClone = Instantiate(building, transform.position, Quaternion.identity);
        Debug.Log(building.name);
        float buildingBounds = buildingClone.GetComponent<PolygonCollider2D>().bounds.extents.x;
        float buildingRight = transform.position.x + buildingBounds;
        float buildingLeft = transform.position.x - buildingBounds;
        Debug.Log("buildingLeft " + buildingLeft + ", lastBuildingRight" + lastBuildingRight + ", transform.position.x " + transform.position.x + ", buildingBounds " + buildingClone.GetComponent<PolygonCollider2D>().bounds.extents.x);


        if (buildingLeft <= lastBuildingRight)
        {
            Destroy(buildingClone);
        }
        else
        {
            lastBuildingLeft = buildingLeft;
            lastBuildingRight = buildingRight;
        }
        Invoke("Spawn", Random.Range(minSpawn, maxSpawn));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
