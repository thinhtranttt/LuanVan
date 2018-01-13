using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Transform Item;
    public Transform bullets;
    public Transform sun;
    public Transform pin;

    public float range;
    private string weaponTag = "Weapon";
    private bool turnOnPin = true;

    private void OnDrawGizmosSelected() // khoang cach nhat item cua nhan vat
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Use this for initialization
    void Start()
    {
        
        InvokeRepeating("UpdateTheItem",0f,0.5f);
        
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
        //if(sun.rotation.y <= -60 || sun.position.y >= 100)
        //{
        //    pin.transform.gameObject.SetActive(true);
        //}

        if(sun.rotation.y <= -60 || sun.position.y >= 100)
        {
            RenderSettings.fog = false;
        }else
        {
            RenderSettings.fog = true;
        }
    }

}
