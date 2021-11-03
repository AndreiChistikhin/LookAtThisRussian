using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Granny : MonoBehaviour
{
    private NavMeshAgent babka;
    private Transform target;
    private Vector3 rotation;
   
    void Start()
    {
        babka = GetComponent<NavMeshAgent>();
        target = GameObject.FindObjectOfType<Player>().transform;
        rotation = gameObject.transform.eulerAngles;
        rotation.x = 90;
    }

    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(rotation.x, 0, 0);
        babka.SetDestination(target.position);
        if (target.localPosition.x - gameObject.transform.position.x<-1)
        {
            gameObject.transform.localScale = new Vector3(-0.5f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else if (target.localPosition.x - gameObject.transform.position.x > 1)
        {
            gameObject.transform.localScale = new Vector3(0.5f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
    }

    

}
