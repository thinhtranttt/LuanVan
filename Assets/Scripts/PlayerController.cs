using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Transform Item;
    private Transform bullets;
    private Transform sun;
    public Transform pin;

    public float health;
    public float range;
    private string weaponTag = "Weapon";
    private bool turnOnPin = true;
    private Animator anim;

    public Text txtLose;

    private Transform headCam;
    private bool isRotate;


    // Use this for initialization
    void Start()
    {
        sun = GameObject.FindGameObjectWithTag("Sun").transform;
        InvokeRepeating("UpdateTheItem", 0f, 0.5f);
        anim = GetComponent<Animator>();
        headCam = GameObject.FindGameObjectWithTag("HeadCam").transform;

    }

    void UpdateTheItem() // set item khi nv lai gan voi khoang cach range
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(weaponTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestWeapon = null;

        GameObject[] tmpBullets = GameObject.FindGameObjectsWithTag("Bullets");
        float shortestDistanceBullets = Mathf.Infinity;
        GameObject nearestBullets = null;

        foreach (GameObject item in items)
        {
            float distanceToItem = Vector3.Distance(transform.position, item.transform.position);
            if (distanceToItem < shortestDistance)
            {
                shortestDistance = distanceToItem;
                nearestWeapon = item;
            }
        }

        foreach (GameObject bullet in tmpBullets)
        {
            float distanceToItem = Vector3.Distance(transform.position, bullet.transform.position);
            if (distanceToItem < shortestDistanceBullets)
            {
                shortestDistanceBullets = distanceToItem;
                nearestBullets = bullet;
            }
        }

        if (nearestWeapon != null && shortestDistance <= range)
        {
            Item = nearestWeapon.transform;


        }
        else
        {
            Item = null;
        }

        if (nearestBullets != null && shortestDistanceBullets <= range)
        {
            bullets = nearestBullets.transform;


        }
        else
        {
            bullets = null;
        }


    }

    public void TurnOnPin()
    {

        if (pin.transform.gameObject.active)
        {
            pin.transform.gameObject.SetActive(false);
        }
        else
        {
            pin.transform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Item != null)
            return;

        //Debug.Log(headCam.eulerAngles.y + " " + transform.eulerAngles.y);
        if (Input.GetAxis("Rotate") != 0 && !isRotate)
        {
            Debug.Log(Input.GetAxis("Rotate") + " " + isRotate);
            var tmp = headCam.eulerAngles;

            transform.eulerAngles = new Vector3(tmp.x, tmp.y, 0);

            Debug.Log(isRotate);
        }
        else
        {
            isRotate = false;
        }

    }

    public void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Debug.Log("Dead");
        anim.SetBool("Die", true);
        health = 0;
        txtLose.text = "Game Over!!!!";
        txtLose.color = Color.red;
    }
}
