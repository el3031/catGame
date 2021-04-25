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
