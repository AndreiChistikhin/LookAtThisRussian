using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    private int savedLevel;

    void SaveGame()
    {
        PlayerPrefs.SetInt("SavedInteger", savedLevel);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }
    void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedInteger"))
        {
            savedLevel = PlayerPrefs.GetInt("SavedInteger");
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
