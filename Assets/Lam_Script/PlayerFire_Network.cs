using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerFire_Network : NetworkBehaviour
{
    
    public int dame;
    public GameObject bloodZom;
    public GameObject headGun;
    public GameObject scope;

    public Transform gunDame;
    public Transform gunWearing;
    public GameObject bullet;
    public Transform shootHole;

    public GameObject headCam;

    public AudioClip[] audioClip;
    public AudioSource audioSource;
    public AudioSource audioSourceNature;
    private Transform numberBullet;

    public int maxBullet;
    public int curBullet;
    public int curBulletReload;

    public string nameOfWeapon;

    private Text textBullet;

    private Animator anim;
    private bool pickBullets = false;
    private bool reload = false;
    private IEnumerator coroutine;
    private int numBullets = 0;
    private float startShoot = 0f;
    private float nextShoot = 0.1f;


    public void SetMaxBullet(int addBullet) // them bang dan
    {
        maxBullet += addBullet;
        GetCurGun().GetComponent<GunInfo>().maxBullet += addBullet;
        DisplayBulletText();
    }

    public int GetMaxBullet() // tra ve bang dan
    {
        return maxBullet;
    }

    public Transform GetCurGun() // lay sung hien tai dang cam
    {
        for (int i = 0; i < gunWearing.GetChildCount(); i++)
        {
            if (gunWearing.GetChild(i).gameObject.active)
            {
                return gunWearing.GetChild(i);
            }
        }
        return null;
    }

    public void GetDame() // lay thong tin sung
    {
        //for (int i = 0; i < gunWearing.GetChildCount(); i++)
        //{
        //    if (gunWearing.GetChild(i).gameObject.active)
        //    {

        //        for (int j = 0; j < gunDame.GetChildCount(); j++)
        //        {

        //            if (gunWearing.GetChild(i).gameObject.name.Equals(gunDame.GetChild(j).gameObject.name))
        //            {
        //                var tmp = gunDame.GetChild(j).GetComponent<GunInfo>();
        //                dame = tmp.dame;
        //                maxBullet = tmp.maxBullet;
        //                curBullet = tmp.curBullet;
        //                curBulletReload = curBullet;
        //                nameOfWeapon = gunWearing.GetChild(i).name;
        //            }
        //        }
        //    }
        //}
        var tmp = GetCurGun().GetComponent<GunInfo>();
        dame = tmp.dame;
        maxBullet = tmp.maxBullet;
        curBullet = tmp.curBullet;
        curBulletReload = tmp.curBulletReLoad;
        for (int i = 0; i < gunWearing.GetChildCount(); i++)
        {
            if (gunWearing.GetChild(i).gameObject.active)
            {
                nameOfWeapon = gunWearing.GetChild(i).name;
                break;

            }
        }
        
    }

    private void UpdateInfo()
    {
        GetCurGun().GetComponent<GunInfo>().maxBullet = maxBullet;
        GetCurGun().GetComponent<GunInfo>().curBullet = curBullet;

    }

    public bool Check() // kiem tra luc nhat sung de cap nhat lai thong tin
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
    public void DisplayBulletText() // hien thi thong tin bang dan len man hinh
    {
        textBullet.text = curBullet + "/" + maxBullet;
    }

    private IEnumerator WaitReload(float time) // thay dan
    {
        yield return new WaitForSeconds(time);

        GetDame();

        maxBullet -= (curBulletReload - curBullet);
        curBullet = curBulletReload;
        if (maxBullet <= 0) maxBullet = 0;
        if (maxBullet < curBulletReload) curBulletReload = maxBullet;

        
        DisplayBulletText();
        anim.SetBool(Constant.RELOAD, false);
        reload = false;
        UpdateInfo();
    }

    private IEnumerator WaitShoot(float time) //cho ban
    {
        yield return new WaitForSeconds(time);
        anim.SetBool(Constant.SHOOT, false);
        anim.SetBool(Constant.WALK_FIRING, false);
        
    }
    public void ReloadBullet()// thay dan
    {

        if (maxBullet != 0 && curBullet != curBulletReload && Input.GetAxis("Leave") != 0 && !reload)
        {
            GetDame();
            reload = true;
            anim.SetBool(Constant.WALK, false);
            anim.SetBool(Constant.RELOAD, true);
            audioSourceNature.clip = audioClip[1];
            audioSourceNature.Play();
            coroutine = WaitReload(3.0f);
            StartCoroutine(coroutine);

        }

    }
    [Command]
    void CmdSpawnBullet()
    {
        var bl = Instantiate(bullet);
        bl.transform.position = headGun.transform.position;
        bl.GetComponent<Rigidbody>().AddForce((scope.transform.position - headGun.transform.position).normalized*20f, ForceMode.Impulse);

        //bl.GetComponent<Rigidbody>().AddForce(bl.transform.forward * 20f, ForceMode.Impulse);
        NetworkServer.Spawn(bl);
        //NetworkServer.SpawnWithClientAuthority(bl, connectionToClient);
    }

    void Fire() // ban dan
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            if (Time.time > startShoot)
            {

                startShoot = Time.time + nextShoot;
                if (curBullet <= 0)
                {

                    Debug.Log("Het Dan");
                    audioSourceNature.clip = audioClip[2];
                    audioSourceNature.Play();
                    GetCurGun().GetChild(0).gameObject.SetActive(false);
                    if (maxBullet != 0 && !reload)
                    {
                        reload = true;
                        anim.SetBool(Constant.WALK, false);
                        anim.SetBool(Constant.RELOAD, true);
                        audioSourceNature.clip = audioClip[1];
                        audioSourceNature.Play();
                        coroutine = WaitReload(3.0f);
                        StartCoroutine(coroutine);
                    }
                    return;
                }
                if (reload) return;

                CmdSpawnBullet();

                //NetworkServer.Listen(NetworkServer.listenPort);

                audioSourceNature.clip = audioClip[0];
                audioSourceNature.Play();
                

                GetCurGun().GetChild(0).gameObject.SetActive(true);
                //shoot = false;

                curBullet--;
                GetCurGun().GetComponent<GunInfo>().curBullet = curBullet;

                DisplayBulletText();
                coroutine = WaitShoot(0.1f);
                StartCoroutine(coroutine);
            }
        }
        else
        {
            GetCurGun().GetChild(0).gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            //Destroy(this);
            return;
        }

        // Joystick button
        //move = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<VirtualJoyStick>();

        // Bullet information text GUI
        numberBullet = GameObject.FindGameObjectWithTag("NumBulletCanvas").transform;

        anim = GetComponent<Animator>();
        textBullet = numberBullet.GetChild(0).GetComponent<Text>();
        GetDame();
        DisplayBulletText();
        UpdateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            //Destroy(this);
            return;
        }
        Fire();
        ReloadBullet();
        
    }
}
