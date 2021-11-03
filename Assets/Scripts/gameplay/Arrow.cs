using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Linq;

public class Arrow : MonoBehaviour
{
    
    [SerializeField]
    GameObject arrow;
    private Vodka[] vodkaScripts;

    private bool vodkaInstatiated;
    private bool dictionaryCompleted;

    private Vodka followedVodka;
    private Color color;
    private SpriteRenderer arrowSprite;
    private Dictionary<Vodka, float> vodkaDistance=new Dictionary<Vodka, float>();
    private Dictionary<int, Vodka> secondDictionary = new Dictionary<int,Vodka>();

    private void Start()
    {
        arrow = Instantiate<GameObject>(arrow, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        arrowSprite = arrow.GetComponent<SpriteRenderer>();
        color = arrowSprite.material.color;
    }

    private void Update()
    {
        if ((GameObject.FindObjectOfType<LevelInfo>().GameLevel / 5 + 1) == GameObject.FindObjectsOfType<Vodka>().Length && !vodkaInstatiated) 
        {
            vodkaScripts = GameObject.FindObjectsOfType<Vodka>();
            vodkaInstatiated = true;
        }
        if (vodkaInstatiated)
        {
            if (!dictionaryCompleted)
            {
                for (int i = 0; i < vodkaScripts.Length; i++)
                {
                    vodkaDistance.Add(vodkaScripts[i], vodkaScripts[i].PlayerDistance);
                }
                dictionaryCompleted = true;
            }
            else
            {
                vodkaScripts = GameObject.FindObjectsOfType<Vodka>();
                if (vodkaScripts.Length != 0)
                {
                    for (int i = 0; i < vodkaScripts.Length; i++)
                    {
                        vodkaDistance[vodkaScripts[i]] = vodkaScripts[i].PlayerDistance;
                    }

                    var firstVodka = vodkaDistance.OrderBy(pair => pair.Value).First();

                    followedVodka = firstVodka.Key;
                    if (followedVodka == null)
                    {
                        vodkaDistance.Remove(firstVodka.Key);
                    }

                    Vector3 playerPosition = GameObject.FindObjectOfType<Movement>().PlayerPosition;
                    //Adjust arrows pointing at vodka
                    Vector3 arrowPosition = playerPosition + (followedVodka.PlayerDistanceVector.normalized * 5f);
                    arrowPosition.y = 3;
                    arrow.transform.position = arrowPosition;

                    float angleY = Mathf.Acos(followedVodka.PlayerDistanceVector.x / Mathf.Sqrt(Mathf.Pow(followedVodka.PlayerDistanceVector.x, 2) + Mathf.Pow(followedVodka.PlayerDistanceVector.z, 2)));
                    if (followedVodka.PlayerDistanceVector.z > 0)
                    {
                        angleY = -angleY;
                    }

                    angleY = (angleY * 180 / Mathf.PI) + 180;
                    arrow.transform.rotation = Quaternion.Euler(90, angleY, 0);

                    if (followedVodka.PlayerDistance < 15)
                    {
                        color.a = 0f;
                        arrowSprite.material.color = color;
                    }
                    else
                    {
                        color.a = 1f;
                        arrowSprite.material.color = color;
                    }
                }
                
            }
            
        }
        
    }
}
