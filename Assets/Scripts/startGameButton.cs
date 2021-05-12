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

    [SerializeField] private GameObject muteGO;
    [SerializeField] private GameObject unmuteGO;
    private Button muteButton;
    private Button unmuteButton;
    private Image muteImage;
    private Image unmuteImage;
    private bool isMuted;

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
}
