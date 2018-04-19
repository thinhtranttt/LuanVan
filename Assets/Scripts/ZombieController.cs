using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    // Store all players (zombie's targets)
    GameObject[] playerList;

    // Zombie's information
    public string zombieName = "AA";
    int health = 100;
    float rangeFollow = 40;
    float rangeAttack = 2.9f;

    // Zombie's target (the nearest target)
    Transform player;
    bool foundPlayer = false;
    int nearestIndex = 0;

    bool isDead;
    
    // Zombie's animation and its model
    private Animator anim;
    private NavMeshAgent agent;

    private AudioSource audioSource;
    // Zombie's attacked by player
    public void GetHit(int dame)
    {
        if(health > 0)
        {
            health -= dame;
            anim.SetBool("KnockBack", true);
            StartCoroutine(WaitTime(0.2f));
           
        }
        if(health <= 0)
        {
            Dead();
        }
    }

    // Zombie's killed by player
    private void Dead()
    {
        
        if (!isDead)
        {
            PlayerManager.instance.countZombie--;
            Debug.Log("Dead");
            anim.SetBool("Dead", true);
            anim.SetBool("Crawl", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Attack", false);

            StartCoroutine(WaitDead(2f));
            isDead = true;
        }

    }

    // Delay time for dead animation
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

    // Initiate information
    private void Start()
    {
        SetUpZombieInfo(zombieName);
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerList = GameObject.FindGameObjectsWithTag("Player");

            if (playerList.Length == 1)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else
            {
                // Find the nearest player
                nearestIndex = NearestPlayer(playerList);
                player = playerList[nearestIndex].transform;
            }
            
        }
        else
        {
            playerList = GameObject.FindGameObjectsWithTag("FakePlayer");
            player = GameObject.FindGameObjectWithTag("FakePlayer").transform;
        }
    }

    // Update the scence per frame
    private void Update()
    {
        ZombieControll();
    }

    private void ZombieControll()
    {
        
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Store the current number of players
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");

            // If the current number of players is different of last stored
            if (temp.Length != playerList.Length)
            {
                // Change the last stored to the new one
                playerList = temp;
            }

            if (playerList.Length == 1)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else
            {
                // Find the nearest player
                nearestIndex = NearestPlayer(playerList);
                player = playerList[nearestIndex].transform;
            }
        }
        else
        {
            playerList = GameObject.FindGameObjectsWithTag("FakePlayer");
            player = GameObject.FindGameObjectWithTag("FakePlayer").transform;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= rangeFollow)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= rangeAttack)
            {
                agent.isStopped = true;
                anim.SetBool("Attack", true);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                anim.SetBool("Walk", true);
                anim.SetBool("Attack", false);
            }
            audioSource.mute = false;

        }
        else
        {
            anim.SetBool("Walk", false);
            audioSource.mute = true;
        }
        if (health <= 60)
        {
            anim.SetBool("Crawl", true);
            //anim.SetBool("Walk", false);
            //anim.SetBool("Attack", false);
        }

    }

    // Set up zombie's information base on its name
    private void SetUpZombieInfo(string name)
    {
        switch (name)
        {
            case "AA":
                health = 200;
                rangeFollow = 25;
                rangeAttack = 2.8f;
                break;
            case "A":
                health = 250;
                rangeFollow = 25;
                rangeAttack = 2.8f;
                break;
            case "B":
                health = 180;
                rangeFollow = 25;
                rangeAttack = 2.8f;
                break;
            case "C":
                health = 300;
                rangeFollow = 30;
                rangeAttack = 2.8f;
                break;
            case "D":
                health = 265;
                rangeFollow = 30;
                rangeAttack = 2.8f;
                break;
            case "E1":
                health = 500;
                rangeFollow = 20;
                rangeAttack = 2.8f;
                break;
            case "E2":
                health = 1000;
                rangeFollow = 15;
                rangeAttack = 2.8f;
                break;
            default:
                health = 100;
                rangeFollow = 40;
                rangeAttack = 2.9f;
                break;
        }
    }

    // Find the nearest player
    private int NearestPlayer(GameObject[] playerList)
    {
        int result = 0;

        float minDistance = Vector3.Distance(transform.position, playerList[0].transform.position);
        int minIndex = 0;

        for(int i = 0; i < playerList.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, playerList[i].transform.position);
            if (minDistance > distance)
            {
                minIndex = i;
                minDistance = distance;
            }
        }

        result = minIndex;
        return result;
    }
   
}
