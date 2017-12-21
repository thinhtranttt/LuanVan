using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    public GameObject player;
    public int health = 100;
    private NavMeshAgent agent;

    public void GetHit(int dame)
    {
        if(health > 0)
        {
            health -= dame;
            Debug.Log("quat");
        }
        if(health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 5)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
