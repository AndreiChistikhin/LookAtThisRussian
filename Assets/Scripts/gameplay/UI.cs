using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Advertisements;


public class UI : MonoBehaviour
{
    [SerializeField]
    Image[] life;
    private int lifeNumber;

    [SerializeField]
    TextMeshProUGUI vodkaText;
    private int vodkaPoints;
    private int objectNumber;

    [SerializeField]
    TextMeshProUGUI grannyText;
    private int grannyCount;

    [SerializeField]
    TextMeshProUGUI levelText;

    private LevelInfo levelInfo;

    private bool isFinished;
    private bool isFailed;

    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4366297", false);
        }

        levelInfo = GameObject.FindObjectOfType<LevelInfo>();
        if (levelInfo.GameLevel < 51)
        {
            objectNumber = levelInfo.GameLevel / 5 + 1;
        }
        else
        {
            objectNumber= levelInfo.GameLevel / 5 - 5;
        }
        
        vodkaText.text="0/"+ objectNumber;
        grannyText.text = "0/" + objectNumber;
        levelText.text = "lvl" + levelInfo.GameLevel;
        lifeNumber = life.Length;
        GameObject.FindObjectOfType<Player>().GetComponent<Player>().OnGrannyEnterEvent.AddListener(HealthChange);
    }

    //StartGameAgain
    private void Update()
    {
        if (lifeNumber == 0&&!isFailed)
        {
            if (Advertisement.IsReady())
            {
                Debug.Log(2);
                Advertisement.Show("Interstitial_Android");
            }
            GameObject.FindObjectOfType<Menu>().FailMenu.SetActive(true);
            isFailed = true;
            Time.timeScale = 0;
        }
        if (vodkaPoints == objectNumber&&!isFinished)
        {
            if (Advertisement.IsReady()&&levelInfo.GameLevel>9&& levelInfo.GameLevel%5==0)
            {
                Debug.Log(2);
                Advertisement.Show("Interstitial_Android");
            }
            if (levelInfo.GameLevel != 99)
            {
                GameObject.FindObjectOfType<Menu>().FinishMenu.SetActive(true);
                isFinished = true;
                FindObjectOfType<AudioManager>().Play("Win");
                Time.timeScale = 0;
            }
            else
            {
                GameObject.FindObjectOfType<Menu>().GameEndMenu.SetActive(true);
                isFinished = true;
                FindObjectOfType<AudioManager>().Play("Win");
                Time.timeScale = 0;
            }
        }
    }
    
    public void TextChange()
    {
        vodkaPoints++;
        vodkaText.text = vodkaPoints + "/"+ objectNumber;
    }

    public void HealthChange()
    {
        lifeNumber--;
        Destroy(life[lifeNumber]);
    }
    
    public void GrannyAdded()
    {
        grannyCount++;
        grannyText.text = grannyCount + "/"+ objectNumber;
    }
}
