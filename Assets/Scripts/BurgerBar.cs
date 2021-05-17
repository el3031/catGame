using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerBar : MonoBehaviour
{
    [SerializeField] private Image beef;
    [SerializeField] private Image cheese;
    [SerializeField] private Image bread;
    [SerializeField] private Image tomato;
    [SerializeField] private Image onion;
    [SerializeField] private Image lettuce;

    private Color ungathered;
    private Color gathered;

    public static bool beefGathered;
    public static bool cheeseGathered;
    public static bool breadGathered;
    public static bool tomatoGathered;
    public static bool lettuceGathered;
    public static bool onionGathered;

    [SerializeField] private GameObject cheeseSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        ungathered = new Color(255, 255, 255, 0.3f);
        gathered = new Color(255, 255, 255, 1f);
        StartCoroutine(reset());
    }

    public void ingredientGathered(string tag)
    {
        switch (tag)
        {
            case "Cheese":
                PlayerPrefs.SetInt("cheeseTotal", PlayerPrefs.GetInt("cheeseTotal")+1);
                cheeseGathered = true;
                cheese.color = gathered;
                break;
            case "Beef":
                PlayerPrefs.SetInt("beefTotal", PlayerPrefs.GetInt("beefTotal")+1);
                beefGathered = true;
                beef.color = gathered;
                break;
            case "Tomato":
                PlayerPrefs.SetInt("tomatoTotal", PlayerPrefs.GetInt("tomatoTotal")+1);
                tomatoGathered = true;
                tomato.color = gathered;
                break;
            case "Bread":
                PlayerPrefs.SetInt("breadTotal", PlayerPrefs.GetInt("breadTotal")+1);
                breadGathered = true;
                bread.color = gathered;
                break;
            case "Lettuce":
                PlayerPrefs.SetInt("lettuceTotal", PlayerPrefs.GetInt("lettuceTotal")+1);
                lettuceGathered = true;
                lettuce.color = gathered;
                break;
            case "Onion":
                PlayerPrefs.SetInt("onionTotal", PlayerPrefs.GetInt("onionTotal")+1);
                onionGathered = true;
                onion.color = gathered;
                break;
        }
        if (cheeseGathered && lettuceGathered && beefGathered && 
            tomatoGathered && breadGathered && onionGathered)
        {
            float waitTime = 1f;
            float currentTime = 0f;
            while (currentTime < waitTime)
            {
                currentTime += Time.deltaTime;
            }
            cheeseSpawn.GetComponent<cheeseSpawn>().spawnBurger();
            Debug.Log("burger spawned");
            StartCoroutine(reset());
        }
    }
    
    IEnumerator reset()
    {
        tomatoGathered = false;
        breadGathered = false;
        beefGathered = false;
        cheeseGathered = false;
        lettuceGathered = false;
        onionGathered = false;

        float waitTime = 1f;
        float currentTime = 0f;
        while (currentTime < waitTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        cheese.color = ungathered;
        tomato.color = ungathered;
        lettuce.color = ungathered;
        bread.color = ungathered;
        beef.color = ungathered;
        onion.color = ungathered;
    }
}
