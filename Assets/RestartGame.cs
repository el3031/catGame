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

    private int score;
    
    void Start()
    {
        //change later!
        score = PlayerPrefs.GetInt("Score");
        Vector3 scoreBoardPosition = Camera.main.transform.position;
        scoreCard.transform.position = Vector3.MoveTowards(scoreCard.transform.position, scoreBoardPosition, Time.deltaTime);
    }
    
    void OnGUI()
    {
        scoreGUI.text = score.ToString();
        if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2 +150,100,40),"Retry?"))
        {
            StartCoroutine(LoadScene());
        }
        if(GUI.Button(new Rect(Screen.width/2-50,Screen.height/2 +200,100,40),"Quit"))
        {
            Application.Quit();
        }
    }
    
    IEnumerator LoadScene()
    {
        restartGameAnim.SetTrigger("restart");
        Vector3 outOfView = new Vector3(scoreCard.transform.position.x, Camera.main.orthographicSize / 2 + scoreCard.GetComponent<BoxCollider2D>().bounds.extents.y * 2, scoreCard.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, outOfView, Time.deltaTime);
        PlayerPrefs.SetInt("Restart", 1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextScene);
    }
}
