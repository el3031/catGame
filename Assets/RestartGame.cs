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
    private Vector3 scoreCardHidden;

    private int score;
    
    void Start()
    {
        //change later!
        score = PlayerPrefs.GetInt("Score");
        scoreGUI.horizontalOverflow = HorizontalWrapMode.Overflow;
        scoreCardHidden = new Vector3(0, 10f, 0);
        //restart.Text = "Restart";
        //quit.Text = "Quit";
        restart.onClick.AddListener(RestartOnClick);
        quit.onClick.AddListener(Quit);
    }
    
    void FixedUpdate()
    {
        Vector3 scoreBoardPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        scoreCard.transform.position = Vector3.Lerp(scoreCard.transform.position, scoreBoardPosition, 0.125f);
    }
    
    void OnGUI()
    {    
        scoreGUI.text = score.ToString();
        
        /*
        if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2 +150,100,40),"Retry?"))
        {
            StartCoroutine(LoadScene());
        }
        if(GUI.Button(new Rect(Screen.width/2-50,Screen.height/2 +200,100,40),"Quit"))
        {
            Application.Quit();
        }*/
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
        scoreCard.transform.position = Vector3.Lerp(scoreCard.transform.position, scoreCardHidden, 0.125f);
        restartGameAnim.SetTrigger("restart");
        PlayerPrefs.SetInt("Restart", 1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextScene);
    }
}
