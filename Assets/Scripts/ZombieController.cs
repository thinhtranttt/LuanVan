using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    public GameObject player;
    public int health = 100;
    public float rangeFollow;
    public float rangeAttack;

    private Animator anim;
    private NavMeshAgent agent;

    public void GetHit(int dame)
    {
        if(health > 0)
        {
            health -= dame;
            Debug.Log("quat");
            anim.SetBool("KnockBack", true);
            StartCoroutine(WaitTime(0.2f));
           
        }
        if(health <= 0)
        {
            Dead();
        }
    }

    

    private void Dead()
    {
        Debug.Log("Dead");
        
        anim.SetBool("Dead", true);
        anim.SetBool("Crawl", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Attack", false);

        StartCoroutine(WaitDead(2f));
        
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("KnockBack", false);
    }

    IEnumerator WaitDead(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ZombieControll();
    }

    private void ZombieControll()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= rangeFollow)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= rangeAttack)
            {
                Debug.Log("Attack");
                agent.isStopped = true;
                anim.SetBool("Attack", true);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                anim.SetBool("Walk", true);
                anim.SetBool("Attack", false);
                Debug.Log("Walking");
            }
           

        }
        else
        {
            anim.SetBool("Walk", false);
        }
        if (health <= 60)
        {
            anim.SetBool("Crawl", true);
            //anim.SetBool("Walk", false);
            //anim.SetBool("Attack", false);
        }

    }
}
