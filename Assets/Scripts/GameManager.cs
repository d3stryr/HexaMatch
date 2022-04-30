using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int numberOfSpheres=0;
    public GameObject[] levels;
    private int levelnum=0;
    public GameObject gameOver;
    public static bool isGameRunning = false;
    public static int totalPlatforms;
    public static bool isDisplayGO = false;
    public static bool isFailed = false;
    public static bool isAvailable = false;
    public TMP_Text gameOverText;
    public TMP_Text gameOverButtonText;
    // Start is called before the first frame update
    void Start()
    {
        levelnum = PlayerPrefs.GetInt("currentlev");
        switch(levelnum)
        {
            case 1 : levels[0].active = true;
                
                break;
            case 2 : levels[1].active = true;
                
                break;
            case 3 :  levels[2].active = true;
                
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfSpheres == 0 && isGameRunning)
        {
            var lev = PlayerPrefs.GetInt("levelnum");
            if(levelnum==lev)
            {
                levelnum++;
                if (levelnum > 3)
                {
                    levelnum = 3;
                }
                PlayerPrefs.SetInt("levelnum", levelnum);
            }
            isDisplayGO = true;
            isGameRunning = false;
        }
        if(isDisplayGO)
        {
            DisplayGameover();
        }
        if(isFailed)
        {
            isFailed = false;
            GameOver();
        }
        
    }

    void GameOver()
    {
        gameOverText.text = "You Failed this Level.";
        gameOverButtonText.text = "Go Back";
        DisplayGameover();
    }

    public void OnLevels()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
    }

    public void DisplayGameover()
    {
        isDisplayGO = false;
        gameOver.SetActive(true);
    }
}
