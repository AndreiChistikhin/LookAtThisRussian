using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject playerSprite;

    private BoxCollider playerCollider;
    private Vector3 playerPosition;
    private Rigidbody rb;
    private SpriteRenderer playerRenderer;

    private Timer playerColliderIsTrigger;
    private Timer invulnerability;
    private bool invulnerabilityIsRunning;

    public UnityEvent OnGrannyEnterEvent;

    private LevelInfo levelInfo;

    private float verticalTouchPosition;
    private float horizontalTouchPosition;
    private bool PlayerInstantiated;

    private bool needToDestroy;

    public bool NeedToDestroy => needToDestroy;

    void Start()
    {
        invulnerability = gameObject.AddComponent<Timer>();
        invulnerability.Duration = 3;

        rb = gameObject.GetComponent<Rigidbody>();

        playerCollider = gameObject.GetComponent<BoxCollider>();
        playerCollider.isTrigger = false;

        playerRenderer = playerSprite.GetComponent<SpriteRenderer>();

        OnGrannyEnterEvent.AddListener(GrannyTouched);

        levelInfo = FindObjectOfType<LevelInfo>();
        PlayerSpawn();
    }

    private void Update()
    {
        //Was touched by a granny
        if (invulnerability.Running)
        {
            playerRenderer.color = Color.red;
        }
        else
        {
            playerRenderer.color = Color.white;
        }

        if (invulnerability.Finished)
        {
            invulnerabilityIsRunning = false;
        }
        if (!PlayerInstantiated)
        {
            PlayerSpawn();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Granny granny) && !invulnerabilityIsRunning)
        {
            OnGrannyEnterEvent.Invoke();
        }
    }

    void PlayerSpawn()
    {
        if (levelInfo.MinimumSpawnCoordinate != 0 && levelInfo.MaximumSpawnCoordinate != 0)
        {
            playerPosition = new Vector3(Random.Range(levelInfo.MinimumSpawnCoordinate, levelInfo.MaximumSpawnCoordinate), 0.1f, Random.Range(levelInfo.MinimumSpawnCoordinate, levelInfo.MaximumSpawnCoordinate));
            transform.position = playerPosition;
            PlayerInstantiated = true;
        }
    }

    void GrannyTouched()
    {
        invulnerabilityIsRunning = true;
        invulnerability.Run();
        FindObjectOfType<AudioManager>().Play("HealthDecreased");
    }
}
