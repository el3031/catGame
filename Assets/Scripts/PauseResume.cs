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
        PlayerPrefs.SetInt("Restart", 1);
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
        }
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
    }

    public void quit()
    {
        Application.Quit(0);
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
        Color color = image.color;
        color.a = enabled;
        image.color = color;
        button.enabled = (enabled == 1) ? true : false;
    }
}
