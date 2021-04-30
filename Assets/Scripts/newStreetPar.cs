using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newStreetPar : MonoBehaviour
{
    [SerializeField] private Transform mainCameraPosition;
    [SerializeField] private float backgroundMoveSpeed;
    [SerializeField] private float directionX;
    [SerializeField] private float offsetByX = 13f;
    [SerializeField] private GameObject otherStreet;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxis("Horizontal") * backgroundMoveSpeed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + directionX, transform.position.y);

        if (transform.position.x - mainCameraPosition.position.x < -offsetByX)
        {
            transform.position = new Vector2(otherStreet.transform.position.x + offsetByX, transform.position.y);
        }
        else if (transform.position.x - mainCameraPosition.position.x > offsetByX)
        {
            transform.position = new Vector2(otherStreet.transform.position.x + offsetByX, transform.position.y);
        }
    }
}
