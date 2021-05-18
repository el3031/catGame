using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CanvasGroup welcomeMessage;
    [SerializeField] private CanvasGroup pigeonMessage;
    [SerializeField] private CanvasGroup cheeseMessage;
    [SerializeField] private CanvasGroup score;
    [SerializeField] private GameObject pigeonSpawn;
    [SerializeField] private GameObject gameOverCircle;
    [SerializeField] private GameObject restartCircle;
    [SerializeField] private GameObject trainingIngredients;
    private bool fadeStart = false;
    void Start()
    {
        welcomeMessage.alpha = 0;
        pigeonMessage.alpha = 0;
        score.alpha = 0;
    }

    void Update()
    {
        if ((gameOverCircle.transform.localScale.x > 0 || restartCircle.transform.localScale.x > 0) &&
            !fadeStart)
        {
            welcomeMessage.alpha = 0f;
            score.alpha = 0f;
            pigeonMessage.alpha = 0f;
            cheeseMessage.alpha = 0f;
        }
        else if (gameOverCircle.transform.localScale.x == 0 && 
                 restartCircle.transform.localScale.x == 0 &&
                 !fadeStart)
        {
            fadeStart = true;
            score.alpha = 1f;
            StartCoroutine(FadeInOut());
        }
    }

    IEnumerator FadeInOut()
    {
        if (PlayerPrefs.GetInt("tutorialPlayed") != 1)
        {
            trainingIngredients.SetActive(true);
            StartCoroutine(FadeText(welcomeMessage, 0f, 1f, 0.5f));
            yield return new WaitForSeconds(4f);
            StartCoroutine(FadeText(welcomeMessage, 1f, 0f, 0.5f));
            yield return new WaitForSeconds(2f);
            StartCoroutine(FadeText(cheeseMessage, 0f, 1f, 0.5f));
            yield return new WaitForSeconds(3f);
            StartCoroutine(FadeText(cheeseMessage, 1f, 0f, 0.5f));
            yield return new WaitForSeconds(2f);
            StartCoroutine(FadeText(pigeonMessage, 0f, 1f, 0.5f));
            yield return new WaitForSeconds(3f);
            StartCoroutine(FadeText(pigeonMessage, 1f, 0f, 0.5f));
        }
        else
        {
            trainingIngredients.SetActive(false);
        }
        pigeonSpawn.GetComponent<PigeonSpawn>().canSpawn = true;
        PlayerPrefs.SetInt("tutorialPlayed", 1);
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
