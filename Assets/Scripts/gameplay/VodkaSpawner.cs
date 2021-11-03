using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VodkaSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject vodka;

    private int vodkaNumber;

    private LevelInfo levelInfo;

    public int VodkaNumber => vodkaNumber;
    
    void Start()
    {
        levelInfo = GameObject.FindObjectOfType<LevelInfo>();
        
        if (levelInfo.GameLevel < 51)
        {
            vodkaNumber = levelInfo.GameLevel / 5 + 1;

            for (int i = 0; i < vodkaNumber; i++)
            {
                InstantiateVodka();
            }
        }
        else
        {
            vodkaNumber = levelInfo.GameLevel / 5 -5;

            for (int i = 0; i < vodkaNumber; i++)
            {
                InstantiateVodka();
            }
        }

    }

    public void InstantiateVodka()
    {
            Instantiate(vodka, new Vector3(Random.Range(levelInfo.MinimumSpawnCoordinate, levelInfo.MaximumSpawnCoordinate), 0.1f, Random.Range(levelInfo.MinimumSpawnCoordinate, levelInfo.MaximumSpawnCoordinate)), Quaternion.Euler(90, 0, 0));
    }     
}
