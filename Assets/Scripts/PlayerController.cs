using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Transform Item;
 
    public float range = 3f;
    private string weaponTag = "Weapon";

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Use this for initialization
    void Start()
    {
        
        InvokeRepeating("UpdateTheItem",0f,0.5f);
        //Gun = Gun.GetChild(0).GetComponent<Transform>();
    }

    void UpdateTheItem()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(weaponTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestWeapon = null;

        foreach (GameObject item in items)
        {
            float distanceToItem = Vector3.Distance(transform.position, item.transform.position);
            if (distanceToItem < shortestDistance)
            {
                shortestDistance = distanceToItem;
                nearestWeapon = item;
            }
        }

        if (nearestWeapon != null && shortestDistance <= range)
        {
            Item = nearestWeapon.transform;
            //thay doi sung tai day


        }
        else
        {
            Item = null;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Item != null)
            return;
    }

}
