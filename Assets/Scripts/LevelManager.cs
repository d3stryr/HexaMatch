using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("levelnum"))
        {
            PlayerPrefs.SetInt("levelnum", 1);
        }
        var levelnum = PlayerPrefs.GetInt("levelnum");
        for(int i=0;i<levelnum;i++)
        {
            levelButtons[i].GetComponent<Button>().interactable = true;
        }

    }
    public void OnButtonClick(Button button)
    {
        var lev = int.Parse(button.name);
        PlayerPrefs.SetInt("currentlev", lev);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
    public void OnBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
