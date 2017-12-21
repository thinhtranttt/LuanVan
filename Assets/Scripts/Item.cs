using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public Transform player;
    public Transform Gun;
    public Transform items;

    private Button pick;

    public void PickUp() // nhat sung
    {
        for (int i = 0; i < Gun.GetChildCount(); i++)
        {
            if (checkEmpty(Gun)) // empty
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
                        if (items.GetChild(j).name.Equals(Gun.GetChild(i).name)) // quang sung
                        {
                            items.GetChild(j).gameObject.SetActive(true);
                            var tmpItem = items.GetChild(j).gameObject.GetComponent<GunInfo>();
                            var tmpGun = Gun.GetChild(i).gameObject.GetComponent<GunInfo>();

                            tmpItem.maxBullet = tmpGun.maxBullet;
                            tmpItem.curBullet = tmpGun.curBullet;

                            
                        }
                        Gun.GetChild(j).transform.gameObject.SetActive(false);
                        if (transform.name.Equals(Gun.GetChild(j).name)) // nhat sung
                        {
                            Gun.GetChild(j).gameObject.SetActive(true);  
                        }
                    }
                    return;
                }
            }
            
        }
        
    }

    private bool checkEmpty(Transform x) // kiem tra co dang cam sung hay khong
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


    void UpdatePlayer() // kiem tra khoang cach toi item 
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
}
