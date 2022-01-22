using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    Transform playerTransform;

    void Awake(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTransform.position);
    }
}
