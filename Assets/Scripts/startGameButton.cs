using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startGameButton : MonoBehaviour
{
    private Button startButton;
    [SerializeField] private Button quit;
    [SerializeField] private string nextScene;
    [SerializeField] private Animator startTransitionAnim;
    [SerializeField] private Animator fromGameOverAnim;

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
        startButton = GetComponent<Button>();
        quit.onClick.AddListener(quitGame);
        startButton.onClick.AddListener(startGame);
    }

    void quitGame()
    {
        PlayerPrefs.SetString("lastScene", "quit");
        Application.Quit(0);
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

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("lastScene", "quit");
    }
}
