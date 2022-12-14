using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    Animator myAnim;

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
    public float jumpForce = 200.0f;

    public float rotationSpeed = 0.5f;
    public float camRotationSpeed = 0.05f;

    public float maxSprint = 5.0f;
    float sprintTimer;

    public AudioClip jump;
    public AudioClip backgroundMusic;

    public AudioSource sfxPlayer;
    public AudioSource musicPlayer;

    public float bounceForce = 280f;

    Vector3 respawnPoint = new Vector3(-0.64f,-0.15f,-7.69f);

    public Text timerDisplay;
    float timer;
    bool wantTimer = true;

    int collectablesCollected = 0;

    bool radioActive = false;

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
        if (wantTimer)
        {
            timer += Time.deltaTime;
            timerDisplay.text = timer.ToString("F2");
        }
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
        myAnim.SetBool("isOnGround", isOnGround);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("jumped");
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

        if (collectablesCollected >= 6)
        {
            wantTimer = false;
        }

        sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, maxSprint);

        Vector3 newVelocity = (transform.forward * Input.GetAxis("Vertical") * maxSpeed) + (transform.right * Input.GetAxis("Horizontal") * maxSpeed);

        myAnim.SetFloat("speed", newVelocity.magnitude);


        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);


        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed * -1;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Death Box")
        {
            transform.position = respawnPoint;
        }

        if (other.tag =="radio" && Input.GetKeyDown(KeyCode.E) && !(radioActive))
        {
            //other.GetComponent<SFXSCRIPT>().PlaySoundEffect();
            other.GetComponent<AudioSource>().enabled = true;
            radioActive = true;
        }

        if (other.tag == "bounce")
        {
            myRigidbody.AddForce(new Vector3(0f,70f,0f));
        }

        if (other.tag == "collectable")
        {
            collectablesCollected++;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bounce1")
        {
            myRigidbody.AddForce(new Vector3(0f,70f,0f));
        }
        if (collision.gameObject.tag == "Bounce2")
        {
            myRigidbody.AddForce(new Vector3(0f,bounceForce/3f,0f));
        }
        if (collision.gameObject.tag == "Bounce3")
        {
            myRigidbody.AddForce(new Vector3(0f,bounceForce,0f));
        }
    }
}
