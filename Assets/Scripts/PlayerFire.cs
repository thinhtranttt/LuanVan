using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    public int dame;
    public Transform nongNham;
    public GameObject tracer;
    public GameObject headGun;
    public GameObject shotHole;
    public VirtualJoyStick move;

    public Transform gunDame;
    public Transform gunWearing;

    public AudioClip[] audioClip;
    public AudioSource audioSource;
    public AudioSource audioSourceNature;
    public Transform numberBullet;

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
    

    public void Shoot() // ban 
    {
        //shoot = true;
        Fire();
    }

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
        for (int i = 0; i < gunWearing.GetChildCount(); i++)
        {
            if (gunWearing.GetChild(i).gameObject.active)
            {

                for (int j = 0; j < gunDame.GetChildCount(); j++)
                {

                    if (gunWearing.GetChild(i).gameObject.name.Equals(gunDame.GetChild(j).gameObject.name))
                    {
                        var tmp = gunDame.GetChild(j).GetComponent<GunInfo>();
                        dame = tmp.dame;
                        maxBullet = tmp.maxBullet;
                        curBullet = tmp.curBullet;
                        curBulletReload = curBullet;
                        nameOfWeapon = gunWearing.GetChild(i).name;
                    }
                }
            }
        }
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
    void DisplayBulletText() // hien thi thong tin bang dan len man hinh
    {
        textBullet.text = curBullet + "/" + maxBullet;
    }

    private IEnumerator WaitReload(float time) // thay dan
    {
        yield return new WaitForSeconds(time);
        maxBullet -= (curBulletReload - curBullet);
        if (maxBullet <= 0) maxBullet = 0;
        curBullet = curBulletReload;
        DisplayBulletText();
        anim.SetBool(Constant.RELOAD, false);
        reload = false;
    }

    private IEnumerator WaitShoot(float time) //cho ban
    {
        yield return new WaitForSeconds(time);
        anim.SetBool(Constant.SHOOT, false);
        anim.SetBool(Constant.WALK_FIRING, false);
    }
    public void ReloadBullet()// thay dan
    {

        if (maxBullet != 0 && move.move != true && curBullet != curBulletReload)
        {
            reload = true;
            anim.SetBool(Constant.RELOAD, true);
            audioSource.clip = audioClip[1];
            audioSource.Play();
            coroutine = WaitReload(3.0f);
            StartCoroutine(coroutine);
            
        }

    }

    void Fire() // ban dan
    {
        if (curBullet <= 0)
        {

            Debug.Log("Het Dan");
            audioSource.clip = audioClip[2];
            audioSource.Play();
            return;
        }
        if (reload) return;
        Ray ray = Camera.main.ScreenPointToRay(nongNham.position);
        RaycastHit hit;
        audioSource.clip = audioClip[0];
        audioSource.Play();
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag.Equals("Zombie"))
            {
                Instantiate(shotHole, hit.transform.position, hit.transform.rotation);
                if (GetCurGun() != null)
                {
                    if (Vector3.Distance(transform.position, hit.transform.position) <= GetCurGun().GetComponent<GunInfo>().range)
                    {
                        hit.transform.gameObject.GetComponent<ZombieController>().GetHit(dame);
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<ZombieController>().GetHit(dame / 2);
                    }
                }
            }
            else
            {
                Instantiate(shotHole, hit.transform.position, Quaternion.identity);
            }

        }
        if (move.move == true)
        {
            anim.SetBool(Constant.WALK_FIRING, true);
        }
        else
        {
            anim.SetBool(Constant.SHOOT, true);
        }
        InsTracer();
        //shoot = false;

        curBullet--;
        GetCurGun().GetComponent<GunInfo>().curBullet = curBullet;

        DisplayBulletText();
        coroutine = WaitShoot(0.1f);
        StartCoroutine(coroutine);

    }

    void InsTracer() // particle dan ban
    {
        GameObject trc = Instantiate(tracer, headGun.transform.position, Quaternion.identity);
        Destroy(trc, 0.1f);
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
        if (Check())
        {
            GetDame();
            DisplayBulletText();
        }
    }
}
