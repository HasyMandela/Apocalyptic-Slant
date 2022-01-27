using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float delay;
    [SerializeField] private GameObject[] playerHurt;
    [SerializeField] private GameObject player;
    private float damageRate;
    private bool isDamage;
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && isDamage)
        {
            PlayerScript.Instance.health -= damage;
            Instantiate(playerHurt[Random.Range(0, playerHurt.Length)], transform.position, Quaternion.identity);
            damageRate = delay;
        }
        if (collision.collider.CompareTag("Ground"))
        {
            agent.SetDestination(player.transform.position);
        }
    }
    void Update()
    {
        if (damageRate <= 0)
        {
            isDamage = true;
        } else if (damageRate > 0)
        {
            damageRate -= Time.deltaTime;
            isDamage = false;
        }
    }
}
