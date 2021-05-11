using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus100Anim : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 finalDestination;
    SpriteRenderer sprite;
    private float fadeSpeed;
    void Start()
    {
        finalDestination = new Vector2(transform.position.x, 20f);
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(scoreMove());
        fadeSpeed = 2f;
    }

    // Update is called once per frame
    /*
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, finalDestination, 0.01f);
        if (Mathf.Abs(transform.position.y - finalDestination.y) < 0.01f)
        {
            Destroy(this.gameObject);
        }
    }
    */
    
    
    IEnumerator scoreMove()
    {
        //yield return new WaitForSeconds(2f);
        
        float stayDurationTimer = 0.0f;
        float stayDuration = 1f;
        while (stayDurationTimer < stayDuration)
        {
            stayDurationTimer += Time.deltaTime;
            yield return null;
        }

        while (sprite.color.a > 0)
        {
            float spriteOpacity = sprite.color.a - fadeSpeed * Time.deltaTime;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, spriteOpacity);
            transform.position = Vector2.Lerp(transform.position, finalDestination, 0.005f);
            
            yield return null;
        }
        Destroy(this.gameObject);
    }
    
}
