using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    [SerializeField] private GameObject[] blackCatArray;
    [SerializeField] private GameObject[] calicoCatArray;
    [SerializeField] private GameObject[] brownCatArray;
    [SerializeField] private GameObject[] orangeCatArray;
    [SerializeField] private GameObject[] checkmarks;
    [SerializeField] private GameObject calicoCatBG;
    [SerializeField] private GameObject blackCatBG;
    [SerializeField] private GameObject brownCatBG;
    [SerializeField] private GameObject orangeCatBG;
    private Button calicoCatBGButton;
    private Button orangeCatBGButton;
    private Button brownCatBGButton;

    [SerializeField] private GameObject calicoLocked;
    [SerializeField] private GameObject orangeLocked;
    [SerializeField] private GameObject brownLocked;

    private int highScore;
    
    // Start is called before the first frame update
    void Start()
    {
        changeSkins();
        changeCheckmarks(checkmarks);
        highScore = PlayerPrefs.GetInt("highScore");

        calicoCatBGButton = calicoCatBG.GetComponent<Button>();
        orangeCatBGButton = orangeCatBG.GetComponent<Button>();
        brownCatBGButton = brownCatBG.GetComponent<Button>();
        unlockSkins();
    }

    // Update is called once per frame
    public void changeSkinToBlackCat()
    {
        PlayerPrefs.SetString("skin", "blackCat");
        changeCheckmarks(checkmarks);
        changeSkins();
    }

    public void changeSkinToCalicoCat()
    {
        PlayerPrefs.SetString("skin", "calicoCat");
        changeCheckmarks(checkmarks);
        changeSkins();
    }

    public void changeSkinToBrownCat()
    {
        PlayerPrefs.SetString("skin", "brownCat");
        changeCheckmarks(checkmarks);
        changeSkins();
    }

    public void changeSkinToOrangeCat()
    {
        PlayerPrefs.SetString("skin", "orangeCat");
        changeCheckmarks(checkmarks);
        changeSkins();
    }

    void changeCheckmarks(GameObject[] checkmarks)
    {
        string skin = PlayerPrefs.GetString("skin");
        foreach (GameObject o in checkmarks)
        {
            o.SetActive(false);
        }

        int code;
        switch (skin)
        {
            case "calicoCat":
                code = 1;
                break;
            case "orangeCat":
                code = 2;
                break;
            case "brownCat":
                code = 3;
                break;
            default:
                code = 0;
                break;
        }
        checkmarks[code].SetActive(true);
    }

    void changeSkins()
    {
        string skin = PlayerPrefs.GetString("skin");
        bool brown = false;
        bool black = false;
        bool calico = false;
        bool orange = false;
        
        switch (skin)
        {
            case "calicoCat":
                calico = true;
                break;
            case "brownCat":
                brown = true;
                break;
            case "orangeCat":
                orange = true;
                break;
            default:
                black = true;
                break;
        }

        foreach (GameObject o in brownCatArray)
        {
            o.SetActive(brown);
        }
        foreach (GameObject o in blackCatArray)
        {
            o.SetActive(black);
        }
        foreach (GameObject o in calicoCatArray)
        {
            o.SetActive(calico);
        }
        foreach (GameObject o in orangeCatArray)
        {
            o.SetActive(orange);
        }
    }

    void unlockSkins()
    {
        if (highScore > 2000)
        {
            calicoCatBGButton.interactable = true;
            calicoLocked.SetActive(false);
        }
        if (highScore > 5000)
        {
            orangeCatBGButton.interactable = true;
            orangeLocked.SetActive(false);
        }
        if (highScore > 10000)
        {
            brownCatBGButton.interactable = true;
            brownLocked.SetActive(false);
        }
    }
}
