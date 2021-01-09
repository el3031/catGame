using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSpawn : MonoBehaviour
{
    [SerializeField] private GameObject pigeon;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        RaycastHit2D onBuilding = Physics2D.Raycast(transform.position, Vector2.down, Camera.main.orthographicSize * 2, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * Camera.main.orthographicSize * 2, Color.green);
        Vector3 spawnLoc = new Vector3(transform.position.x, transform.position.y, 0);
        //if (onBuilding.collider != null)
        //{
            Instantiate(pigeon, spawnLoc, Quaternion.identity);
            Invoke("Spawn", Random.Range(minTime, maxTime));
        //}
        //else
        //{
            //Invoke("Spawn", Random.Range(minTime * 2, maxTime * 2));
        //}
    }
}
