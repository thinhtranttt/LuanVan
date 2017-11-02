using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public Transform player;
    public Transform Gun;
    public Transform items;

    public int maxBullet;
    public int currentBullet;
    public int dame;
    public float range;
    private string playerTag = "Player";
    private Button pick;

    public int GetDame()
    {
        return dame;
    }


    public void PickUp()
    {
        for (int i = 0; i < Gun.GetChildCount(); i++)
        {
            if (checkEmpty(Gun))
            {
                Gun.GetChild(i).transform.gameObject.SetActive(false);
                if (transform.name.Equals(Gun.GetChild(i).name))
                {
                    Gun.GetChild(i).gameObject.SetActive(true);
                }
                Destroy(gameObject);
            }
            else
            {
                if (Gun.GetChild(i).gameObject.active)
                {
                    for (int j = 0; j < items.GetChildCount(); j++)
                    {
                        items.GetChild(j).transform.gameObject.SetActive(false);
                        if (items.GetChild(j).name.Equals(Gun.GetChild(i).name))
                        {
                            items.GetChild(j).gameObject.SetActive(true);
                        }
                        Gun.GetChild(j).transform.gameObject.SetActive(false);
                        if (transform.name.Equals(Gun.GetChild(j).name))
                        {
                            Gun.GetChild(j).gameObject.SetActive(true);
                        }
                    }
                    return;
                }
            }
            
        }
        
    }

    private bool checkEmpty(Transform x)
    {
        for(int i=0; i<x.GetChildCount(); i++)
        {
            if(x.GetChild(i).gameObject.active)
            {
                return false;
            }
        }
        return true;
    }


    void UpdatePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(Vector3.Distance(transform.position, player.position) <= 4f)
        {
            pick.transform.gameObject.SetActive(true);
        }
        else
        {
            pick.transform.gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        
        pick = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        InvokeRepeating("UpdatePlayer", 0f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
