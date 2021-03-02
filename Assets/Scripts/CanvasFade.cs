using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CanvasGroup welcomeMessage;
    [SerializeField] private CanvasGroup pigeonMessage;
    [SerializeField] private CanvasGroup score;
    [SerializeField] private GameObject pigeonSpawn;
    [SerializeField] private GameObject gameOverCircle;
    void Start()
    {
        welcomeMessage.alpha = 0;
        pigeonMessage.alpha = 0;
        score.alpha = 0;
        StartCoroutine(FadeInOut(welcomeMessage, pigeonMessage));
    }

    void Update()
    {
        if (gameOverCircle.transform.localScale.x > 1)
        {
            welcomeMessage.alpha = 0f;
            score.alpha = 0f;
            pigeonMessage.alpha = 0f;
        }
    }

    IEnumerator FadeInOut(CanvasGroup firstMessage, CanvasGroup secondMessage)
    {
        bool restarted = false;
        if (PlayerPrefs.GetInt("Restart") == 1)
        {
            yield return new WaitForSeconds(1.5f);
            restarted = true;
        }
        score.alpha = 1f;
        if (!restarted)
        {
            StartCoroutine(FadeText(firstMessage, 0f, 1f, 0.5f));
            yield return new WaitForSeconds(4f);
            StartCoroutine(FadeText(firstMessage, 1f, 0f, 0.5f));
            yield return new WaitForSeconds(2f);
            StartCoroutine(FadeText(secondMessage, 0f, 1f, 0.5f));
            yield return new WaitForSeconds(3f);
            StartCoroutine(FadeText(secondMessage, 1f, 0f, 0.5f));
        }
        pigeonSpawn.GetComponent<PigeonSpawn>().canSpawn = true;
    }

    IEnumerator FadeText(CanvasGroup text, float startAlpha, float endAlpha, float duration)
    {
         var startTime = Time.time;
         var endTime = Time.time + duration;
         var elapsedTime = 0f;
 
         // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
         text.alpha = startAlpha;
         // loop repeatedly until the previously calculated end time
         while (Time.time <= endTime)
         {
             elapsedTime = Time.time - startTime; // update the elapsed time
             var percentage = 1/(duration/elapsedTime); // calculate how far along the timeline we are
             if (startAlpha > endAlpha) // if we are fading out/down 
             {
                  text.alpha = startAlpha - percentage; // calculate the new alpha
             }
             else // if we are fading in/up
             {
                 text.alpha = startAlpha + percentage; // calculate the new alpha
             }
 
             yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }
        text.alpha = endAlpha;
        //yield return new WaitForSeconds(3f);      
        //Debug.Log("faded " + text + " " + "startAlpha: " + startAlpha + "endAlpha: " + endAlpha);
    }
}
