using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{

    public float speed;
    public GameObject shotHole;
    public GameObject bloodZom;

    private float life = 3f;
    private float age;

    public AudioClip audioBlood;

    private Transform head;
    private Transform tail;
    private AudioSource audioSource;
    private int damage;
    
	// Use this for initialization
	void Start () {
        damage = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerFire_Network>().dame;
        audioSource = GetComponent<AudioSource>();
        age = 0;
        //Destroy(gameObject, 3f);
	}

    [ServerCallback]
    private void Update()
    {
        age += Time.deltaTime;
        if(age > life)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer)
        {
            return;
        }
        if (other.tag == "Zombie")
        {
            other.GetComponent<ZombieController>().GetHit(damage);
            //Instantiate(bloodZom, other.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(audioBlood);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(shotHole, other.transform.position, Quaternion.identity);
        }
    }
}
