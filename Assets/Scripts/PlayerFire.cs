using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    public int dame;
    public Transform nongNham;
    public GameObject tracer;
    public GameObject headGun;
    public VirtualJoyStick move;

    public Transform gunDame;
    public Transform gunWearing;

    public AudioClip[] audioClip;
    public AudioSource audioSource;

    public Transform numberBullet;

    public int maxBullet;
    public int curBullet;
    public int curBulletReload;

    public string nameOfWeapon;

    private Text textBullet;
    private Animator anim;
    private bool shoot = false;
    private bool reload = false;
    private IEnumerator coroutine;

    public void Shoot()
    {
        shoot = true;
    }

    public void GetDame()
    {
        for (int i = 0; i < gunWearing.GetChildCount(); i++)
        {
            if (gunWearing.GetChild(i).gameObject.active)
            {
                
                for (int j = 0; j < gunDame.GetChildCount(); j++)
                {
                    
                    if (gunWearing.GetChild(i).gameObject.name.Equals(gunDame.GetChild(j).gameObject.name))
                    {
                        var tmp = gunDame.GetChild(j).GetComponent<Item>();
                        dame = tmp.dame;
                        maxBullet = tmp.maxBullet;
                        curBullet = tmp.currentBullet;
                        curBulletReload = curBullet;
                        nameOfWeapon = gunWearing.GetChild(i).name;
                    }
                }
            }
        }
    }
    public bool Check()
    {
        for (int i = 0; i < gunWearing.GetChildCount(); i++)
        {
            if (gunWearing.GetChild(i).gameObject.active && gunWearing.GetChild(i).gameObject.name.Equals(nameOfWeapon))
            {
               
               return false;
                
            }
        }
        return true;
    }
    void DisplayBulletText()
    {
        textBullet.text = curBullet + "/" + maxBullet;
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        maxBullet -= (curBulletReload - curBullet);
        curBullet = curBulletReload;
        DisplayBulletText();
        anim.SetBool("Reload", false);
        reload = false;
    }

    public void ReloadBullet()
    {
        
        if (maxBullet != 0 && move.move != true && curBullet != curBulletReload)
        {
            reload = true;
            anim.SetBool("Reload", true);
            audioSource.clip = audioClip[1];
            audioSource.Play();
            coroutine = Wait(3.0f);
            StartCoroutine(coroutine);
            
            
        }
        
    }

    void Fire()
    {
        if (shoot == true)
        {
            if(curBullet == 0 || reload)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(nongNham.position);
            RaycastHit hit;
            audioSource.clip = audioClip[0];
            audioSource.Play();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag.Equals("Zombie"))
                {
                    hit.transform.gameObject.GetComponent<ZombieController>().GetHit(dame);
                }
            }
            if (move.move == true)
            {
                anim.SetBool("WalkFiring", true);
            }
            else
            {
                anim.SetBool("Shoot", true);
            }
            InsTracer();
            shoot = false;

            curBullet--;
            
            DisplayBulletText();
        }
        else
        {
            anim.SetBool("Shoot", false);
            anim.SetBool("WalkFiring", false);
        }
    }

    void InsTracer()
    {
        GameObject trc = Instantiate(tracer, headGun.transform.position, transform.rotation);
        Destroy(trc, 0.2f);
    }

 


    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        textBullet = numberBullet.GetChild(0).GetComponent<Text>();
        GetDame();
        DisplayBulletText();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        if(Check())
        {
            GetDame();
            DisplayBulletText();
        }
    }
}
