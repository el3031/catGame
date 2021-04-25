using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private Animator restartGameAnim;
    [SerializeField] private Text scoreGUI;
    [SerializeField] private RectTransform scoreCard;
    [SerializeField] private Transform mainCameraPosition;
    [SerializeField] private Button restart;
    [SerializeField] private Button mainMenu;
    [SerializeField] private GameObject player;
    private Vector3 scoreCardHidden;
    private Vector3 scoreCardShown;

    private int score;
    
    void Start()
    {
        //change later!
        score = PlayerPrefs.GetInt("Score");
        scoreGUI.horizontalOverflow = HorizontalWrapMode.Overflow;
        scoreCardHidden = new Vector3(0, 1000, 0);
        scoreCardShown = Vector3.zero;
        restart.onClick.AddListener(RestartOnClick);
        mainMenu.onClick.AddListener(toMainMenu);
    }
    
    void FixedUpdate()
    {
        scoreCard.anchoredPosition = Vector3.Lerp(scoreCard.anchoredPosition, scoreCardShown, 0.125f);
    }
    
    void OnGUI()
    {    
        scoreGUI.text = score.ToString();
    }

    void RestartOnClick()
    {
        StartCoroutine(LoadScene());
    }

    void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    IEnumerator LoadScene()
    {
        //Debug.Log(scoreCardHidden);
        //Debug.Log("transform.position: " + scoreCard.transform.position);
        //restartGameAnim.SetTrigger("restart");
        
        float moveDurationTimer = 0.0f;
        float moveDuration = 1f;

        while (moveDurationTimer < moveDuration) 
        {
            moveDurationTimer += Time.deltaTime;
            // Lerp using initial value!
            scoreCard.anchoredPosition = Vector2.Lerp(scoreCard.anchoredPosition, scoreCardHidden, moveDurationTimer / moveDuration);
            yield return null;
        }
        PlayerPrefs.SetInt("Restart", 1);
        SceneManager.LoadScene(nextScene);
    }
}
