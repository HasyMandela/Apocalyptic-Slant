using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    //Movement Var
    public float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float downwardForce;
    float HorizontalSpeed, VerticalSpeed;

    //Jump Check
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float groundRadius;
    [SerializeField] Transform groundCheck;
    bool isGround;

    //GetComponent
    private Rigidbody rb;
    [SerializeField] CinemachineVirtualCamera cinemachineCam;
    public GunSwitch gunSwitch;

    private static PlayerScript instance;
    public static PlayerScript Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<PlayerScript>();
            return instance;
        }
    }
    void Awake(){
        rb = GetComponent<Rigidbody>();
    }
    //Sound
    [SerializeField] GameObject[] footstepSound;
    [SerializeField] float footstepTimer;
    float timeBtwFootstep;
    //[SerializeField] AudioSource jumpSound;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //Checking For isGround
        isGround = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayerMask);


        //Getting Input
        HorizontalSpeed = Input.GetAxis("Horizontal");
        VerticalSpeed = Input.GetAxis("Vertical");

        // SFX
        int random = Random.Range(0, footstepSound.Length);
        if (VerticalSpeed != 0 || HorizontalSpeed != 0){
            if (timeBtwFootstep <= 0){
                Instantiate(footstepSound[random], transform.position, Quaternion.identity);
                timeBtwFootstep = footstepTimer;
            } else {
                timeBtwFootstep -= Time.deltaTime;
            }
        } else if (VerticalSpeed == 0 && HorizontalSpeed == 0){
            return;
        }
    }
    void FixedUpdate()
    {
        //Initial Movement Code
        Vector3 move = cinemachineCam.transform.right * HorizontalSpeed + cinemachineCam.transform.forward * VerticalSpeed;
        rb.velocity = new Vector3(move.x * speed * Time.deltaTime, rb.velocity.y, move.z * speed * Time.deltaTime);

        //Jump
        if (Input.GetKey(KeyCode.Space) && isGround){
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime * 100f);
        }

        //Adding More Force To Player When Not Grounded
        if (!isGround){
            rb.AddForce(Vector3.up * -downwardForce * Time.deltaTime);
        }
    }
}
