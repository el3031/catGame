using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class statsCard : MonoBehaviour
{
    [SerializeField] private Text burgerTotal;
    [SerializeField] private Text lettuceTotal;
    [SerializeField] private Text tomatoTotal;
    [SerializeField] private Text beefTotal;
    [SerializeField] private Text cheeseTotal;
    [SerializeField] private Text onionTotal;
    [SerializeField] private Text breadTotal;
    [SerializeField] private Text highScore;
    
    // Start is called before the first frame update
    void Start()
    {
        cheeseTotal.text = PlayerPrefs.GetInt("cheeseTotal", 0).ToString();
        tomatoTotal.text = PlayerPrefs.GetInt("tomatoTotal", 0).ToString();
        lettuceTotal.text = PlayerPrefs.GetInt("lettuceTotal", 0).ToString();
        onionTotal.text = PlayerPrefs.GetInt("onionTotal", 0).ToString();
        beefTotal.text = PlayerPrefs.GetInt("beefTotal", 0).ToString();
        breadTotal.text = PlayerPrefs.GetInt("breadTotal", 0).ToString();
        burgerTotal.text = PlayerPrefs.GetInt("burgerTotal", 0).ToString();
        highScore.text = PlayerPrefs.GetInt("highScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
