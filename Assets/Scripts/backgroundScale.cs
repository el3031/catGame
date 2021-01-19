using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScale : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    //private Vector2 initialCamera;

    //void Start()
    //{
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //initialCamera = new Vector2(Camera.main.aspect * Camera.main.orthographicSize * 2, Camera.main.orthographicSize * 2);
    //}
    // Start is called before the first frame update
    void Awake() {        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
    
        Debug.Log(transform.localScale);
        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y) { // Landscape (or equal)
            scale.x *= cameraSize.x / spriteSize.x;
        } else { // Portrait
            scale.x *= cameraSize.y / spriteSize.y;
        }
        
        //transform.position = Vector2.zero; // Optional
        transform.localScale = scale;

        //initialCamera = cameraSize;
    }
}
