using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabsStoreStats : MonoBehaviour
{
    [SerializeField] private GameObject burgerStatsInGO;
    [SerializeField] private GameObject burgerStatsOutGO;
    private Button burgerStatsInButton;
    private Button burgerStatsOutButton;
    private Image burgerStatsInImage;
    private Image burgerStatsOutImage;    
    [SerializeField] private GameObject burgerTransition;
    private AudioSource burgerPop;
    private GameObject[] burgerTransitionChildren;
    private GameObject[] burgerTransitionChildrenReversed;
    [SerializeField] private RectTransform statsCard;
    private Vector3 statsCardHidden;
    private Vector3 statsCardShown;
    private bool enableBurgerStats;
    private bool done;

    
    // Start is called before the first frame update
    void Start()
    {
        burgerPop = GetComponent<AudioSource>();
        burgerTransitionChildren = new GameObject[burgerTransition.transform.childCount];
        burgerTransitionChildrenReversed = new GameObject[burgerTransition.transform.childCount];

        int i = 0;
        int j = burgerTransitionChildrenReversed.Length - 1;
        foreach (Transform child in burgerTransition.transform)
        {
            burgerTransitionChildren[i] = child.gameObject;
            burgerTransitionChildrenReversed[j] = child.gameObject;
            j--;
            i++;
        }
        
        burgerStatsInButton = burgerStatsInGO.GetComponent<Button>();
        burgerStatsInImage = burgerStatsInGO.GetComponent<Image>();
        burgerStatsOutButton = burgerStatsOutGO.GetComponent<Button>();
        burgerStatsOutImage = burgerStatsOutGO.GetComponent<Image>();

        statsCardHidden = new Vector3(0, 1300, 0);
        statsCardShown = Vector3.zero;
        statsCard.anchoredPosition = statsCardHidden;
        enableBurgerStats = false;
        done = true;
    }

    public void burgerStatsInPublic()
    {
        enableBurgerStats = true;
        StartCoroutine(burgerStatsIn());
    }

    IEnumerator burgerStatsIn()
    {
        changeButtonVisibility(burgerStatsInImage, burgerStatsInButton, 0);
        while (!done)
        {
            yield return null;
        }
        burgerPop.Play();
        StartCoroutine(displayBurgerScene());
        while (!done)
        {
            yield return null;
        }
        StartCoroutine(displayBurgerStatsScoreCard());
        while (!done)
        {
            yield return null;
        }
        changeButtonVisibility(burgerStatsOutImage, burgerStatsOutButton, 1);
    }

    public void burgerStatsOutPublic()
    {
        enableBurgerStats = false;
        StartCoroutine(burgerStatsOut());
    }
    public IEnumerator burgerStatsOut()
    {
        changeButtonVisibility(burgerStatsOutImage, burgerStatsOutButton, 0);
        while (!done)
        {
            yield return null;
        }
        StartCoroutine(displayBurgerStatsScoreCard());
        while (!done)
        {
            yield return null;
        }
        burgerPop.Play();
        StartCoroutine(displayBurgerScene());
        while (!done)
        {
            yield return null;
        }
        changeButtonVisibility(burgerStatsInImage, burgerStatsInButton, 1);
    }
    IEnumerator displayBurgerScene()
    {
        done = false;
        GameObject[] array = (enableBurgerStats) ? burgerTransitionChildren : burgerTransitionChildrenReversed;
        foreach (GameObject child in array)
        {
            float timeSet = Random.Range(0.005f, 0.02f);
            float timer = 0f;
            while (timer < timeSet)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            child.SetActive(enableBurgerStats);
        }
        done = true;
    }

    IEnumerator displayBurgerStatsScoreCard()
    {
        done = false;
        float statsBoxTime = 0.8f;
        float statsBoxTimer = 0f;
        Vector3 direction = (enableBurgerStats) ? statsCardShown : statsCardHidden;
        
        while (statsBoxTimer < statsBoxTime)
        {
            statsBoxTimer += Time.deltaTime;
            statsCard.anchoredPosition = Vector3.Lerp(statsCard.anchoredPosition, direction, statsBoxTimer / statsBoxTime);
            yield return null;
        }
        done = true;
    }

    void changeButtonVisibility(Image image, Button button, int enabled)
    {
        button.enabled = (enabled == 1) ? true : false;
        image.enabled = button.enabled;
    }
}
