using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerMove_Network : NetworkBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    public AudioClip audioJump;
    public AudioClip audioWalk;

    public AudioSource audioSource;

    private Rigidbody rigid;
    private Animator anim;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    private bool jump;
    private bool walk;
    private bool onGround;

    public void Jump()
    {
        if (Input.GetAxis("Jump") != 0 && onGround)
        {
            rigid.velocity = new Vector3(0f, jumpSpeed, 0f);
            onGround = false;
            anim.SetBool(Constant.JUMP, true);

            if (jump)
            {
                audioSource.clip = audioJump;
                audioSource.Play();
                jump = false;
            }
        }
        else
        {
            jump = true;
            anim.SetBool(Constant.JUMP, false);
        }
    }

    // Camera
    public GameObject vr;
    public Camera cameraVR;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            vr.SetActive(false);
            cameraVR.enabled = false;
            Destroy(this);
            return;
        }
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        //joystick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<VirtualJoyStick>();
        //GameObject.FindGameObjectWithTag("Jump").GetComponent<Button>().onClick.AddListener(Jump);
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CharacterMove();
        Jump();
    }

    void CharacterMove()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (walk)
            {
                audioSource.clip = audioWalk;
                audioSource.Play();
                walk = false;
            }
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
            anim.SetBool(Constant.WALK, true);

        }
        else
        {
            walk = true;
            audioSource.Stop();
            anim.SetBool(Constant.WALK, false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            onGround = true;
        }
    }

    
}