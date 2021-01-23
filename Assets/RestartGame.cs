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
    [SerializeField] private GameObject scoreCard;
    [SerializeField] private Transform mainCameraPosition;
    [SerializeField] private Button restart;
    [SerializeField] private Button quit;
    [SerializeField] private GameObject player;
    private Vector3 scoreCardHidden;
    private Vector3 scoreCardShown;

    private int score;
    
    void Start()
    {
        //change later!
        score = PlayerPrefs.GetInt("Score");
        scoreGUI.horizontalOverflow = HorizontalWrapMode.Overflow;
        scoreCardHidden = new Vector3(0, 10f, 0);
        scoreCardShown = Vector3.zero;
        restart.onClick.AddListener(RestartOnClick);
        quit.onClick.AddListener(Quit);
    }
    
    void FixedUpdate()
    {
        scoreCard.transform.position = Vector3.Lerp(scoreCard.transform.position, scoreCardShown, 0.125f);
    }
    
    void OnGUI()
    {    
        scoreGUI.text = score.ToString();
    }

    void RestartOnClick()
    {
        StartCoroutine(LoadScene());
    }

    void Quit()
    {
        Application.Quit();
    }
    
    IEnumerator LoadScene()
    {
        //Debug.Log(scoreCardHidden);
        //Debug.Log("transform.position: " + scoreCard.transform.position);
        //restartGameAnim.SetTrigger("restart");
        
        float moveDurationTimer = 0.0f;
        float moveDuration = 1.5f;

        while (moveDurationTimer < moveDuration) 
        {
            moveDurationTimer += Time.deltaTime;
            // Lerp using initial value!
            transform.position = Vector2.Lerp(scoreCard.transform.position, scoreCardHidden, moveDurationTimer / moveDuration);
            yield return null;
        }
        PlayerPrefs.SetInt("Restart", 1);
        SceneManager.LoadScene(nextScene);
    }
}
