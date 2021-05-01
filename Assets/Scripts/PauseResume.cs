using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{
    // Start is called before the first frame update
    private bool paused = false;
    private Button pauseButton;
    [SerializeField] private GameObject pauseMenu;
    void Start()
    {
        pauseButton = GetComponent<Button>();
    }

    public void pause()
    {
        Time.timeScale = 0;
        paused = true;
        pauseMenu.SetActive(true);
    }

    public void resume()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
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
