using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public Transform player;
    private Transform Gun;
    public Transform items;

    private Button pick;

    private bool isPick = false;

    public void PickUp() // nhat sung
    {
        if (Input.GetAxis("PickUp") != 0)
        {
            Debug.Log(isPick + " " + !isPick);
            if (!isPick)
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
                        isPick = true;
                    }
                    else
                    {
                        if (Gun.GetChild(i).gameObject.active)
                        {

                            for (int j = 0; j < items.GetChild(0).GetChildCount(); j++)
                            {
                                items.GetChild(0).GetChild(j).transform.gameObject.SetActive(false);
                                if (items.GetChild(0).GetChild(j).name.Equals(Gun.GetChild(i).name)) // quang sung
                                {
                                    items.GetChild(0).GetChild(j).gameObject.SetActive(true);
                                    
                                }
                                Gun.GetChild(j).transform.gameObject.SetActive(false);
                                if (transform.name.Equals(Gun.GetChild(j).name)) // nhat sung
                                {
                                    Gun.GetChild(j).gameObject.SetActive(true);
                                }
                            }
                            isPick = true;
                            player.GetComponent<PlayerFire_Network>().GetDame();
                            player.GetComponent<PlayerFire_Network>().DisplayBulletText();
                            return;
                        }
                    }
                }
            }
        }
        else
        {
            isPick = false;
        }
    }

    private bool checkEmpty(Transform x) // kiem tra co dang cam sung hay khong
    {
        for (int i = 0; i < x.GetChildCount(); i++)
        {
            if (x.GetChild(i).gameObject.active)
            {
                return false;
            }
        }
        return true;
    }

    
    void UpdatePlayer() // kiem tra khoang cach toi item 
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Gun = GameObject.FindGameObjectWithTag("Gun").transform;

            if (Vector3.Distance(transform.position, player.position) <= 4f)
            {
                pick.transform.gameObject.SetActive(true);
                PickUp();
            }
            else
            {
                pick.transform.gameObject.SetActive(false);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        
        pick = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        //InvokeRepeating("UpdatePlayer", 0f, 0.5f);
    }

    private void Update()
    {
        UpdatePlayer();
    }
}
