using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseResume : MonoBehaviour
{
    // Start is called before the first frame update
    private bool paused = false;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Animator gameOverAnim;

    void Start()
    {
        Color resumeButtonColor = resumeButton.GetComponent<Image>().color;
        resumeButtonColor.a = 0;
        resumeButton.GetComponent<Image>().color = resumeButtonColor;

        Color mainMenuButtonColor = resumeButton.GetComponent<Image>().color;
        mainMenuButtonColor.a = 0;
        mainMenuButton.GetComponent<Image>().color = mainMenuButtonColor;

        mainMenuButton.GetComponent<Button>().enabled = false;
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
        Time.timeScale = 0;
        paused = true;
        pauseMenu.SetActive(true);
        
        Color pauseButtonColor = pauseButton.GetComponent<Image>().color;
        pauseButtonColor.a = 0;
        pauseButton.GetComponent<Image>().color = pauseButtonColor;

        Color resumeButtonColor = resumeButton.GetComponent<Image>().color;
        resumeButtonColor.a = 1;
        resumeButton.GetComponent<Image>().color = resumeButtonColor;

        Color mainMenuButtonColor = resumeButton.GetComponent<Image>().color;
        mainMenuButtonColor.a = 1;
        mainMenuButton.GetComponent<Image>().color = mainMenuButtonColor;

        mainMenuButton.GetComponent<Button>().enabled = true;
        
    }

    public void resume()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
        
        mainMenuButton.GetComponent<Button>().enabled = false;

        Color pauseButtonColor = pauseButton.GetComponent<Image>().color;
        pauseButtonColor.a = 1;
        pauseButton.GetComponent<Image>().color = pauseButtonColor;

        Color resumeButtonColor = resumeButton.GetComponent<Image>().color;
        resumeButtonColor.a = 0;
        resumeButton.GetComponent<Image>().color = resumeButtonColor;
        
        Color mainMenuButtonColor = resumeButton.GetComponent<Image>().color;
        mainMenuButtonColor.a = 0;
        mainMenuButton.GetComponent<Image>().color = mainMenuButtonColor;
        
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
}
