using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startGameButton : MonoBehaviour
{
    private Button startButton;
    [SerializeField] private string nextScene;

    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(startGame);
    }

    void startGame()
    {
        SceneManager.LoadScene(nextScene);
        //StartCoroutine(toStartingSequence());
    }

    IEnumerator toStartingSequence()
    {
        
        yield return null;
    }
}
