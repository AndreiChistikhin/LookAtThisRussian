using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBar : MonoBehaviour
{
    private GameObject player;
    private Vodka[] vodkaScript;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
        vodkaScript = GameObject.FindObjectsOfType<Vodka>();
    }

    //Destroy and adjust PickUpBar
    void Update()
    {
        gameObject.transform.position = player.transform.position + new Vector3(0, 0, 1);

        foreach (Vodka script in vodkaScript)
        {
            if (script.VodkaAddedTimer.Finished||script.VodkaAddedTimer.IsStopped)
            {
                Destroy(gameObject);
                if (script.VodkaAddedTimer.IsStopped)
                {
                    script.VodkaAddedTimer.IsStopped = false;
                }
            }
        }  
    }
}
