using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{
    // Start is called before the first frame update
    private bool paused = false;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
    }
    
    public void pause()
    {
        Time.timeScale = 0;
        paused = true;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

    }

    public void resume()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
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
