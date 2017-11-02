using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public int health = 100;

    public void GetHit(int dame)
    {
        if(health > 0)
        {
            health -= dame;
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
}
