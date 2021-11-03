using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    private GameObject plane;

    private int gameLevel;
    private float minimumSpawnCoordinate;
    private float maximumSpawnCoordinate;

    public float MinimumSpawnCoordinate=> minimumSpawnCoordinate;
    public float MaximumSpawnCoordinate=>maximumSpawnCoordinate;
    public int GameLevel => gameLevel;

    void Awake()
    {
        gameLevel=Int32.Parse(SceneManager.GetActiveScene().name.Replace("Level_","")); 

        plane = FindObjectOfType<Plane>().gameObject;

        minimumSpawnCoordinate = -plane.transform.localScale.x * 5 + 2;
        maximumSpawnCoordinate = +plane.transform.localScale.x * 5 - 2;
    }
}
