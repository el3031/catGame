using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startGameButton : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quit;
    [SerializeField] private string nextScene;
    [SerializeField] private Animator startTransitionAnim;
    [SerializeField] private Animator fromGameOverAnim;
    
    [SerializeField] private Button burgerStats;
    [SerializeField] private GameObject burgerTransition;
    [SerializeField] private RectTransform statsCard;
    [SerializeField] private Text burgerTotal;
    [SerializeField] private Text lettuceTotal;
    [SerializeField] private Text tomatoTotal;
    [SerializeField] private Text beefTotal;
    [SerializeField] private Text cheeseTotal;
    [SerializeField] private Text onionTotal;
    [SerializeField] private Text breadTotal;
    [SerializeField] private Text highScore;
    private Vector3 statsCardHidden;
    private Vector3 statsCardShown;

    [SerializeField] private GameObject muteGO;
    [SerializeField] private GameObject unmuteGO;
    private Button muteButton;
    private Button unmuteButton;
    private Image muteImage;
    private Image unmuteImage;
    private bool isMuted;
    private bool burgerStatsEnabled;
    private bool done;

    void Awake()
    {
        string lastScene = PlayerPrefs.GetString("lastScene", "justStarted");
        
        if (lastScene.Equals("quit") || lastScene.Equals("justStarted"))
        {
            fromGameOverAnim.enabled = false;
        }
    }
    void Start()
    {
        quit.onClick.AddListener(quitGame);
        startButton.onClick.AddListener(startGame);

        muteButton = muteGO.GetComponent<Button>();
        muteImage = muteGO.GetComponent<Image>();
        muteButton.onClick.AddListener(mute);

        unmuteButton = unmuteGO.GetComponent<Button>();
        unmuteImage = unmuteGO.GetComponent<Image>();
        unmuteButton.onClick.AddListener(unmute);

        cheeseTotal.text = PlayerPrefs.GetInt("cheeseTotal", 0).ToString();
        tomatoTotal.text = PlayerPrefs.GetInt("tomatoTotal", 0).ToString();
        lettuceTotal.text = PlayerPrefs.GetInt("lettuceTotal", 0).ToString();
        onionTotal.text = PlayerPrefs.GetInt("onionTotal", 0).ToString();
        beefTotal.text = PlayerPrefs.GetInt("beefTotal", 0).ToString();
        breadTotal.text = PlayerPrefs.GetInt("breadTotal", 0).ToString();
        burgerTotal.text = PlayerPrefs.GetInt("burgerTotal", 0).ToString();
        highScore.text = PlayerPrefs.GetInt("highScore", 0).ToString();

        statsCardHidden = new Vector3(0, 1300, 0);
        statsCardShown = Vector3.zero;
        statsCard.anchoredPosition = statsCardHidden;
        done = true;

        if (PlayerPrefs.GetInt("mute") == 1)
        {
            mute();
        }
        else
        {
            unmute();
        }
    }

    void quitGame()
    {
        PlayerPrefs.SetString("lastScene", "quit");
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE) 
            Application.Quit();
        #elif (UNITY_WEBGL)
            Application.OpenURL("https://gorfmcgorf.itch.io/");
        #endif
    }
    void startGame()
    {
        StartCoroutine(toStartingSequence());
    }

    IEnumerator toStartingSequence()
    {
        PlayerPrefs.SetString("lastScene", "MainMenu");
        startTransitionAnim.SetTrigger("CircleS2B");
        yield return new WaitForSeconds(1f);
        fromGameOverAnim.enabled = true;
        SceneManager.LoadScene(nextScene);
    }

    void mute()
    {
        PlayerPrefs.SetInt("mute", 1);
        changeButtonVisibility(unmuteImage, unmuteButton, 1);
        changeButtonVisibility(muteImage, muteButton, 0);
        /*
        muteImage.transform.position = new Vector3(muteImage.transform.position.x, muteImage.transform.position.y, -1000000);
        unmuteImage.transform.position = new Vector3(unmuteImage.transform.position.x, unmuteImage.transform.position.y, 0);
        */
        isMuted = true;
        changeAudio();
    }

    void unmute()
    {
        PlayerPrefs.SetInt("mute", 0);
        changeButtonVisibility(unmuteImage, unmuteButton, 0);
        changeButtonVisibility(muteImage, muteButton, 1);
        /*
        muteImage.transform.position = new Vector3(muteImage.transform.position.x, muteImage.transform.position.y, 0);
        unmuteImage.transform.position = new Vector3(unmuteImage.transform.position.x, unmuteImage.transform.position.y, -1000000);
        */
        isMuted = false;
        changeAudio();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("lastScene", "quit");
    }

    void changeButtonVisibility(Image image, Button button, int enabled)
    {
        button.enabled = (enabled == 1) ? true : false;
        image.enabled = button.enabled;
    }

    void changeAudio()
    {
        AudioListener.volume = isMuted ? 0.0f : 1.0f;
    }

    public void burgerStatsButton()
    {
        if (burgerStatsEnabled)
        {
            StartCoroutine(burgerStatsOut());
        }
        else
        {
            StartCoroutine(burgerStatsIn());
        }
        burgerStatsEnabled = !burgerStatsEnabled;
    }

    IEnumerator burgerStatsIn()
    {
        
        while (!done)
        {
            yield return null;
        }
        StartCoroutine(displayBurgerScene());
        while (!done)
        {
            yield return null;
        }
        StartCoroutine(displayBurgerStatsScoreCard());
        
        /*
        StartCoroutine(displayBurgerScene());
        yield return new WaitForSeconds(1f);
        StartCoroutine(displayBurgerStatsScoreCard());
        */
    }

    IEnumerator burgerStatsOut()
    {
        while (!done)
        {
            yield return null;
        }
        StartCoroutine(displayBurgerStatsScoreCard());
        while (!done)
        {
            yield return null;
        }
        StartCoroutine(displayBurgerScene());
        
        /*
        displayBurgerStatsScoreCard();
        yield return new WaitForSeconds(1f);
        displayBurgerScene();
        */
    }

    IEnumerator displayBurgerScene()
    {
        done = false;
        foreach (Transform child in burgerTransition.transform)
        {
            float timeSet = Random.Range(0.01f, 0.03f);
            float timer = 0f;
            while (timer < timeSet)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            child.gameObject.SetActive(burgerStatsEnabled);
        }
        done = true;
    }

        
    IEnumerator displayBurgerStatsScoreCard()
    {
        done = false;
        float statsBoxTime = 1f;
        float statsBoxTimer = 0f;
        Vector3 direction = (burgerStatsEnabled) ? statsCardShown : statsCardHidden;
        
        while (statsBoxTimer < statsBoxTime)
        {
            statsBoxTimer += Time.deltaTime;
            Debug.Log(direction);
            statsCard.anchoredPosition = Vector3.Lerp(statsCard.anchoredPosition, direction, statsBoxTimer / statsBoxTime);
            yield return null;
        }
        done = true;
    }
        
}
