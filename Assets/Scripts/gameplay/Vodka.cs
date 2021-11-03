using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Vodka : MonoBehaviour
{
    [SerializeField]
    GameObject pickUpBar;

    private bool collisionEnter;
    private bool pickUpBarInstantiated;

    private Timer vodkaAddedTimer;
    private Vector3 playerPosition;

    private UI scriptUI;
 

    public bool CollisionEnter => collisionEnter;
    public Timer VodkaAddedTimer => vodkaAddedTimer;

    private LevelInfo levelInfo;

    private Vector3 playerDistance;
    public Vector3 PlayerDistanceVector => playerDistance;
    public float PlayerDistance => Mathf.Sqrt(Mathf.Pow(playerDistance.x, 2) + Mathf.Pow(playerDistance.z, 2));

    private PickUpBar instantiatedPickUpBar;


    private void Start()
    {
        vodkaAddedTimer = gameObject.AddComponent<Timer>();
        playerPosition = GameObject.FindObjectOfType<Movement>().PlayerPosition;
        levelInfo = GameObject.FindObjectOfType<LevelInfo>();
        scriptUI = GameObject.FindObjectOfType<UI>().GetComponent<UI>();
    }

    void Update()
    { 
        Vector3 playerPosition = GameObject.FindObjectOfType<Movement>().PlayerPosition;
        //Adjust arrows pointing at vodka
        playerDistance = transform.position - playerPosition;
        instantiatedPickUpBar = GameObject.FindObjectOfType<PickUpBar>();

        if (collisionEnter&&!pickUpBarInstantiated&&instantiatedPickUpBar==null)
        {
            Instantiate<GameObject>(pickUpBar, playerPosition + new Vector3(0, 0, 1), Quaternion.Euler(90, 0, 0));
            pickUpBarInstantiated = true;
        }

        if (vodkaAddedTimer.Finished)
        {
            //collisionEnter = false;
            scriptUI.TextChange();
            FindObjectOfType<AudioManager>().Play("VodkaAdded");
            Destroy(gameObject);
            //Destroy(arrow);
        }
        if ((Mathf.Abs(playerDistance.x) > 2|| Mathf.Abs(playerDistance.z) > 2)&& collisionEnter == true)
        {
                vodkaAddedTimer.Stop();
                collisionEnter = false;
                pickUpBarInstantiated = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player1))
        {
            if (collisionEnter == false&& instantiatedPickUpBar == null)
            {
                vodkaAddedTimer.Duration = 1;
                vodkaAddedTimer.Run();
                collisionEnter = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Wall wall) && levelInfo != null)
        {
            gameObject.transform.position = new Vector3(Random.Range(levelInfo.MinimumSpawnCoordinate, levelInfo.MaximumSpawnCoordinate), 0.1f, Random.Range(levelInfo.MinimumSpawnCoordinate, levelInfo.MaximumSpawnCoordinate));
        }
    }
}

