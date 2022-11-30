using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float maxSpeed;
    public float normalSpeed = 09.0f;
    public float sprintSpeed = 14.0f;

    float rotation = 0.0f;
    float camRotation = 0.0f;
    GameObject cam;
    Rigidbody myRigidbody;

    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300.0f;

    float rotationSpeed = 2.0f;
    float camRotationSpeed = 1.5f;

    public float maxSprint = 5.0f;
    float sprintTimer;

    public AudioClip jump;
    public AudioClip backgroundMusic;

    public AudioSource sfxPlayer;
    public AudioSource musicPlayer;

    Animator myAnim;

    Vector3 respawnPoint = new Vector3(-0.64f,-0.15f,-7.69f);
    void Start()
    {
        myAnim = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;

        sprintTimer = maxSprint;

        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(transform.up * jumpForce);
        }

        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0.0f)
        {
            maxSpeed = sprintSpeed;
            sprintTimer = sprintTimer - Time.deltaTime;
        }else 
        {
            maxSpeed = normalSpeed;
            if (Input.GetKey(KeyCode.LeftShift) == false) {
                sprintTimer = sprintTimer + Time.deltaTime;
            }
        }

        sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, maxSprint);

        Vector3 newVelocity = (transform.forward * Input.GetAxis("Vertical") * maxSpeed) + (transform.right * Input.GetAxis("Horizontal") * maxSpeed);

        myAnim.SetFloat("speed", newVelocity.magnitude);


        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);


        rotation = rotation + Input.GetAxis("Mouse X") * camRotation;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Death Box")
        {
            transform.position = respawnPoint;
        }
    }
}
