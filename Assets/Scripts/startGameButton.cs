using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class startGameButton : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quit;
    [SerializeField] private string nextScene;
    [SerializeField] private Animator startTransitionAnim;
    [SerializeField] private Animator fromGameOverAnim;

    [SerializeField] private GameObject muteGO;
    [SerializeField] private GameObject unmuteGO;
    private Button muteButton;
    private Button unmuteButton;
    private Image muteImage;
    private Image unmuteImage;
    private bool isMuted;

    [SerializeField] private GameObject[] blackCatArray;
    [SerializeField] private GameObject[] calicoCatArray;
    [SerializeField] private GameObject[] brownCatArray;
    [SerializeField] private GameObject[] orangeCatArray;
    [SerializeField] private GameObject[] checkmarks;
    [SerializeField] private GameObject calicoCatBG;
    [SerializeField] private GameObject blackCatBG;
    [SerializeField] private GameObject brownCatBG;
    [SerializeField] private GameObject orangeCatBG;

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
        changeSkins();
        changeCheckmarks(checkmarks);
        
        quit.onClick.AddListener(quitGame);
        startButton.onClick.AddListener(startGame);

        muteButton = muteGO.GetComponent<Button>();
        muteImage = muteGO.GetComponent<Image>();
        muteButton.onClick.AddListener(mute);

        unmuteButton = unmuteGO.GetComponent<Button>();
        unmuteImage = unmuteGO.GetComponent<Image>();
        unmuteButton.onClick.AddListener(unmute);

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
    
    public void changeSkinToBlackCat()
    {
        PlayerPrefs.SetString("skin", "blackCat");
        changeCheckmarks(checkmarks);
        changeSkins();
    }

    public void changeSkinToCalicoCat()
    {
        PlayerPrefs.SetString("skin", "calicoCat");
        changeCheckmarks(checkmarks);
        changeSkins();
    }

    public void changeSkinToBrownCat()
    {
        PlayerPrefs.SetString("skin", "brownCat");
        changeCheckmarks(checkmarks);
        changeSkins();
    }

    public void changeSkinToOrangeCat()
    {
        PlayerPrefs.SetString("skin", "orangeCat");
        changeCheckmarks(checkmarks);
        changeSkins();
    }

    void changeCheckmarks(GameObject[] checkmarks)
    {
        string skin = PlayerPrefs.GetString("skin");
        foreach (GameObject checkmark in checkmarks)
        {
            checkmark.SetActive(false);
        }
        switch (skin)
        {
            case "calicoCat":
                foreach (Transform child in calicoCatBG.transform)
                {
                    child.gameObject.SetActive(true);
                }
                break;
            case "brownCat":
                foreach (Transform child in brownCatBG.transform)
                {
                    child.gameObject.SetActive(true);
                }
                break;
            case "orangeCat":
                foreach (Transform child in orangeCatBG.transform)
                {
                    child.gameObject.SetActive(true);
                }
                break;
            default:
                foreach (Transform child in blackCatBG.transform)
                {
                    child.gameObject.SetActive(true);
                }
                break;
        }
    }

    void changeSkins()
    {
        string skin = PlayerPrefs.GetString("skin");
        bool brown = false;
        bool black = false;
        bool calico = false;
        bool orange = false;
        
        switch (skin)
        {
            case "calicoCat":
                calico = true;
                break;
            case "brownCat":
                brown = true;
                break;
            case "orangeCat":
                orange = true;
                break;
            default:
                black = true;
                break;
        }

        foreach (GameObject o in brownCatArray)
        {
            o.SetActive(brown);
        }
        foreach (GameObject o in blackCatArray)
        {
            o.SetActive(black);
        }
        foreach (GameObject o in calicoCatArray)
        {
            o.SetActive(calico);
        }
        foreach (GameObject o in orangeCatArray)
        {
            o.SetActive(orange);
        }
    }
}
