using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController_Network : NetworkBehaviour
{

    private Transform Item;
    private Transform bullets;
    private Transform sun;
    private Transform camEmty;

    public Transform parent;

    public GameObject VR;

    public float health;
    public float range;
    private string weaponTag = "Weapon";
    private bool turnOnPin = true;
    private Animator anim;

    public Text txtHealth;
    public Image blackScreen;

    private Transform headCam;
    private bool isRotate;

    public Transform ahead;

    // Use this for initialization
    void Start()
    {
        // If the player is not the user's player
        if (!isLocalPlayer)
        {
            //... then remove the script. Because we don't want player who are
            // not the user's player control the camera.
            Destroy(this);
            return;
        }
        UnityEngine.XR.XRSettings.enabled = true;
        sun = GameObject.FindGameObjectWithTag("Sun").transform;
        InvokeRepeating("UpdateTheItem", 0f, 0.5f);
        anim = GetComponent<Animator>();
        headCam = GameObject.FindGameObjectWithTag("HeadCam").transform;
        txtHealth.text = health + "";
        camEmty = GameObject.FindGameObjectWithTag("Rotate").transform;
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

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Item != null)
            return;


        parent.localScale = VR.transform.localScale;

        VR.transform.SetParent(camEmty);


        transform.eulerAngles = new Vector3(headCam.eulerAngles.x, headCam.eulerAngles.y, 0f);

        VR.transform.SetParent(transform);
        VR.transform.position = parent.transform.position;
        VR.transform.localScale = parent.localScale;
        Debug.Log(transform.eulerAngles.y + "   " + headCam.eulerAngles.y);
    }

    public void GetHit(float damage)
    {
        health -= damage;
        txtHealth.text = health + "";
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

        blackScreen.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 255), 1f * Time.deltaTime);
        StartCoroutine(WaitDead());
    }

    IEnumerator WaitDead()
    {
        yield return new WaitForSeconds(2f);

        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopClient();

    }
}
