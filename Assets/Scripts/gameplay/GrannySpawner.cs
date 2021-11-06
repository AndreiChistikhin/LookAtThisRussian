using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GrannySpawner : MonoBehaviour
{
	
	[SerializeField]
	GameObject prefabGranny;
	private UI scriptUI;
    private Timer spawnTimer;
    private Vector3 spawnLocation;
    private LevelInfo levelInfo;

    void Start()
    {
        levelInfo = GameObject.FindObjectOfType<LevelInfo>();
        scriptUI = GameObject.FindObjectOfType<UI>().GetComponent<UI>();
        spawnTimer = gameObject.AddComponent<Timer>();
		StartRandomTimer();
	}

    void Update()
    {
		if (spawnTimer.Finished)
		{
			HandleSpawnTimerFinishedEvent();
		}
	}

	private void HandleSpawnTimerFinishedEvent()
	{
        if (levelInfo.GameLevel < 51)
        {
            if (GameObject.FindObjectsOfType<Granny>().Length < levelInfo.GameLevel / 5 + 1)
            {
                GrannySpawn();
                StartRandomTimer();
            }
        }

        else
        {
            if (GameObject.FindObjectsOfType<Granny>().Length < levelInfo.GameLevel / 5 -5)
            {
                GrannySpawn();
                StartRandomTimer();
            }
        }
	}

    //Spawn granny on an edge of the map
	void GrannySpawn()
    {
        spawnLocation.x = Random.Range(levelInfo.MinimumSpawnCoordinate, levelInfo.MaximumSpawnCoordinate);
		spawnLocation.y = 0.1f;
		spawnLocation.z = Random.Range(levelInfo.MinimumSpawnCoordinate, levelInfo.MaximumSpawnCoordinate);
		int i = Random.Range(1, 5);
		if (i == 1)
		{
            spawnLocation.x = levelInfo.MinimumSpawnCoordinate;
		}
		else if (i == 2)
		{
            spawnLocation.x = levelInfo.MaximumSpawnCoordinate;
        }
		else if (i == 3)
		{
            spawnLocation.z = levelInfo.MinimumSpawnCoordinate;
        }
		else 
		{
            spawnLocation.z = levelInfo.MaximumSpawnCoordinate;
        }

        Instantiate<GameObject>(prefabGranny, new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z), Quaternion.identity);
        scriptUI.GrannyAdded();
	}

	void StartRandomTimer()
    {
        float minimumSpawnTime;
        float maximumSpawnTime;
        if (GameObject.FindObjectsOfType<Granny>().Length == 0)
        {
            minimumSpawnTime = 1;
            maximumSpawnTime=2;
        }
        else
        {
            minimumSpawnTime = 4;
            maximumSpawnTime = 7;
        }
		spawnTimer.Duration =Random.Range(minimumSpawnTime,maximumSpawnTime);
		spawnTimer.Run();
	}
}
