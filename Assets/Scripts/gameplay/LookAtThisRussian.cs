using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LookAtThisRussian : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    TextMeshProUGUI gameSavedText;

    private Timer textTimer;
    private int savedLevel = 1;
    private LevelInfo levelInfo;

    private void Start()
    {
        levelInfo = GameObject.FindObjectOfType<LevelInfo>();
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            SaveGame();
        }
        textTimer = gameObject.AddComponent<Timer>();
        textTimer.Duration = 2;
        textTimer.Run();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;  
        }
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (levelInfo.GameLevel % 5 == 0&& levelInfo.GameLevel>9)
            {
                if (gameSavedText.IsActive() && textTimer.Finished)
                {
                    gameSavedText.gameObject.SetActive(false);
                }
            }
        }
    }
    
    void SaveGame()
    {
        if (levelInfo.GameLevel > 9 && levelInfo.GameLevel % 5 == 0)
        {

            savedLevel = levelInfo.GameLevel;
            
            if (savedLevel> PlayerPrefs.GetInt("SavedInteger"))
            {
                Debug.Log("Game data saved!");
                PlayerPrefs.SetInt("SavedInteger", savedLevel);
                gameSavedText.gameObject.SetActive(true);
            }
            
        }  
        PlayerPrefs.Save();
        

    }
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedInteger"))
        {
            savedLevel = PlayerPrefs.GetInt("SavedInteger");
            SceneManager.LoadScene("Level_"+savedLevel);
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }    
    }
}
