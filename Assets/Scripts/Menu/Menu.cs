using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject optionsMenu;
    [SerializeField]
    GameObject failMenu;
    [SerializeField]
    GameObject finishMenu;
    [SerializeField]
    GameObject gameEndMenu;
    [SerializeField]
    GameObject loading;
    [SerializeField]
    Slider loadingBar;
    [SerializeField]
    Button continueButton;
    [SerializeField]
    Button startButton;
    [SerializeField]
    Button optionsButton;

    LevelInfo levelInfo;
    bool continueGame;

    public GameObject FinishMenu => finishMenu;
    public GameObject FailMenu => failMenu;
    public GameObject GameEndMenu => gameEndMenu;

    private void Start()
    {
        Color greyColor = new Color(80, 80, 80);
        levelInfo = GameObject.FindObjectOfType<LevelInfo>();
        if (!PlayerPrefs.HasKey("SavedInteger"))
        {
            continueButton.interactable = false;
            ColorBlock cb = continueButton.colors;
            cb.normalColor = greyColor;
            continueButton.colors = cb; 
        }
    }

    public void StartGame()
    {
        continueGame = false;
        loading.SetActive(true);
        //SceneManager.LoadScene("Level_1");
        StartCoroutine(LoadAsync());
    }

    public void ContinueGame()
    {
        continueGame = true;
        loading.SetActive(true);
        //SceneManager.LoadScene("Level_1");
        StartCoroutine(LoadAsync());
        //FindObjectOfType<LookAtThisRussian>().LoadGame();
    }

    IEnumerator LoadAsync()
    {
        continueButton.interactable = false;
        startButton.interactable = false;
        optionsButton.interactable = false;
        AsyncOperation asyncLoad;
        if (PlayerPrefs.HasKey("SavedInteger") && continueGame == true)
        {
            asyncLoad = SceneManager.LoadSceneAsync("Level_"+ PlayerPrefs.GetInt("SavedInteger"));
        }
        else
        {
            asyncLoad = SceneManager.LoadSceneAsync("Level_1");
        }
            
        

        while (!asyncLoad.isDone)
        {
            loadingBar.value = asyncLoad.progress;
            yield return null;     
        }    
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level_"+(levelInfo.GameLevel+1).ToString());
        Time.timeScale = 1;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Level_" + (levelInfo.GameLevel).ToString());
        Time.timeScale = 1;
    }

    public void Options()
    {
        gameObject.SetActive(false);
        optionsMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Exit()
    {
        optionsMenu.SetActive(false);
        gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
