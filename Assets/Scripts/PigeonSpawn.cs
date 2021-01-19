using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSpawn : MonoBehaviour
{
    [SerializeField] private GameObject pigeon;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private LayerMask groundLayer;
    public bool canSpawn;
    private int i = 0;

    private double pigeonLength = 0.58397;


    // Start is called before the first frame update
    void Update()
    {
        if (canSpawn && i == 0)
        {
            Spawn();
            i++;
        }
    }

    void Spawn()
    {
        RaycastHit2D onBuilding = Physics2D.Raycast(transform.position, Vector2.down, Camera.main.orthographicSize * 2, groundLayer);

        Vector2 leftMost = new Vector2(transform.position.x - ((float) (pigeonLength / 2)), transform.position.y);
        Vector2 rightMost = new Vector2(transform.position.x + ((float) (pigeonLength / 2)), transform.position.y);


        RaycastHit2D onBuildingLeft = Physics2D.Raycast(leftMost, Vector2.down, Camera.main.orthographicSize*2, groundLayer);
        RaycastHit2D onBuildingRight = Physics2D.Raycast(rightMost, Vector2.down, Camera.main.orthographicSize*2, groundLayer);


        float buildingSlope = Vector2.Angle(onBuilding.normal, Vector2.up);
        //Debug.DrawRay(transform.position, Vector2.down * Camera.main.orthographicSize * 2, Color.green);

        Debug.DrawRay(leftMost, Vector2.down * Camera.main.orthographicSize * 2, Color.green);
        Debug.DrawRay(rightMost, Vector2.down * Camera.main.orthographicSize * 2, Color.green);

        Vector3 spawnLoc = new Vector3(onBuilding.point.x, onBuilding.point.y + 3f, 0);
        //if (onBuilding.collider != null && Mathf.Abs(buildingSlope) < 5f)
        if (onBuildingLeft.collider != null && onBuildingRight.collider != null && Mathf.Abs(buildingSlope) < 5f)
        {
            Instantiate(pigeon, spawnLoc, Quaternion.identity);
            Invoke("Spawn", Random.Range(minTime, maxTime));
        }
        else
        {
            Invoke("Spawn", Random.Range(minTime * 2, maxTime * 2));
        }
    }
}
