using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startGameButton : MonoBehaviour
{
    private Button startButton;
    [SerializeField] private string nextScene;
    [SerializeField] private Animator startTransitionAnim;
    [SerializeField] private Animator fromGameOverAnim;

    void Awake()
    {
        if (PlayerPrefs.GetInt("Restart") != 1)
        {
            fromGameOverAnim.enabled = false;
        }
    }
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(startGame);
    }

    void startGame()
    {
        StartCoroutine(toStartingSequence());
    }

    IEnumerator toStartingSequence()
    {
        startTransitionAnim.SetTrigger("startGame");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
    }
}
