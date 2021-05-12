using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseResume : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool paused = false;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject quitButton;
    private Image resumeImage;
    private Image mainMenuImage;
    private Image quitImage;
    private Image pauseImage;
    private Button resumeButtonButton;
    private Button pauseButtonButton;
    private Button quitButtonButton;
    private Button mainMenuButtonButton;
    [SerializeField] private Animator gameOverAnim;

    [SerializeField] private GameObject muteGO;
    [SerializeField] private GameObject unmuteGO;
    private Button muteButton;
    private Button unmuteButton;
    private Image muteImage;
    private Image unmuteImage;

    [SerializeField] private Text scoreText;

    void Start()
    {
        pauseImage = pauseButton.GetComponent<Image>();
        resumeImage = resumeButton.GetComponent<Image>();
        mainMenuImage = mainMenuButton.GetComponent<Image>();
        quitImage = quitButton.GetComponent<Image>();

        pauseButtonButton = pauseButton.GetComponent<Button>();
        resumeButtonButton = resumeButton.GetComponent<Button>();
        mainMenuButtonButton = mainMenuButton.GetComponent<Button>();
        quitButtonButton = quitButton.GetComponent<Button>();

        muteButton = muteGO.GetComponent<Button>();
        muteImage = muteGO.GetComponent<Image>();
        muteButton.onClick.AddListener(mute);

        unmuteButton = unmuteGO.GetComponent<Button>();
        unmuteImage = unmuteGO.GetComponent<Image>();
        unmuteButton.onClick.AddListener(unmute);

        changeButtonVisibility(pauseImage, pauseButtonButton, 1);
        changeButtonVisibility(resumeImage, resumeButtonButton, 0);
        changeButtonVisibility(mainMenuImage, mainMenuButtonButton, 0);
        changeButtonVisibility(quitImage, quitButtonButton, 0);
    }
    
    public void LoadMainMenu()
    {
        //PlayerPrefs.SetInt("Restart", 1);
        //SceneManager.LoadScene("MainMenu");
        StartCoroutine(toMainMenu());
        
    }
    IEnumerator toMainMenu()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("lastScene", "startingSequence");
        gameOverAnim.SetTrigger("CircleS2B");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
        Debug.Log("loaded next scene main menu");
    }
    public void pause()
    {
        if (CatMovement.canPause)
        {
            Time.timeScale = 0;
            paused = true;
            pauseMenu.SetActive(true);
            
            changeButtonVisibility(pauseImage, pauseButtonButton, 0);
            changeButtonVisibility(resumeImage, resumeButtonButton, 1);
            changeButtonVisibility(mainMenuImage, mainMenuButtonButton, 1);
            changeButtonVisibility(quitImage, quitButtonButton, 1);

            if (PlayerPrefs.GetInt("mute", 1) == 1)
            {
                changeButtonVisibility(muteImage, muteButton, 0);
                changeButtonVisibility(unmuteImage, unmuteButton, 1);
            }
            else
            {
                changeButtonVisibility(muteImage, muteButton, 1);
                changeButtonVisibility(unmuteImage, unmuteButton, 0);
            }

            scoreText.enabled = false;
        }
    }

    public void mute()
    {
        PlayerPrefs.SetInt("mute", 1);
        changeButtonVisibility(unmuteImage, unmuteButton, 1);
        changeButtonVisibility(muteImage, muteButton, 0);
    }

    public void unmute()
    {
        PlayerPrefs.SetInt("mute", 0);
        changeButtonVisibility(unmuteImage, unmuteButton, 0);
        changeButtonVisibility(muteImage, muteButton, 1);
    }

    public void resume()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
        
        changeButtonVisibility(pauseImage, pauseButtonButton, 1);
        changeButtonVisibility(resumeImage, resumeButtonButton, 0);
        changeButtonVisibility(mainMenuImage, mainMenuButtonButton, 0);
        changeButtonVisibility(quitImage, quitButtonButton, 0);
        changeButtonVisibility(muteImage, muteButton, 0);
        changeButtonVisibility(unmuteImage, unmuteButton, 0);

        scoreText.enabled = true;
    }

    public void quit()
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    void changeButtonVisibility(Image image, Button button, int enabled)
    {
        button.enabled = (enabled == 1) ? true : false;
        image.enabled = button.enabled;
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("lastScene", "quit");
    }
}
